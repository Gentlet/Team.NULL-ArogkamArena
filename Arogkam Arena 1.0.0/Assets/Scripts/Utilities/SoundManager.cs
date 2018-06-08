using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public bool iseffOn;
    public AudioSource LoadingBGM;
    public AudioSource MainSceneBGM;
    public AudioSource GameSceneBGM;
    public AudioSource AranaCrowd;
    public AudioSource Fight;
    public AudioSource ButtonClick;
    public AudioSource Back;
    public AudioSource gun1;
    public AudioSource sword1;
    public AudioSource sword2;
    public AudioSource choice;
    public AudioSource hit1;
    public AudioSource hit2;



    public static SoundManager instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void PlayGameSceneBGM()
    {
        GameSceneBGM.Play();
    }
    public void pauseGameSceneBGM()
    {
        GameSceneBGM.Pause();
    }
    public void PlayGun()
    {
        gun1.Play();
    }
    public void PlaySword1()
    {
        sword1.Play();
    }
    public void PlaySword2()
    {
        sword2.Play();
    }
    public void PlayChoice()
    {
        choice.Play();
    }
    public void PlayHit1()
    {
        hit1.Play();
    }
    public void PlayHit2()
    {
        hit2.Play();
    }
    public void PlayLoadingBGM()
    {
        LoadingBGM.Play();
    }


    public void PlayAranaCrowd()
    {
        AranaCrowd.Play();
    }
    public void PlayFight()
    {
        if (iseffOn)
            Fight.Play();
    }
    public void PauseBGM()
    {
        if (MainSceneBGM.isPlaying)
            MainSceneBGM.Pause();
        if (GameSceneBGM.isPlaying)
            GameSceneBGM.Pause();
    }

    public void PlayMainBGM()
    {
        MainSceneBGM.Play();
    }
    public void PlayButtonClick()
    {
        if (iseffOn)
            ButtonClick.Play();
    }
    public void PlayBack()
    {
        if(iseffOn)
        Back.Play();
    }

    // Use this for initialization
    void Start () {
        iseffOn = true;
        MainSceneBGM.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
