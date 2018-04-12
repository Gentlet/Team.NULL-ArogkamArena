using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimeManAttackObjects : MonoBehaviour {
    public string AttackType;
    public float time;

    private Unit unit;
    private Rigidbody2D rigid;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (AttackType == "Skill1" && unit.tag != collision.collider.tag)
        {
            //unit.

            GameManager.Instance.CreateAttackParticle("MimeMan", null, 1f).transform.position = transform.position + (Vector3.up * 1.25f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (AttackType == "Skill1" && unit.tag == collision.tag)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private IEnumerator ToyTrainTimer(float timer)
    {
        yield return new WaitForSeconds(timer);

        //unit.

        GameManager.Instance.CreateAttackParticle("MimeMan", null, 1f).transform.position = transform.position + (Vector3.up * 1.25f);
        Destroy(gameObject);
    }

    #region Properties
    public Unit Unit
    {
        get
        {
            return unit;
        }
        set
        {
            if (unit == null)
            {
                unit = value;
                gameObject.tag = unit.tag;
                rigid = GetComponent<Rigidbody2D>();

                if(AttackType == "Special")
                    StartCoroutine(ToyTrainTimer(time));
            }
        }
    }

    public Vector2 Velocity
    {
        get
        {
            return rigid.velocity;
        }
        set
        {
            rigid.velocity = value;
        }
    }
    #endregion
}
