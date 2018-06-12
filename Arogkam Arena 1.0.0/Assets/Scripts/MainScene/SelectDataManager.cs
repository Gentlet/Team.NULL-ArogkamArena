using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDataManager : MonoBehaviour {

    public int Player1Char;
    public int Player2Char;
    public int Map;
    public static SelectDataManager instance = null;
    public GameObject[] Characters;
    public GameObject[] Maps;
    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync("",LoadSceneMode.)
    }
    
}
