using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackEffect
{
    [SerializeField]
    private string name;
    [SerializeField]
    private GameObject particle;

    #region Properties
    public string Name
    {
        get
        {
            return name;
        }
    }

    public GameObject Particle
    {
        get
        {
            return particle;
        }
    }
    #endregion
}

public class GameManager : SingletonGameObject<GameManager> {

    [SerializeField]
    private Vector4 stagesize;

    [SerializeField]
    private GameObject hitparticle;

    [SerializeField]
    private AttackEffect[] attackparticle;

    [SerializeField]
    private BulletAttackReport bulletprefab;

    [SerializeField]
    private GameObject bustmodparitcle;

    private Unit[] players;

    private List<AttackProperties[]> attackproperties;

    private void Awake()
    {
        attackproperties = new List<AttackProperties[]>();
    }
    public void SetPlayer(Unit unit)
    {
        if (players == null)
            players = new Unit[2];

        if (unit.CompareTag("Player1"))
            players[0] = unit;
        else if (unit.CompareTag("Player2"))
            players[1] = unit;
        else
            Debug.LogError("'unit's tag' data is not correct in GameManager's SetPlayer Function");
    }

    public Unit GetAnotherPlayer(string tag)
    {
        if(players == null)
        {
            Debug.LogError("'players' data is not defined in GameManager");
        }
        else if (tag == "Player1")
        {
            if (players[1] != null)
                return players[1];
            else
                Debug.LogError("'players[1]' data is not defined in GameManager");
        }
        else if (tag == "Player2")
        {
            if (players[0] != null)
                return players[0];
            else
                Debug.LogError("'players[0]' data is not defined in GameManager");
        }
        else
            Debug.LogError("tag is not correct in GameManager's GetAnotherPlayer Function");

        return null;
    }

    public void CreateHitParticle(Vector2 pos)
    {
        GameObject obj = Instantiate(hitparticle, transform);

        obj.transform.position = pos + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f));

        StartCoroutine(DestroyObj(obj, 0.5f));
    }

    public void AttackPropertiesSet(string tag, AttackProperties[] properties)
    {
        if (tag == "Player1")
        {
            attackproperties.Add(properties);
            return;
        }

        if (tag == "Player2")
        {
            attackproperties.Add(properties);
            return;
        }

        Debug.LogError(tag + "is not correct so attackproperties can not stored");
        return;
    }

    public AttackProperties GetAttackProperties(string tag, string name)
    {
        int num;

        if (tag == "Player1") num = 0;
        else if (tag == "Player2") num = 1;
        else
        {
            Debug.LogError(tag + "is not correct");
            return null;
        }

        for (int i = 0; i < attackproperties[num].Length; i++)
        {
            if(attackproperties[num][i].name == name)
            {
                return attackproperties[num][i];
            }
        }


        Debug.LogError(name + " is not correct in GetAttackProperties function");
        return null;
    }

    public BulletAttackReport CreateBullet(Unit unit)
    {
        BulletAttackReport bullet = Instantiate(bulletprefab);
        bullet.SetUnit(unit);

        return bullet;
    }
    
    #region public_GameObject_CreateAttackParticle
    public GameObject CreateAttackParticle(string name, float destroytime)
    {
        for (int i = 0; i < attackparticle.Length; i++)
        {
            if (attackparticle[i].Name == name)
            {
                GameObject particle = Instantiate(attackparticle[i].Particle);

                StartCoroutine(DestroyObj(particle.gameObject, destroytime));
                return particle;
            }
        }

        Debug.LogError(name + " is not correct in GameManager's CreateAttackParticle Function");
        return null;
    }
    public GameObject CreateAttackParticle(string name, GameObject parent, float destroytime)
    {
        for (int i = 0; i < attackparticle.Length; i++)
        {
            if (attackparticle[i].Name == name)
            {
                GameObject particle = Instantiate(attackparticle[i].Particle, parent.transform);

                StartCoroutine(DestroyObj(particle.gameObject, destroytime));
                return particle;
            }
        }

        Debug.LogError(name + " is not correct in GameManager's CreateAttackParticle Function");
        return null;
    }
    #endregion

    public GameObject CreateBustModParticle(Unit unit)
    {
        GameObject obj = Instantiate(bustmodparitcle, unit.transform);

        obj.tag = "BustMod";
        obj.transform.localPosition = new Vector3(0f, 0.5f, 0f);

        return obj;
    }

    public void RemoveBustModParticle(Unit unit)
    {
        for (int i = 0; i < unit.transform.childCount; i++)
        {
            if(unit.transform.GetChild(i).CompareTag("BustMod"))
            {
                StartCoroutine(DestroyObj(unit.transform.GetChild(i).gameObject, 0f));
            }
        }
    }

    private IEnumerator DestroyObj(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(obj); 
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 middle = new Vector2((StageSize.x + StageSize.z) / 2f, (StageSize.y + StageSize.w) / 2f);
        Vector2 size = new Vector2((StageSize.z - middle.x) * 2, (StageSize.w - middle.y) * 2);

        Gizmos.DrawWireCube(middle, size);
    }


    #region Properties

    public Vector4 StageSize
    {
        get
        {
            return stagesize;
        }
    }
#endregion
}
