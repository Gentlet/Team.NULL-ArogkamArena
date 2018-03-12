using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WesternAttack : UnitAttack
{
    [SerializeField]
    private float speed;

    private void Update()
    {
        Attack();

        DebugManager.Instance.listInit("IsAttacking", isAttacking.ToString());
        DebugManager.Instance.listInit("IsAttackDelay", isattackdelaying.ToString());
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

                StartCoroutine(CreateBullet(new Vector2(-1f, -1f) * speed, new Vector2(-0.35f, 0.13f), 45f, 0.05f));

                StartCoroutine(AccelerationDelay(new Vector2(0,10f), 0f));
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
                //Unit.ColliderCtrl.AttackColliderActive(properties[4]);
                Debug.Log("방어깨기가 구현되어있지 않음");
            }
            else
            {
                strongattackcombo += 1;

                Unit.Animator.PlayAnimation("StrongAttack" + strongattackcombo.ToString());
                Unit.ColliderCtrl.AttackColliderActive(properties[5]);

                StartCoroutine(CreateBullet(new Vector2(-1f, 0) * speed, new Vector2(-1.1f * (isRight ? -1f : 1f), 0.37f), 0, 0.1f));

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
            Unit.ColliderCtrl.AttackColliderActive(properties[7]);

            StartCoroutine(CreateBullet(new Vector2(-1f, 0) * speed, new Vector2(-0.7f, 0.35f), 0, 0.02f));

            StartCoroutine(AccelerationDelay(new Vector2(15 * (isRight == true ? -1f : 1f), Unit.Rigid.velocity.y), 0f));
            
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

    private IEnumerator CreateBullet(Vector2 direction, Vector2 pos,float rotation, float time)
    {
        yield return new WaitForSeconds(time);

        BulletAttackReport bullet = GameManager.Instance.CreateBullet(Unit);

        if (isRight)
            direction.x *= -1f;

        bullet.Rigid.velocity = direction;
        bullet.transform.position = (Vector2)transform.position + pos;
        bullet.transform.rotation = Quaternion.Euler(bullet.transform.eulerAngles.x, (isRight == true ? 180f : 0f), rotation);
    }

    #region Properties
    #endregion
}
