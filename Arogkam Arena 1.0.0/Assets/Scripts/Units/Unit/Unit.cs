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

public abstract class Unit : MonoBehaviour {
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

    protected void Awake () {
        GameManager.Instance.SetPlayer(this);

        rigid = GetComponent<Rigidbody2D>();

        shadow = transform.GetChild(0).gameObject;

        status = StatusManager.Instance.GetStatus(UnitName);
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
        while(true)
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

    public virtual void Hit(Unit unit)
    {
        if (!(unit.CompareTag("Wall") || unit.CompareTag("Ground")))
        {
            //Debug.Log("hit : " + unit.ColliderCtrl.ActiveColliderName);

            AttackProperties property = GameManager.Instance.GetAttackProperties(unit.tag, unit.ColliderCtrl.ActiveColliderName);

            status.hp -= property.damage;

            GameManager.Instance.CreateHitParticle(transform.position);
        }
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
