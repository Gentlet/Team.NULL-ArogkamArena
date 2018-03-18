using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitProperties
{
    public Vector2 knockback;
    public float hitdelay;
    public bool stun;
    public float stuntime;
}

[System.Serializable]
public class AttackProperties
{
    public string name;
    public float fdelay;
    public float delay;
    public float during;
    public int times;
    public float damage;

    public float stamina;

    public HitProperties hitProperties;
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

    [SerializeField]
    protected bool isattacking;
    [SerializeField]
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
            if (!AttackSetting("WeakAttack" + weakattackcombo))
                return;
        }

        if (keys[(int)KeyArray.Strong] == keyState.KeyDown.ToChar())
        {
            if (!AttackSetting("StrongAttack" + strongattackcombo))
                return;
        }

        if (keys[(int)KeyArray.Skill1] == keyState.KeyDown.ToChar())
        {
            if (!AttackSetting("Skill1"))
                return;
        }

        if (keys[(int)KeyArray.SKill2] == keyState.KeyDown.ToChar())
        {
            if (!AttackSetting("Skill2"))
                return;
        }

        if (keys[(int)KeyArray.Special] == keyState.KeyDown.ToChar())
        {
            if (!AttackSetting("Special"))
                return;
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

        if (keys[(int)KeyArray.Special] == keyState.KeyUp.ToChar())
        {
        }
        #endregion
    }

    protected AttackProperties GetAttackpropertie(string name)
    {
        for (int i = 0; i < properties.Length; i++)
        {
            if (properties[i].name == name)
                return properties[i];
        }

        return null;
    }

    protected bool AttackSetting(string attackname)
    {
        if(0 <= Unit.Status.stamina - GetAttackpropertie(attackname).stamina)
        {
            isattacking = true;
            isattackdelaying = true;
            Unit.State = UnitState.Attack;

            Unit.UseStamina(GetAttackpropertie(attackname).stamina);

            return true;
        }

        return false;
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

    public bool isRight
    {
        get
        {
            return (Unit.transform.rotation.eulerAngles.y == 180 ? true : false);
        }
    }
#endregion
}
