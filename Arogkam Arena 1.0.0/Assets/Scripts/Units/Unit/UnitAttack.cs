using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackProperties
{
    public string name;
    public float fdelay;
    public float delay;
    public float during;
    public int times;
    public float damage;
}

public abstract class UnitAttack : ChildUnitInterface
{
    private float attackdelay = 0.25f;

    [SerializeField]
    protected int EndofWeakCombo;
    [SerializeField]
    protected int EndofStrongCombo;

    [SerializeField]
    protected AttackProperties[] properties;

    protected int weakattackcombo;
    protected int strongattackcombo;

    protected bool isattacking;
    protected bool isattackdelaying;

    private void Start()
    {
        GameManager.Instance.AttackPropertiesSet(Unit.tag, properties);
    }

    void Update () {
        Attack();
	}
    

    protected virtual void Attack()
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
        }

        if (keys[(int)KeyArray.Strong] == keyState.KeyDown.ToChar())
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;
        }

        if (keys[(int)KeyArray.Skill1] == keyState.KeyDown.ToChar())
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;
        }

        if (keys[(int)KeyArray.SKill2] == keyState.KeyDown.ToChar())
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;
        }

        if (keys[(int)KeyArray.special] == keyState.KeyDown.ToChar())
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;
        }
        #endregion

        #region KeyUp
        if (keys[(int)KeyArray.Weak] == keyState.KeyUp.ToChar())
        {
        }

        if (keys[(int)KeyArray.Strong] == keyState.KeyUp.ToChar())
        {
        }

        if (keys[(int)KeyArray.Skill1] == keyState.KeyUp.ToChar())
        {
        }

        if (keys[(int)KeyArray.SKill2] == keyState.KeyUp.ToChar())
        {
        }

        if (keys[(int)KeyArray.special] == keyState.KeyUp.ToChar())
        {
        }
        #endregion
    }

    protected IEnumerator AccelerationDelay(Vector2 vel, float delay)
    {
        yield return new WaitForSeconds(delay);

        Unit.Rigid.velocity = vel;
    }

    protected virtual IEnumerator ComboContinueDelay()
    {
        int weaktmp = weakattackcombo;
        int strongtmp = strongattackcombo;

        yield return new WaitForSeconds(0.3f + attackdelay);

        if(weaktmp == weakattackcombo)
            weakattackcombo = 0;

        if (strongtmp == strongattackcombo)
            strongattackcombo = 0;
    }
    
    public virtual void AttackAnimationIsEnd()
    {
        isattacking = false;

        if (0 < weakattackcombo + strongattackcombo)
        {
            StopCoroutine(ComboContinueDelay());
            StartCoroutine(ComboContinueDelay());
        }
    }

    public void AttackDelay()
    {
        StartCoroutine(AttackDelaying());
    }

    private IEnumerator AttackDelaying()
    {
        yield return new WaitForSeconds(attackdelay);

        isattackdelaying = false;
    }

    #region Properties

    public bool isAttacking
    {
        get
        {
            return isattacking;
        }
    }
#endregion
}
