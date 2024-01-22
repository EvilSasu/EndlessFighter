using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FightAnimationController : MonoBehaviour
{
    public float attackDuration;
    public float idleDuration;
    public float getDmgDuration;
    public float healDuration;
    public float fireballDuration;
    public float lightingStrikeDuration;

    public ParticleSystem blood;
    private const string ENEMY_BLOOD_TAG = "EnemyBlood";

    private Animator _animator;
    private const string GET_DMG_TRIGGER = "GetDmg";
    private const string IDLE_TRIGGER = "Idle";
    private const string ATTACK_ONE_HAND_TRIGGER = "AttackOneHand";
    private const string HEAL_TRIGGER = "Heal";
    private const string FIRE_BALL_TRIGGER = "Fireball";
    private const string LIGHTING_STRIKE_TRIGGER = "LightingStrike";
    private const string DODGE_TRIGGER = "Dodge";
    private void Awake()
    {
        if (blood == null)
            FindBloodObject();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "Heal":
                    healDuration = clip.length; break;
                case "AttackOneHand":
                    attackDuration = clip.length; break;
                case "Idle":
                    idleDuration = clip.length; break;
                case "GetDmg":
                    getDmgDuration = clip.length; break;
                case "Fireball":
                    fireballDuration = clip.length; break;
                case "LightingStrike":
                    lightingStrikeDuration = clip.length; break;
            }
        }
    }

    public void PlayAnimGetDmg()
    {
        _animator.SetTrigger(GET_DMG_TRIGGER);
        blood.Play();
    }

    public void PlayAnimIdle()
    {
        _animator.SetTrigger(IDLE_TRIGGER);
    }

    public void PlayAnimAttackOneHand(Action onAnimationEndCallback)
    {
        _animator.GetBehaviour<AnimationEndEvent>().onAnimationEndCallback = onAnimationEndCallback;

        _animator.SetTrigger(ATTACK_ONE_HAND_TRIGGER);
    }

    public void PlayAnimHeal()
    {
        _animator.SetTrigger(HEAL_TRIGGER);
    }

    public void PlayAnimFireBall()
    {
        _animator.SetTrigger(FIRE_BALL_TRIGGER);
    }

    public void PlayAnimLightingStrike()
    {
        _animator.SetTrigger(LIGHTING_STRIKE_TRIGGER);
    }

    public void PlayAnimDodge()
    {
        _animator.SetTrigger(DODGE_TRIGGER);
    }

    private void FindBloodObject()
    {
        if (GetComponent<Unit>().isPlayer == false)
            blood = GameObject.FindGameObjectWithTag(ENEMY_BLOOD_TAG).GetComponent<ParticleSystem>();
    }
}
