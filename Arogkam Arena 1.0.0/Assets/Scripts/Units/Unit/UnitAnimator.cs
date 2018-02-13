using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : ChildUnitInterface {

    protected Animator animator;

    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayAnimation(string name)
    {
        animator.Play(name);

        if (!Unit.Attack.isAttacking)
            SetAnimationTrigger(false);
    }

    public void SetAnimationTrigger(bool type)
    {
        animator.SetBool("Trigger", type);
    }

    #region Properties
    
    public bool FlipX
    {
        get
        {
            return spriteRenderer.flipX;
        }
        set
        {
            spriteRenderer.flipX = value;
        }
    }

#endregion
}
