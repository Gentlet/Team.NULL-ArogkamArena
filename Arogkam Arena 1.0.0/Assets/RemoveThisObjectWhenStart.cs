using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveThisObjectWhenStart : MonoBehaviour {

    private void Awake()
    {
        Debug.Log(SelectDataManager.instance);
        SelectDataManager.instance.SettingPlayersAndMap();
    }
    // Use this for initialization
    void Start () {
        Destroy(gameObject);
	}

}
