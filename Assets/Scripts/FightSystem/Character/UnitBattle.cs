using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBattle : MonoBehaviour
{
    private Unit unit;
    private FightAnimationController fightAnimationController;
    public State _state;
    private Vector3 _dashTargetPosition;
    private Action _onDashComplited;
    public GameObject fireBall;
    public GameObject lightingStrike;
    public GameObject canvas;
    public ActiveSkillController activeSkillController;
    public enum State
    {
        Idle,
        Dashing,
        Busy,
    }

    private void Awake()
    {
        unit = GetComponent<Unit>();
        fightAnimationController = GetComponent<FightAnimationController>();
        _state = State.Idle;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Dashing:
                float _dashSpeed = 10f;
                transform.position += (_dashTargetPosition - GetPosition()) * _dashSpeed * Time.deltaTime;

                float _reachDisctance = 1f;
                if (Vector3.Distance(GetPosition(), _dashTargetPosition) < _reachDisctance)
                {
                    transform.position = _dashTargetPosition;
                    _onDashComplited();
                }
                break;
            case State.Busy:
                break;
        }
    }

    public void Setup(bool isPlayerTeam)
    {
        if(isPlayerTeam)
        {

        }
        else
        {

        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Attack(UnitBattle targetUnitBattle ,Action OnAttackComplited)
    {
        Vector3 _dashTargetPosition = targetUnitBattle.GetPosition() + (GetPosition() - targetUnitBattle.GetPosition()).normalized * 1f;
        Vector3 startPosition = GetPosition();
        DashToPosition(_dashTargetPosition, () =>
        {
            _state = State.Busy;
            targetUnitBattle.unit.healthManager.TakeDamage(unit.physicalDmgWithPassivesAndBuffs, "physical", 
                unit.critChanceWithPassivesAndBuffs, unit.critDmgMultiplicationWithPassivesAndBuffs);
            fightAnimationController.PlayAnimAttackOneHand(() => {
                
                DashToPosition(startPosition, () =>
                {
                    _state = State.Idle;
                    OnAttackComplited();
                });
                
            });
        });
    }

    public void AttackAction(object[] paramiters)
    {
        UnitBattle targetUnitBattle = (UnitBattle)paramiters[0];
        Action OnAttackComplited = (Action)paramiters[1];
        if(targetUnitBattle != null)
        {
            Vector3 _dashTargetPosition = targetUnitBattle.GetPosition() + (GetPosition() - targetUnitBattle.GetPosition()).normalized;
            Vector3 startPosition = GetPosition();
            DashToPosition(_dashTargetPosition, () =>
            {
                _state = State.Busy;
                targetUnitBattle.unit.healthManager.TakeDamage(unit.physicalDmgWithPassivesAndBuffs, "physical",
                    unit.critChanceWithPassivesAndBuffs, unit.critDmgMultiplicationWithPassivesAndBuffs);
                fightAnimationController.PlayAnimAttackOneHand(() => {

                    DashToPosition(startPosition, () =>
                    {
                        _state = State.Idle;
                        OnAttackComplited();
                    });

                });
            });
        }else
        {
            OnAttackComplited();
        }  
    }

    // Skills

    public void DoubleAttack(object[] paramiters)
    {
        UnitBattle targetUnitBattle = (UnitBattle)paramiters[0];
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];
        float damageToDeal = unit.physicalDmg * (1.25f + (skill.skillLevel * (skill.bonusPerLevel / 100)));

        Vector3 _dashTargetPosition = targetUnitBattle.GetPosition() + (GetPosition() - targetUnitBattle.GetPosition()).normalized * 1f;
        Vector3 startPosition = GetPosition();
        DashToPosition(_dashTargetPosition, () =>
        {
            _state = State.Busy;
            targetUnitBattle.unit.healthManager.TakeDamage(unit.physicalDmgWithPassivesAndBuffs, "physical",
                unit.critChanceWithPassivesAndBuffs, unit.critDmgMultiplicationWithPassivesAndBuffs);
            fightAnimationController.PlayAnimAttackOneHand(() => {
                if (targetUnitBattle.unit.currentHp <= 0)
                {
                    DashToPosition(startPosition, () =>
                    {
                        _state = State.Idle;
                        OnAttackComplited();
                    });
                }
                else
                {
                    targetUnitBattle.unit.healthManager.TakeDamage(unit.physicalDmgWithPassivesAndBuffs, "physical",
                        unit.critChanceWithPassivesAndBuffs, unit.critDmgMultiplicationWithPassivesAndBuffs);
                    fightAnimationController.PlayAnimAttackOneHand(() => {

                        DashToPosition(startPosition, () =>
                        {
                            _state = State.Idle;
                            OnAttackComplited();
                        });

                    });
                }
            });
        });
    }

    public void HealAction(object[] paramiters)
    {
        UnitBattle targetUnitBattle = (UnitBattle)paramiters[0];
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];

        float healAmout = unit.maxHp * (0.25f + (skill.skillLevel * (skill.bonusPerLevel / 100)));

        _state = State.Busy;
        unit.healthManager.Heal(healAmout);

        fightAnimationController.PlayAnimHeal();

        StartCoroutine(OnSkillComplited(fightAnimationController.healDuration, OnAttackComplited));
    }

    public void DefendYourSelf(object[] paramiters)
    {
        _state = State.Busy;
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];
        Debug.Log("Using skill: " + skill.skillName.ToString());
        fightAnimationController.PlayAnimHeal();
        activeSkillController.ActivateSkillInUI(skill, unit);
        StartCoroutine(OnSkillComplited(fightAnimationController.healDuration, OnAttackComplited));
    }

    public void Berserk(object[] paramiters)
    {
        _state = State.Busy;
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];
        Debug.Log("Using skill: " + skill.skillName.ToString());
        fightAnimationController.PlayAnimHeal();
        activeSkillController.ActivateSkillInUI(skill, unit);
        StartCoroutine(OnSkillComplited(fightAnimationController.healDuration, OnAttackComplited));
    }

    public void Fireball(object[] paramiters)
    {
        _state = State.Busy;
        UnitBattle targetUnitBattle = (UnitBattle)paramiters[0];
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];
        Debug.Log("Using skill: " + skill.skillName.ToString());
        fightAnimationController.PlayAnimFireBall();
        CreateFireBall(targetUnitBattle, unit.magicDmgWithPassivesAndBuffs);
        StartCoroutine(OnSkillComplited(fightAnimationController.fireballDuration, OnAttackComplited));
    }

    private void CreateFireBall(UnitBattle enemy, float damage)
    {
        _state = State.Busy;
        GameObject newFireBall = Instantiate(fireBall, fireBall.transform.position, Quaternion.identity);
        newFireBall.transform.SetParent(canvas.transform, false);
        newFireBall.transform.position = fireBall.transform.position;
        newFireBall.name = "FireballPref";
        newFireBall.GetComponent<MagicAttack>().damage = damage;
        newFireBall.GetComponent <MagicAttack>().enemy = enemy.unit;
        newFireBall.GetComponent<MagicAttack>().player = unit;
        newFireBall.SetActive(true);
    }

    public void LightingStrike(object[] paramiters)
    {
        _state = State.Busy;
        UnitBattle targetUnitBattle = (UnitBattle)paramiters[0];
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];
        Debug.Log("Using skill: " + skill.skillName.ToString());
        fightAnimationController.PlayAnimLightingStrike();
        CreateLightingStrike(targetUnitBattle, unit.magicDmgWithPassivesAndBuffs);
        StartCoroutine(OnSkillComplited(fightAnimationController.lightingStrikeDuration, OnAttackComplited));
    }

    private void CreateLightingStrike(UnitBattle enemy, float damage)
    {
        _state = State.Busy;
        GameObject newLightingStrike = Instantiate(lightingStrike, lightingStrike.transform.position, Quaternion.Euler(0f, 180f, -30f));
        newLightingStrike.transform.SetParent(canvas.transform, false);
        newLightingStrike.transform.position = lightingStrike.transform.position;
        newLightingStrike.name = "LightingStrikePref";
        newLightingStrike.GetComponent<MagicAttack>().damage = damage;
        newLightingStrike.GetComponent<MagicAttack>().enemy = enemy.unit;
        newLightingStrike.GetComponent<MagicAttack>().player = unit;
        newLightingStrike.SetActive(true);
    }

    public void BowShot(object[] paramiters)
    {
        _state = State.Busy;
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];
        Debug.Log("Using skill: " + skill.skillName.ToString());
        fightAnimationController.PlayAnimHeal();

        StartCoroutine(OnSkillComplited(fightAnimationController.healDuration, OnAttackComplited));
    }

    public void Dodger(object[] paramiters)
    {
        _state = State.Busy;
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];
        Debug.Log("Using skill: " + skill.skillName.ToString());
        fightAnimationController.PlayAnimHeal();
        activeSkillController.ActivateSkillInUI(skill, unit);
        StartCoroutine(OnSkillComplited(fightAnimationController.healDuration, OnAttackComplited));
    }

    public void CritFighter(object[] paramiters)
    {
        _state = State.Busy;
        Action OnAttackComplited = (Action)paramiters[1];
        Skill skill = (Skill)paramiters[2];
        Debug.Log("Using skill: " + skill.skillName.ToString());
        fightAnimationController.PlayAnimHeal();
        activeSkillController.ActivateSkillInUI(skill, unit);
        StartCoroutine(OnSkillComplited(fightAnimationController.healDuration, OnAttackComplited));
    }

    private void DashToPosition(Vector3 _dashTargetPosition, Action _onDashComplited)
    {
        this._dashTargetPosition = _dashTargetPosition;
        this._onDashComplited = _onDashComplited;
        _state = State.Dashing;
    }

    public void SetState(State state) => this._state = state;

    private IEnumerator OnSkillComplited(float time, Action OnAttackComplited)
    {
        yield return new WaitForSeconds(time);
        _state = State.Idle;
        OnAttackComplited();
    }
}
