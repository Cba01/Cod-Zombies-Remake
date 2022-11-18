using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    public void SetAnimationController(AnimatorOverrideController animatorOverrideController)
    {
        animator.runtimeAnimatorController = animatorOverrideController;
    }
    public void GunUpAnimation()
    {
        animator.Play("Pistol_Up");
    }

    public void PlayReloadAnimation()
    {
        animator.SetTrigger("Reload");
    }

}
