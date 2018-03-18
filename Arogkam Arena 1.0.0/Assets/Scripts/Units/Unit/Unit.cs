using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    Idle,
    Jump,
    Defance,
    Right,
    Left,

    RightDash,
    LeftDash,

    Attack,

    Hit,
    Stun,

    Down,
}

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected string unitname;

    protected GameObject shadow;

    protected Rigidbody2D rigid;

    protected Status status;
    protected UnitState state;

    protected UnitAnimator animator;
    protected UnitAttack attack;
    protected UnitMovement movement;
    protected UnitColliderController colliderctrl;

    protected bool stuning;
    protected bool hitdelay;

    private Coroutine chargedelay;
    private Coroutine charge;

    protected void Awake()
    {
        GameManager.Instance.SetPlayer(this);

        rigid = GetComponent<Rigidbody2D>();

        shadow = transform.GetChild(0).gameObject;

        status = StatusManager.Instance.GetStatus(UnitName);
        status.hp = 100f;
        status.stamina = 100f;


        state = UnitState.Idle;

        ChildSetting(transform);

        //하위 컴포넌트 초기화
        {
            animator = GetComponent<UnitAnimator>();
            attack = GetComponent<UnitAttack>();
            movement = GetComponent<UnitMovement>();
            colliderctrl = GetComponent<UnitColliderController>();

            animator.ConnectUnit(this);
            attack.ConnectUnit(this);
            movement.ConnectUnit(this);
            colliderctrl.ConnectUnit(this);
        }

        shadow.transform.parent = null;
        StartCoroutine(Shadow());
    }

    private IEnumerator Shadow()
    {
        while (true)
        {
            shadow.transform.position = new Vector2(transform.position.x, -3.722509f);

            yield return new WaitForEndOfFrame();
        }
    }

    private void ChildSetting(Transform tmp)
    {
        if (tmp.CompareTag("Shadow"))
            return;

        for (int i = 0; i < tmp.childCount; i++)
        {
            ChildSetting(tmp.GetChild(i));
        }

        tmp.tag = transform.tag;
    }

    private void BustMod(bool state)
    {
        if (state && status.bustmodstate != 1)
        {
            status.bustmodstate = 2;
            GameManager.Instance.CreateBustModParticle(this);
        }
        else if (!state && status.bustmodstate == 2)
        {
            status.bustmodstate = 1;
            GameManager.Instance.RemoveBustModParticle(this);
        }
    }

    private IEnumerator StunDelay(float time)
    {
        stuning = true;
        Animator.PlayAnimation("Stun");
        GameObject particle = GameManager.Instance.CreateSunParticle(this);

        yield return new WaitForSeconds(time);

        stuning = false;
        Destroy(particle);
    }

    private IEnumerator HitDelay(float time)
    {
        hitdelay = true;
        Animator.PlayAnimation("Hit");

        yield return new WaitForSeconds(time);

        hitdelay = false;
    }

    public void UseStamina(float value)
    {
        status.stamina -= value;
        GameManager.Instance.SetFillAmount(transform.tag + "Stamina", status.stamina / 100f);

        if (chargedelay != null)
            StopCoroutine(chargedelay);
        if (charge != null)
            StopCoroutine(charge);

        chargedelay = StartCoroutine(ChargeStainadelay());
    }

    private IEnumerator ChargeStainadelay()
    {
        yield return new WaitForSeconds(0.7f);

        charge = StartCoroutine(ChargeStaina());
    }

    private IEnumerator ChargeStaina()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            status.stamina += 0.4f;
            GameManager.Instance.SetFillAmount(transform.tag + "Stamina", status.stamina / 100f);

            if (100f <= status.stamina)
                break;
        }
    }

    public virtual void Hit(Unit unit)
    {
        AttackProperties property = GameManager.Instance.GetAttackProperties(unit.tag, unit.ColliderCtrl.ActiveColliderName);

        Hit(unit, property);
    }

    public virtual void BulletHit(Unit unit, BulletAttackReport bullet)
    {
        AttackProperties property = GameManager.Instance.GetAttackProperties(unit.tag, bullet.name);

        Hit(unit, property);
    }

    protected virtual void Hit(Unit unit, AttackProperties property)
    {
        status.hp -= property.damage + (property.damage * (unit.status.bustmodstate == 2 ? 0.1f : 0f));

        GameManager.Instance.SetFillAmount(transform.tag + "Hp", status.hp / 100f);

        if (status.hp < 30f)
            BustMod(true);

        GameManager.Instance.CreateHitParticle(transform.position);

        Vector2 knockback = property.hitProperties.knockback;

        knockback.x = knockback.x * (Attack.isRight ? -1f : 1f);

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, (unit.Attack.isRight ? 0f : 180f), transform.eulerAngles.z);

        Rigid.velocity = knockback;


        if (property.hitProperties.stun)
            StartCoroutine(StunDelay(property.hitProperties.stuntime));
        else
            StartCoroutine(HitDelay(property.hitProperties.hitdelay));
    }

    #region Properties
    public string UnitName
    {
        get
        {
            return unitname;
        }
    }

    public Rigidbody2D Rigid
    {
        get
        {
            return rigid;
        }
    }

    public Status Status
    {
        get
        {
            return status;
        }
    }

    public UnitState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }

    public UnitAnimator Animator
    {
        get
        {
            return animator;
        }
    }

    public UnitAttack Attack
    {
        get
        {
            return attack;
        }
    }

    public UnitMovement Movement
    {
        get
        {
            return movement;
        }
    }

    public UnitColliderController ColliderCtrl
    {
        get
        {
            return colliderctrl;
        }
    }

    public bool Stuning
    {
        get
        {
            return stuning || hitdelay;
        }
    }

    public T GetAttack<T>() where T : UnitAttack
    {
        return attack as T;
    }

    public T GetMovement<T>() where T : UnitMovement
    {
        return movement as T;
    }

    #endregion
}
