using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    public FightAnimationController animationController;
    private float currentTimer = 20f;
    private int randomNumberForAnimation;
    private void Update()
    {
        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0)
        {
            randomNumberForAnimation = Random.Range(1, 6);
            switch (randomNumberForAnimation)
            {
                case 1: animationController.PlayAnimAttackOneHand(FunctionThatDoNothing); break;
                case 2: animationController.PlayAnimDodge(); break;
                case 3: animationController.PlayAnimFireBall(); break;
                case 4: animationController.PlayAnimLightingStrike(); break;
                case 5: animationController.PlayAnimHeal(); break;
                default: animationController.PlayAnimIdle(); break;
            }
            currentTimer = 20f;
        }
    }

    private void FunctionThatDoNothing()
    {

    }
}
