using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float currentTimer = 3f;

    private void Update()
    {
        currentTimer -= Time.deltaTime;

        if(currentTimer <= 0)
            Destroy(gameObject);
    }
}
