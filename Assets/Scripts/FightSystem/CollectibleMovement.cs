using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public SaveLevelProgress progress;

    public Transform target;
    public float speed = 20f;

    public float minStartSpeed = 25f;
    public float maxStartSpeed = 50f;

    public int value;
    public Type typeOf;

    public enum Type
    {
        Gold,
        Exp,
        Diamonds
    }

    private Rigidbody2D _rb;
    private GameObject _player;
    private bool _canMove = false;
    private GameObject _levelProgress;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("PlayerObj");
        target = _player.transform;
        _levelProgress = GameObject.FindGameObjectWithTag("LevelProgress");
    }

    private void Start()
    {
        StartCoroutine(WaitForMove(0f,Random.Range(1f,3f)));
    }

    private void FixedUpdate()
    {
        if (winPanel != null && losePanel != null)
        {
            if (winPanel.activeSelf == true || losePanel.activeSelf == true)
            {
                AddValueToProgress();
                progress.SetAllCollectivesToText();
                Destroy(gameObject);
            }
        }
        if (_canMove)
        {
            if (target != null)
            {
                _rb.gravityScale = 0;
                Vector3 dir = (target.position - transform.position).normalized;
                _rb.velocity = dir * speed;
                if (Vector3.Distance(transform.position, target.position) <= transform.localScale.x || transform.position.x < target.position.x)
                {
                    AddValueToProgress();
                    Destroy(gameObject);
                }
            }
        }          
    }


    IEnumerator WaitForMove(float time1, float time2)
    {
        yield return new WaitForSeconds(time1);
        if (_rb != null)
        {
            Vector3 randomVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
            float randomSpeed = Random.Range(minStartSpeed, maxStartSpeed);
            _rb.velocity = randomVelocity * randomSpeed;
        }
        yield return new WaitForSeconds(time2);
        _canMove = true;
    }

    private void AddValueToProgress()
    {
        if(_levelProgress != null)
        {
            if (typeOf == Type.Gold)
                _levelProgress.GetComponent<SaveLevelProgress>().AddGold(value);
            else if(typeOf == Type.Exp)
                _levelProgress.GetComponent<SaveLevelProgress>().AddExp(value);
            else if(typeOf == Type.Diamonds)
                _levelProgress.GetComponent<SaveLevelProgress>().AddDiamonds(value);
        }
    }
}
