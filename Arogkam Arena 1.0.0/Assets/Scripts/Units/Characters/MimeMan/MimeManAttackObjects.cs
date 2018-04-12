using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimeManAttackObjects : MonoBehaviour {
    public string AttackType;
    public float time;
    public float speed;

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

    private void Update()
    {
    }

    private IEnumerator ToyTrainTimer(float timer)
    {
        yield return new WaitForSeconds(timer);

        //unit.

        GameManager.Instance.CreateAttackParticle("MimeMan", null, 1f).transform.position = transform.position + (Vector3.up * 1.25f);
        Destroy(gameObject);
    }

    private IEnumerator ToyTrainMover()
    {
        while(true)
        {
            yield return new WaitForFixedUpdate();

            Vector3 apos = GameManager.Instance.GetAnotherPlayer(unit.tag).transform.position;

            apos.y = transform.position.y;
            apos.z = transform.position.z;

            apos = Vector3.MoveTowards(transform.position, apos, speed * Time.deltaTime);

            if (0f < (apos.x - transform.position.x) * 100f) 
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);

            transform.position = apos;
        }
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

                if (AttackType == "Special")
                {
                    StartCoroutine(ToyTrainMover());
                    StartCoroutine(ToyTrainTimer(time));
                }
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
