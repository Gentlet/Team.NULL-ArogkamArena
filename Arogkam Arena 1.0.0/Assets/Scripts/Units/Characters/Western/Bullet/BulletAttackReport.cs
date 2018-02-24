using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackReport : MonoBehaviour
{
    private Unit unit;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (unit == null) return;

        //Debug.LogWarning("report before : " + collision.name);

        if (!collision.CompareTag(tag) && collision.name != "Foot" && collision.name != "Ground" && collision.name != "Walls")
        {
            //Debug.Log("report : " + collision.name);
            GameManager.Instance.GetAnotherPlayer(unit.tag).Hit(unit);
        }

        if (!collision.CompareTag(tag))
            Destroy(gameObject);
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
        transform.tag = unit.tag;
    }

    #region Properties

    public Rigidbody2D Rigid
    {
        get
        {
            return rigid;
        }

        set
        {
            rigid = value;
        }
    }

    #endregion
}
