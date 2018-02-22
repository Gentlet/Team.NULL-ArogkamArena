using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WesternAttack : UnitAttack
{

    private void Update()
    {
        Attack();

        DebugManager.Instance.listInit("IsAttacking", isAttacking.ToString());
    }


    protected override void Attack()
    {
        if (isAttacking || isattackdelaying || Unit.Movement.isDefance || Unit.State == UnitState.Attack)
            return;

        string keys = KeyManager.Instance.GetKeys(Unit.tag);

        #region KeyStay
        //if (keys[(int)KeyArray.Weak] == keyState.KeyStay.ToChar())
        //{
        //}

        //if (keys[(int)KeyArray.Strong] == keyState.KeyStay.ToChar())
        //{
        //}

        //if (keys[(int)KeyArray.Skill1] == keyState.KeyStay.ToChar())
        //{
        //}

        //if (keys[(int)KeyArray.SKill2] == keyState.KeyStay.ToChar())
        //{
        //}

        //if (keys[(int)KeyArray.special] == keyState.KeyStay.ToChar())
        //{
        //}
        #endregion

        #region KeyDown
        if (keys[(int)KeyArray.Weak] == keyState.KeyDown.ToChar())
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;

            if (Unit.Movement.isJump)
            {
                Unit.Animator.PlayAnimation("JumpAttack");
                Unit.ColliderCtrl.AttackColliderActive(properties[0]);
            }
            else if (Unit.Movement.isDash)
            {
                Unit.Animator.PlayAnimation("DashAttack");
                Unit.ColliderCtrl.AttackColliderActive(properties[1]);

                Vector2 vel = Unit.Rigid.velocity;

                vel.x = 20f * (isRight ? 1 : -1);

                Unit.Rigid.velocity = vel;

                Unit.Movement.isDash = false;
            }
            else
            {
                weakattackcombo += 1;

                Unit.Animator.PlayAnimation("WeakAttack" + weakattackcombo.ToString());
                Unit.ColliderCtrl.AttackColliderActive(properties[1 + weakattackcombo]);

                if (weakattackcombo == EndofWeakCombo)
                    weakattackcombo = 0;
            }
        }

        if (keys[(int)KeyArray.Strong] == keyState.KeyDown.ToChar())
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;

            if (Unit.Movement.isDash && !Unit.Movement.isJump)
            {
                Unit.Animator.PlayAnimation("DefanceBreak");
                // Unit.ColliderCtrl.AttackColliderActive(properties[4]);
                Debug.Log("방어깨기가 구현되어있지 않음");
            }
            else
            {
                strongattackcombo += 1;

                Unit.Animator.PlayAnimation("StrongAttack" + strongattackcombo.ToString());
                Unit.ColliderCtrl.AttackColliderActive(properties[5]);

                if (strongattackcombo == EndofStrongCombo)
                    strongattackcombo = 0;
            }
        }

        if (keys[(int)KeyArray.Skill1] == keyState.KeyDown.ToChar())
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;

            Unit.Animator.PlayAnimation("Skill1");
            Unit.ColliderCtrl.AttackColliderActive(properties[6]);

            //StartCoroutine(AccelerationDelay(new Vector2(0, 23), 0.2f));
        }

        if (keys[(int)KeyArray.SKill2] == keyState.KeyDown.ToChar())
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;

            Unit.Animator.PlayAnimation("Skill2");
            //Unit.ColliderCtrl.AttackColliderActive(properties[7]);

            Unit.Rigid.velocity = new Vector2(6f * (isRight ? 1 : -1), 18f);
        }

        if (keys[(int)KeyArray.special] == keyState.KeyDown.ToChar() && !Unit.Movement.isJump)
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;

            Unit.Animator.PlayAnimation("Special");
            Unit.ColliderCtrl.AttackColliderActive(properties[8]);
        }
        #endregion

        #region KeyUp
        //if (keys[(int)KeyArray.Weak] == keyState.KeyUp.ToChar())
        //{
        //}

        //if (keys[(int)KeyArray.Strong] == keyState.KeyUp.ToChar())
        //{
        //}

        //if (keys[(int)KeyArray.Skill1] == keyState.KeyUp.ToChar())
        //{
        //}

        //if (keys[(int)KeyArray.SKill2] == keyState.KeyUp.ToChar())
        //{
        //}

        //if (keys[(int)KeyArray.special] == keyState.KeyUp.ToChar())
        //{
        //}
        #endregion
    }

    #region Properties
    #endregion
}
