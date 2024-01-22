using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public GameObject target;
    public float speed;

    private const string ENEMY_TARGET_TAG = "EnemyTarget";

    private void Awake()
    {
        if(target == null)
            FindTarget();
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, target.transform.position) > 0.1f)
        {
            MoveToTar();
        }
        else
        {
            //GetComponent<UnitBattle>().SetState(UnitBattle.State.Idle);
            (this as Behaviour).enabled = false;
        }
    }

    private void MoveToTar()
    {
        Vector3 way = (target.transform.position - transform.position).normalized;

        Vector3 newPos = transform.position + way * speed * Time.deltaTime;

        transform.position = newPos;
    }

    private void FindTarget()
    {
        if(GetComponent<Unit>().isPlayer == false) 
        {
            target = GameObject.FindGameObjectWithTag(ENEMY_TARGET_TAG);
        }
    }
}
