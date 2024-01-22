using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private static BattleController instance;

    public static BattleController GetInstance() {  return instance; }
    public FightAchievementUpdate fightAchievementUpdate;

    public GameObject parent;
    public GameObject player;
    public GameObject enemyPrefab;

    public List<GameObject> enemies;
    public List<int> enemiesLevels;
    public SaveLevelProgress levelProgress;
    
    public Transform playerRespownPos;
    public Transform enemyRespownPos;
    private int _indexOfEnemy = 0;

    public delegate void PlayerAction(params object[] parameters);

    private List<PlayerAction> playerActions = new List<PlayerAction>();
    private Dictionary<PlayerAction, object[]> actionParameters = new Dictionary<PlayerAction, object[]>();

    private GameObject _fightingEnemy;
    private State _state;

    private UnitBattle _playerUnitBattle;
    private UnitBattle _fightingEnemyUnitBattle;
    private UnitBattle _activeUnitBattle;

    private bool _canStartBattle = true;
    private Action actionOnComplitedAnimation;
    private bool _levelComplited = false;

    private enum State
    {
        WaitingForPlayer,
        Busy
    }

    private void Awake()
    {
        instance = this;
        actionOnComplitedAnimation = () =>
        {
            if (playerActions.Count <= 0)
                ChooseNextActiveUnitBattle();
            else
                ExecuteActionFromList(0);
        };

        EnemyLevelsDataSS dataForEnemies = LoadEnemyData();
        FromDataToGameObejct(dataForEnemies);
    }

    private void Start()
    {
        SpawnCharacter(true);
        SpawnCharacter(false);

        SetActiveUnitBattle(_playerUnitBattle);
        _state = State.WaitingForPlayer;
        StartCoroutine(WaitForEnemy());
    }

    private void Update()
    {
        if (!_levelComplited)
        {
            if (player != null)
            {
                CheckIfEnemyIsDead();

                if (_canStartBattle)
                {
                    if (_state == State.WaitingForPlayer)
                    {
                        _state = State.Busy;

                        AddActionToQueue(_playerUnitBattle.AttackAction, _fightingEnemyUnitBattle, actionOnComplitedAnimation);
                        ExecuteActionFromList(0);
                    }
                }
            }
            else
            {
                levelProgress.LevelCompleted(false);
                _levelComplited = true;
                (this as Behaviour).enabled = false;
            }
        }         
    }

    public void SpawnCharacter(bool isPlayerTeam)
    {
        if(isPlayerTeam)
        {
            player = SetupNewCreatedCharacter(player, playerRespownPos);
            _playerUnitBattle = player.GetComponent<UnitBattle>();
            levelProgress.player = player;
            player.GetComponent<Unit>().isPlayer = true;
            player.GetComponent<Unit>().CalculateAll();
        }
        else
        {
            if(enemiesLevels.Count != 0)
            {
                _fightingEnemy = SetupNewCreatedCharacter(enemyPrefab, enemyRespownPos);
                _fightingEnemy.GetComponent<Unit>().unitLevel = enemiesLevels[0];
                _fightingEnemyUnitBattle = _fightingEnemy.GetComponent<UnitBattle>();
                enemiesLevels.Remove(enemiesLevels[0]);
                _indexOfEnemy++;
            }          
        }
    }

    private GameObject SetupNewCreatedCharacter(GameObject newObject, Transform pos)
    {
        if (newObject is null)
            throw new ArgumentNullException(nameof(newObject));
        if (pos is null)
            throw new ArgumentNullException(nameof(pos));

        GameObject refer;
        refer = Instantiate(newObject, pos.position, Quaternion.identity);
        refer.transform.SetParent(parent.transform, false);
        refer.transform.position = pos.position;
        refer.transform.rotation = newObject.transform.rotation;
        refer.gameObject.SetActive(true);
        return refer;
    }

    private void SetActiveUnitBattle(UnitBattle unitBattle)
    {
        _activeUnitBattle = unitBattle;
    }

    private void ChooseNextActiveUnitBattle()
    {
        if (_activeUnitBattle == _playerUnitBattle)
        {
            _state = State.Busy;
            if(_fightingEnemyUnitBattle != null)
            {
                if(_fightingEnemy.GetComponent<MoveToTarget>().enabled == false)
                {
                    SetActiveUnitBattle(_fightingEnemyUnitBattle);
                    _fightingEnemyUnitBattle.Attack(_playerUnitBattle, () =>
                    {
                        ChooseNextActiveUnitBattle();
                    });
                }
                else
                {
                    StartCoroutine(DelayAttackForTarget());
                }
                
            }         
        }          
        else
        {
            if(_playerUnitBattle != null)
            {
                SetActiveUnitBattle(_playerUnitBattle);
                _state = State.WaitingForPlayer;
            }          
        }          
    }

    private void CheckIfEnemyIsDead()
    {
        PanelWinLoseControl();

        if (_fightingEnemy != null)
        {
            if (_fightingEnemy.GetComponent<Unit>().currentHp <= 0)
            {
                levelProgress.GetComponent<DropController>().DropOnDeath(_fightingEnemy.GetComponent<Unit>());
                fightAchievementUpdate.UpdateEnemiesDefeated();
                Destroy(_fightingEnemy);
                CleanActionList();
                SpawnCharacter(false);
                SetActiveUnitBattle(_playerUnitBattle);
                StartCoroutine(WaitForEnemy());
            }
        }
    }

    private void PanelWinLoseControl()
    {
        if(player.GetComponent<Unit>().currentHp <= 0)
        {
            Destroy(player);
            CleanActionList();
        }
        if (_fightingEnemy == null && enemies.Count == 0)
        {
            levelProgress.LevelCompleted(true);
            _levelComplited = true;
        }         
        if (player == null && enemies.Count > 0)
        {
            levelProgress.LevelCompleted(false);
            _levelComplited = true;
        }
            
    }

    public void AddActionToQueue(PlayerAction playerAction, params object[] parameters)
    {
        playerActions.Add(playerAction);
        actionParameters[playerAction] = parameters;
    }

    private void ExecuteActionList()
    {
        foreach (var playerAction in playerActions)
        {
            playerAction(actionParameters[playerAction]);
        }
    }

    private void ExecuteActionFromList(int i)
    {
        PlayerAction action = playerActions[i];
        action(actionParameters[action]);
        actionParameters.Remove(action);
        playerActions.Remove(action);
    }

    private void CleanActionList()
    {
        playerActions = new List<PlayerAction>();
        actionParameters = new Dictionary<PlayerAction, object[]>();
    }

    IEnumerator WaitForEnemy()
    {
        _canStartBattle = false;
        yield return new WaitForSeconds(2f);
        _canStartBattle = true;
    }

    IEnumerator DelayAttackForTarget()
    {
        yield return new WaitForSeconds(2f);
        if(_fightingEnemyUnitBattle != null)
        {
            SetActiveUnitBattle(_fightingEnemyUnitBattle);
            _fightingEnemyUnitBattle.Attack(_playerUnitBattle, () =>
            {
                ChooseNextActiveUnitBattle();
            });
        }     
    }

    private EnemyLevelsDataSS LoadEnemyData()
    {
        string jsonEnemiesLevels = PlayerPrefs.GetString("EnemiesOnNextLevel", "");

        if (!string.IsNullOrEmpty(jsonEnemiesLevels))
        {
            EnemyLevelsDataSS loadedEnemiesData = JsonUtility.FromJson<EnemyLevelsDataSS>(jsonEnemiesLevels);
            return loadedEnemiesData;
        }
        else
        {
           // Debug.LogWarning("Enemy lvl not found in PlayerPrefs.");
            return null;
        }
    }

    private void FromDataToGameObejct(EnemyLevelsDataSS dataSS)
    {
        if (dataSS != null && dataSS.enemyLevels != null)
        {
            foreach (EnemyInfo enemyLVL in dataSS.enemyLevels)
            {
                enemiesLevels.Add(enemyLVL.level);
            }
            levelProgress.nameOfLevel = dataSS.nameOfLevel;
            levelProgress.timeToBeat = dataSS.TimeToBeat;
        }
        else
        {
           // Debug.LogWarning("Failed to load object list from JSON");
        }     
    }

    // skills for player
    public void AddAttack()
    {
        if(_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.DoubleAttack, _fightingEnemyUnitBattle, actionOnComplitedAnimation);
    }

    public void AddDoubleAttack(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.DoubleAttack, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }

    public void AddDefendYourself(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.DefendYourSelf, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }

    public void AddBerserk(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.Berserk, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }

    public void AddHeal(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.HealAction, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }

    public void AddFireball(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.Fireball, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }

    public void AddLightingStrike(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.LightingStrike, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }

    public void AddBowShot(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.BowShot, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }

    public void AddDodger(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.Dodger, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }

    public void AddCritFighter(Skill skill)
    {
        if (_playerUnitBattle != null && _fightingEnemyUnitBattle != null)
            AddActionToQueue(_playerUnitBattle.CritFighter, _fightingEnemyUnitBattle, actionOnComplitedAnimation, skill);
    }
}
