using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공격하면 공격당한놈한티 공격당했다고 말해주는 넘
//attacktrigger에 넣어주면됨

public class UnitAttackReport : MonoBehaviour {
    private Unit unit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogWarning("report before : " + collision.name);

        if (!collision.CompareTag(tag) && collision.name != "Foot" && collision.name != "Ground") 
        {
            //Debug.Log("report : " + collision.name);
            GameManager.Instance.GetAnotherPlayer(unit.tag).Hit(unit);
        }
    }

    private void Awake()
    {
        Transform tmp = transform;
        while(true)
        {
            if (tmp.parent == null)
            {
                unit = tmp.GetComponent<Unit>();
                break;
            }

            tmp = tmp.parent;
        }
    }

    #region Properties

    #endregion
}
