using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    public GameObject coinPref;
    public GameObject expPref;
    public GameObject diamondsPref;
    public GameObject _parent;
    public Transform target;

    private int _baseGold = 5;
    private int _baseExp = 10;
    private int _baseDiamonds = 1;
    private void Awake()
    {
        _parent = GameObject.FindGameObjectWithTag("FightCanvas");
    }

    public void DropOnDeath(Unit enemy)
    {
        int _lvl = enemy.unitLevel;
        int amountOfGold = Random.Range((_baseGold * _lvl)/2, (_baseGold * _lvl));
        int amountOfExp = Random.Range((_baseExp * _lvl) / 2, (_baseExp * _lvl));
        int amountOfDiamonds = Random.Range((_baseDiamonds * _lvl) / 2, (_baseDiamonds * _lvl));

        CaculateAmount(coinPref, amountOfGold);
        CaculateAmount(expPref, amountOfExp);
        CaculateAmount(diamondsPref, amountOfDiamonds);
    }

    private void CaculateAmount(GameObject obj, int amount)
    {
        int _amountOfBig = amount / 100;
        int _amountOfMedium = (amount % 100) / 10;
        int _amountOfSmall = amount % 10;

        DropAmount(obj, 100, 5f, _amountOfBig);
        DropAmount(obj, 10, 2.5f, _amountOfMedium);
        DropAmount(obj, 1, 1f, _amountOfSmall);
    }

    private void DropAmount(GameObject obj, int value, float size, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            SetupNewObject(obj, value, size);
        }
    }

    private void SetupNewObject(GameObject obj, int value, float size)
    {
        GameObject newObj = Instantiate(obj, target.position, Quaternion.identity);
        newObj.SetActive(true);
        newObj.transform.SetParent(_parent.transform);
        newObj.transform.localScale = new Vector3(size, size, size);
        newObj.GetComponent<CollectibleMovement>().value = value;
    }
}
