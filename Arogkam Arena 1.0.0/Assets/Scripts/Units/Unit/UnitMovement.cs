using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitMovement : ChildUnitInterface {
    
    protected int isdash;

    protected bool isjump;
    protected bool isdefance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Vector2 vel = Unit.Rigid.velocity;

            vel.y = 0f;

            Unit.Rigid.velocity = vel;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && Unit.Rigid.velocity.y <= 0f)
        {
            isjump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && 0f <= Unit.Rigid.velocity.y)
        {
            isjump = true;
        }
    }

    void Start () {
	}
	
	void Update () {
        Movement();

    }

    protected void Movement()
    {
        DebugManager.Instance.listInit("IsJump", isjump.ToString());

        if (Unit.Attack.isAttacking)
            return;

        string keys = KeyManager.Instance.GetKeys(Unit.tag);

        Vector2 vel = Vector2.zero;

        vel.y = Unit.Rigid.velocity.y;

        isdefance = false;
        Unit.State = UnitState.Idle;
        Unit.ColliderCtrl.MovemetColliderActive("Idle");

        if (!(isjump || isdefance))
            Unit.Animator.SetAnimationTrigger(true);


        #region KeyStay
        if (keys[(int)KeyArray.Down] == keyState.KeyStay.ToChar() && !isjump)
        {
            Unit.State = UnitState.Defance;

            Unit.Animator.PlayAnimation("Defance");
            Unit.ColliderCtrl.MovemetColliderActive("Defance");

            isdefance = true;
        }

         if (keys[(int)KeyArray.Up] == keyState.KeyStay.ToChar() && !isdefance)
        {
            if (!isjump)
            {
                Unit.State = UnitState.Jump;

                Unit.Animator.PlayAnimation("Jump");
                Unit.ColliderCtrl.MovemetColliderActive("Jump");

                vel.y = Unit.Status.jump_force;

                isjump = true;
            }
        }

        if (keys[(int)KeyArray.Right] == keyState.KeyStay.ToChar() && !isdefance)
        {
            if (isdash == 1)
                isdash = 2;

            Unit.State = (isdash == 2 ? UnitState.RightDash : UnitState.Right);

            if (!isjump)
            {
                Unit.Animator.PlayAnimation((isdash == 2 ? "Dash" : "Run"));
                Unit.ColliderCtrl.MovemetColliderActive((isdash == 2 ? "Dash" : "Run"));
            }
            Unit.transform.rotation = Quaternion.Euler(Unit.transform.rotation.eulerAngles.x, 180, Unit.transform.rotation.eulerAngles.z);

            vel.x += Unit.Status.speed * (isdash == 2 ? 2 : 1);
        }

        if (keys[(int)KeyArray.Left] == keyState.KeyStay.ToChar() && !isdefance)
        {
            if (isdash == 1)
                isdash = 2;

            Unit.State = (isdash == 2 ? UnitState.LeftDash : UnitState.Left);

            if (!isjump)
            {
                Unit.Animator.PlayAnimation((isdash == 2 ? "Dash" : "Run"));
                Unit.ColliderCtrl.MovemetColliderActive((isdash == 2 ? "Dash" : "Run"));
            }
            Unit.transform.rotation = Quaternion.Euler(Unit.transform.rotation.eulerAngles.x, 0, Unit.transform.rotation.eulerAngles.z);

            vel.x -= Unit.Status.speed * (isdash == 2 ? 2 : 1);
        }
        #endregion

        #region KeyDown
        //        if (keys[(int)KeyArray.Right] == keyState.KeyDown.ToChar() && !isdefance)
        //        {

        //        }

        //        if (keys[(int)KeyArray.Left] == keyState.KeyDown.ToChar() && !isdefance)
        //        {

        //        }
#endregion

        #region KeyUp
        if (keys[(int)KeyArray.Right] == keyState.KeyUp.ToChar() && !isdefance)
        {
            if (isdash == 0)
            {
                isdash = 1;

                StartCoroutine(IsRunReset());
            }
            else
                isdash = 0;
        }

        if (keys[(int)KeyArray.Left] == keyState.KeyUp.ToChar() && !isdefance)
        {
            if (isdash == 0)
            {
                isdash = 1;

                StartCoroutine(IsRunReset());
            }
            else
                isdash = 0;
        }
#endregion

        Unit.Rigid.velocity = vel;
    }

    private IEnumerator IsRunReset()
    {
        yield return new WaitForSeconds(0.3f);

        if (isdash == 1)
            isdash = 0;
    }

    #region Properties

    public bool isJump
    {
        get
        {
            return isjump;
        }
    }

    public bool isDash
    {
        get
        {
            return (isdash == 2 ? true : false);
        }
        set
        {
            if (value)
                isdash = 2;
            else
                isdash = 0;
        }
    }

    public bool isDefance
    {
        get
        {
            return isdefance;
        }
    }
#endregion
}
