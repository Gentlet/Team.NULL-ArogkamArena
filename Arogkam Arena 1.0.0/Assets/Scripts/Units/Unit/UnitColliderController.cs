using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitColliders
{
    [SerializeField]
    private string name;
    [SerializeField]
    private Collider2D[] colliders;

    #region Properties
    public string Name
    {
        get
        {
            return name;
        }
    }

    public Collider2D[] Colliders
    {
        get
        {
            return colliders;
        }
    }

    public bool CollidersActive
    {
        get
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].enabled)
                    return false;
            }

            return true;
        }
        set
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = value;
            }
        }
    }

    public bool TriggerColliderActive
    {
        get
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].isTrigger && !colliders[i].enabled)
                    return false;
            }

            return true;
        }
        set
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].isTrigger)
                    colliders[i].enabled = value;
            }
        }
    }

    public bool NotTriggerColliderActive
    {
        get
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].isTrigger && !colliders[i].enabled)
                    return false;
            }

            return true;
        }
        set
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].isTrigger)
                    colliders[i].enabled = value;
            }
        }
    }
#endregion
}

public class UnitColliderController : ChildUnitInterface
{
    [SerializeField]
    private UnitColliders[] colliders;

    private string activecollidername;

    private bool isactive;

    private void Awake()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].CollidersActive = false;
        }
    }


    public  void MovemetColliderActive(string name)
    {
        AllColliderActiveFalse();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].Name == name)
            {
                activecollidername = name;
                colliders[i].CollidersActive = true;
                return;
            }
        }

        Debug.LogError("Collider is not Actived (collider is not founded)");
    }

    public void AttackColliderActive(AttackProperties proerties)
    {
        if (isactive)
        {
            Debug.LogError("Collider is already Actived");
            return;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].Name == proerties.name)
            {
                activecollidername = proerties.name;
                StartCoroutine(ColliderActive(colliders[i], proerties.fdelay, proerties.delay, proerties.during, proerties.times, proerties.damage));
                return;
            }
        }

        Debug.LogError("Collider is not Actived (collider is not founded)");
    }

    private IEnumerator ColliderActive(UnitColliders collider, float fdelay, float delay, float during, int times, float damage)
    {
        isactive = true;

        AllColliderActiveFalse();

        collider.NotTriggerColliderActive = true;

        yield return new WaitForSeconds(fdelay);

        for (int i = 0; i < times; i++)
        {
            collider.TriggerColliderActive = true;


            yield return new WaitForSeconds(during);

            collider.TriggerColliderActive = false;

            yield return new WaitForSeconds(delay);
        }

        Unit.Attack.AttackDelay();

        isactive = false;
    }

    private void AllColliderActiveFalse()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].CollidersActive = false;
        }
    }

    private void AllNotTriggerColliderActiveFalse()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].NotTriggerColliderActive = false;
        }
    }

    #region Properties

    public string ActiveColliderName
    {
        get
        {
            return activecollidername;
        }
    }

    public bool isActive
    {
        get
        {
            return isactive;
        }
    }
    #endregion
}
