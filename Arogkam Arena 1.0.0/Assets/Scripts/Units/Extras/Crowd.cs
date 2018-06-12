using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {
    Vector3 OriginPos;
    public float DelayTime;
    public float UpPower;
	// Use this for initialization
	void Start () {
        OriginPos = transform.position;
       // Invoke("nothing", Random.Range(0.00001f, DelayTime));
        Invoke("StartUpDown", Random.Range(0.00001f, DelayTime));
	}
    public void StartUpDown()
    {
        StartCoroutine("UpDown");
    }
    public void nothing()
    { }
    IEnumerator UpDown()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(DelayTime + Random.Range(0f,0.2f));
            transform.position = OriginPos + new Vector3(0, UpPower, 0);

            yield return new WaitForSecondsRealtime(DelayTime);
            transform.position = OriginPos;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
