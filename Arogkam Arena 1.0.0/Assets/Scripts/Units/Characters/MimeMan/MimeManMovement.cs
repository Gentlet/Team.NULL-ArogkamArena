using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimeManMovement : UnitMovement
{
    private int isDoubleJump;

    protected void Update()
    {
        Movement();
    }

    protected override void Movement()
    {
        if (Unit.Stuning || isdash == 3)
            return;

        DebugManager.Instance.listInit("IsJump", isjump.ToString());

        if (!Unit.GetAttack<MimeManAttack>().Ghost && Unit.Attack.isAttacking)
            return;

        string keys = KeyManager.Instance.GetKeys(Unit.tag);

        Vector2 vel = Unit.Rigid.velocity;

        //vel.y = Unit.Rigid.velocity.y;

        isdefance = false;
        Unit.State = UnitState.Idle;
        Unit.ColliderCtrl.MovemetColliderActive("Idle");

        if (!(isjump || isdefance))
            Unit.Animator.SetAnimationTrigger(true);


        #region KeyStay
        if (keys[(int)KeyArray.Down] == keyState.KeyStay.ToChar() && !isjump && !Unit.GetAttack<MimeManAttack>().Ghost)
        {
            Unit.State = UnitState.Defance;

            Unit.Animator.PlayAnimation("Defance");
            Unit.ColliderCtrl.MovemetColliderActive("Defance");

            vel = Vector2.zero;

            isdefance = true;
        }

        if (keys[(int)KeyArray.Up] == keyState.KeyDown.ToChar() && !isdefance && !Unit.GetAttack<MimeManAttack>().Ghost)
        {
            if (!isjump)
            {
                Unit.State = UnitState.Jump;

                Unit.Animator.PlayAnimation("Jump");
                Unit.ColliderCtrl.MovemetColliderActive("Jump");

                vel.y = Unit.Status.jump_force;

                isjump = true;
                isDoubleJump = 1;
            }
            else if (isDoubleJump == 2)
            {
                Unit.State = UnitState.Jump;

                Unit.Animator.PlayAnimation("Jump");
                Unit.ColliderCtrl.MovemetColliderActive("Jump");

                vel.y = Unit.Status.jump_force;

                isDoubleJump = 3;
            }
        }

        if (keys[(int)KeyArray.Right] == keyState.KeyStay.ToChar() && !isdefance)
        {
            if (!Unit.GetAttack<MimeManAttack>().Ghost)
            {
                if (isdash == 1)
                    isdash = 2;

                Unit.State = (isdash == 2 ? UnitState.RightDash : UnitState.Right);

                if (!isjump)
                {
                    Unit.Animator.PlayAnimation((isdash == 2 ? "Dash" : "Run"));
                    Unit.ColliderCtrl.MovemetColliderActive((isdash == 2 ? "Dash" : "Run"));
                }
            }

            Unit.transform.rotation = Quaternion.Euler(Unit.transform.rotation.eulerAngles.x, 180, Unit.transform.rotation.eulerAngles.z);

            vel.x = Unit.Status.speed * (isdash == 2 ? 2 : 1) * (Unit.Attack.isRight ? 1f : -1f);
        }

        if (keys[(int)KeyArray.Left] == keyState.KeyStay.ToChar() && !isdefance)
        {
            if (!Unit.GetAttack<MimeManAttack>().Ghost)
            {
                if (isdash == 1)
                    isdash = 2;

                Unit.State = (isdash == 2 ? UnitState.LeftDash : UnitState.Left);

                if (!isjump)
                {
                    Unit.Animator.PlayAnimation((isdash == 2 ? "Dash" : "Run"));
                    Unit.ColliderCtrl.MovemetColliderActive((isdash == 2 ? "Dash" : "Run"));
                }
            }
            Unit.transform.rotation = Quaternion.Euler(Unit.transform.rotation.eulerAngles.x, 0, Unit.transform.rotation.eulerAngles.z);

            vel.x = Unit.Status.speed * (isdash == 2 ? 2 : 1) * (Unit.Attack.isRight ? 1f : -1f);
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
        if (keys[(int)KeyArray.Up] == keyState.KeyUp.ToChar() && !isdefance)
        {
            if (isDoubleJump == 1)
                isDoubleJump = 2;
        }

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
    #region Properties
    #endregion
}
