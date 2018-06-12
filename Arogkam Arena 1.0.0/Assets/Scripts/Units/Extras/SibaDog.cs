using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SibaDog : MonoBehaviour {

    public GameObject content;
    public float speed;
    public float term;
    public bool isShowing;
    SpriteRenderer SR;
    Animator Anitor;
	// Use this for initialization
	void Start () {
        SR = GetComponent<SpriteRenderer>();
        Anitor = GetComponent<Animator>();
        StartCoroutine("SibaCoroutine");
	}
	public IEnumerator SibaCoroutine()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(term);
           switch ( Random.Range(0, 4))
            {
                case 0: // 아래에서 위
                    SR.sortingOrder = -11;
                    content.transform.localPosition = new Vector2(Random.Range(0,20)-13, -1f);
                    //에니메이션 x디폴트
                    Anitor.SetTrigger("Up");
                    break;
                case 1: //위에서 아래
                    SR.sortingOrder = -9;
                    content.transform.localPosition = new Vector2(Random.Range(0, 32) - 16, -1f);
                    Anitor.SetTrigger("Down");
                    break;
                case 2: // 왼쪽에서 오른쪽
                    SR.sortingOrder = -9;
                    content.transform.localPosition = new Vector2(0,Random.Range(0,14)-7);
                    Anitor.SetTrigger("Right");
                    break;
                case 3:
                    SR.sortingOrder = -9;
                    content.transform.localPosition = new Vector2(0, Random.Range(0, 14) - 7);
                    Anitor.SetTrigger("Left");
                    break;
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
