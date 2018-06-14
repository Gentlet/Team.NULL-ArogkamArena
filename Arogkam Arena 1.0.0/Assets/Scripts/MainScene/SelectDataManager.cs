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
    public Animator fadeanitor;
    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        Player1Char = 0;
        Player2Char = Characters.Length-1;
    }
    public void MoveToGameScene()
    {
        fadeanitor.SetTrigger("Fade");
        Invoke("DoThing", 1.5f);
        
    }
    void DoThing()
    {
        StartCoroutine(GameScene());
    }
    public void SettingPlayersAndMap()
    {
        Instantiate(Maps[Map]);
        GameObject GO1 = Instantiate(Characters[Player1Char],new Vector3(-7,-1.93f,0), new Quaternion());
        GO1.tag = "Player1";
        GameObject GO2 = Instantiate(Characters[Player2Char], new Vector3(7,-2.5f,0), new Quaternion());
        GO2.tag = "Player2";
    }
    
    IEnumerator GameScene()
    {
      AsyncOperation AP =  SceneManager.LoadSceneAsync("GameScene");
        while(!AP.isDone)
        {
            yield return null;
        }
    }
}
