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
        if (Unit.Stuning)
            return;

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
            if (Unit.Movement.isJump)
            {
                if (!AttackSetting("JumpAttack"))
                    return;

                Unit.Animator.PlayAnimation("JumpAttack");
                Unit.ColliderCtrl.AttackColliderActive(GetProperty("JumpAttack"));

                StartCoroutine(CreateBullet("JumpAttack", new Vector2(-1f, -1f) * speed, new Vector2(-0.35f, 0.13f), 45f, 0.05f));

                StartCoroutine(AccelerationDelay(new Vector2(0,10f), 0f));
            }
            else if (Unit.Movement.isDash)
            {
                if (!AttackSetting("DashAttack"))
                    return;

                Unit.Animator.PlayAnimation("DashAttack");
                Unit.ColliderCtrl.AttackColliderActive(GetProperty("DashAttack"));

                Vector2 vel = Unit.Rigid.velocity;

                vel.x = 20f * (isRight ? 1 : -1);

                Unit.Rigid.velocity = vel;

                Unit.Movement.isDash = false;
            }
            else
            {
                if (!AttackSetting("WeakAttack" + (weakattackcombo + 1)))
                    return;

                weakattackcombo += 1;

                Unit.Animator.PlayAnimation("WeakAttack" + weakattackcombo.ToString());
                Unit.ColliderCtrl.AttackColliderActive(GetProperty("WeakAttack" + weakattackcombo.ToString()));

                if (weakattackcombo == EndofWeakCombo)
                    weakattackcombo = 0;
            }
        }

        if (keys[(int)KeyArray.Strong] == keyState.KeyDown.ToChar())
        {
            if (Unit.Movement.isDash && !Unit.Movement.isJump)
            {
                if (!AttackSetting("DefanceBreak"))
                    return;

                Unit.Animator.PlayAnimation("DefanceBreak");
                //Unit.ColliderCtrl.AttackColliderActive(properties[4]);
                Debug.Log("방어깨기가 구현되어있지 않음");
            }
            else
            {
                if (!AttackSetting("StrongAttack" + (strongattackcombo + 1)))
                    return;

                strongattackcombo += 1;

                Unit.Animator.PlayAnimation("StrongAttack" + strongattackcombo.ToString());
                Unit.ColliderCtrl.AttackColliderActive(GetProperty("StrongAttack" + strongattackcombo.ToString()));

                StartCoroutine(CreateBullet("StrongAttack" + strongattackcombo.ToString(), new Vector2(-1f, 0) * speed, new Vector2(-1.1f * (isRight ? -1f : 1f), 0.37f), 0, 0.1f));

                if (strongattackcombo == EndofStrongCombo)
                    strongattackcombo = 0;
            }
        }

        if (keys[(int)KeyArray.Skill1] == keyState.KeyDown.ToChar())
        {
            if (!AttackSetting("Skill1"))
                return;

            Unit.Animator.PlayAnimation("Skill1");
            Unit.ColliderCtrl.AttackColliderActive(GetProperty("Skill1"));

            //StartCoroutine(AccelerationDelay(new Vector2(0, 23), 0.2f));
        }

        if (keys[(int)KeyArray.SKill2] == keyState.KeyDown.ToChar())
        {
            if (!AttackSetting("Skill2"))
                return;

            Unit.Animator.PlayAnimation("Skill2");
            Unit.ColliderCtrl.AttackColliderActive(GetProperty("Skill2"));

            StartCoroutine(CreateBullet("Skill2", new Vector2(-1f, 0) * speed, new Vector2(-0.7f, 0.35f), 0, 0.02f));

            StartCoroutine(AccelerationDelay(new Vector2(15 * (isRight == true ? -1f : 1f), Unit.Rigid.velocity.y), 0f));
            
        }

        if (keys[(int)KeyArray.Special] == keyState.KeyDown.ToChar() && !Unit.Movement.isJump)
        {
            if (!AttackSetting("Special"))
                return;

            Unit.Animator.PlayAnimation("Special");
            Unit.ColliderCtrl.AttackColliderActive(GetProperty("Special"));
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

    private IEnumerator CreateBullet(string name, Vector2 direction, Vector2 pos,float rotation, float time)
    {
        yield return new WaitForSeconds(time);

        BulletAttackReport bullet = GameManager.Instance.CreateBullet(Unit);

        bullet.gameObject.name = name;

        if (isRight)
            direction.x *= -1f;

        bullet.Rigid.velocity = direction;
        bullet.transform.position = (Vector2)transform.position + pos;
        bullet.transform.rotation = Quaternion.Euler(bullet.transform.eulerAngles.x, (isRight == true ? 180f : 0f), rotation);
    }

    #region Properties
    #endregion
}
