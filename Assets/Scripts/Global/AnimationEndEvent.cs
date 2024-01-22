using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEndEvent : StateMachineBehaviour
{
    public System.Action onAnimationEndCallback;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onAnimationEndCallback?.Invoke();
    }
}
