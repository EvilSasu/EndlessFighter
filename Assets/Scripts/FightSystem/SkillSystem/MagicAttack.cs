using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public ParticleSystem system;
    public Unit enemy;
    public float damage;
    public Unit player;

    private const string ENEMY_TARGET_TAG = "EnemyTarget";

    private void Awake()
    {
        if (target == null)
            FindTarget();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > 1f)
        {
            MoveToTar();
            if(gameObject.name == "FireballPref")
            {
                if (transform.rotation == Quaternion.Euler(0, 0, 0))
                    transform.rotation = Quaternion.Euler(180, 0, 0);
                else
                    transform.rotation = Quaternion.Euler(0, 0, 0);
            }else if(gameObject.name == "LightingStrikePref")
            {
                if (transform.rotation == Quaternion.Euler(0, 180, -30))
                    transform.rotation = Quaternion.Euler(0, 0, 30);
                else
                    transform.rotation = Quaternion.Euler(0, 180, -30);
            }
        }
        else
        {
            system.Play();
            if(enemy != null)
            {
                enemy.healthManager.TakeDamage(damage, "magic", player.critChanceWithPassivesAndBuffs, player.critDmgMultiplicationWithPassivesAndBuffs);
            }
            Destroy(gameObject);
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
        if (GetComponent<Unit>().isPlayer == false)
        {
            target = GameObject.FindGameObjectWithTag(ENEMY_TARGET_TAG);
        }
    }
}
