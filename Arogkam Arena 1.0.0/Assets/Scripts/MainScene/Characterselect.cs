using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Characterselect : MonoBehaviour {

    public ChangeScene CS;
    public bool joypadkeyR;
    public bool joypadkeyL;

    public GameObject Pointer_1p;
    private int PointersCharacter1p;
    public GameObject Pointer_2p;
    private int PointersCharacter2p;

    public GameObject[] Characters;
    public Sprite[] CharactersImg;
    public GameObject[] EffectImg;
    public Sprite[] CharactersName;


    public Image Player1Img;
    public Image Player2Img;
    public Image Player1NameImg;
    public Image Player2NameImg;

    private bool Player1;
    private bool Player2;

    public BackGroundSelect Select;

    public bool Player1p
    {
        get
        {
            return Player1;
        }
    }
    public bool Player2p
    {
        get
        {
            return Player2;
        }
    }
    // Use this for initialization
    void Start() {
        PointersCharacter1p = 0;
        PointersCharacter2p = Characters.Length - 1;

        PointerStting(Pointer_1p, PointersCharacter1p, 1);
        PointerStting(Pointer_2p, PointersCharacter2p, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (CS.situation != 1)
            return;
        if (Player1 && Input.GetKeyDown(KeyCode.A) && PointersCharacter1p > 0) PointerStting(Pointer_1p, --PointersCharacter1p, 1);
        if (Player1 && Input.GetKeyDown(KeyCode.D) && PointersCharacter1p < Characters.Length - 1) PointerStting(Pointer_1p, ++PointersCharacter1p, 1);

        if (Player1 && Input.GetKeyDown(KeyCode.H)) CharacterChoiceEffect(Player1Img, PointersCharacter1p, 1);


        if (Player2 && (Input.GetKeyDown(KeyCode.LeftArrow) || isFirstDownJoyL()) && PointersCharacter2p > 0) PointerStting(Pointer_2p, --PointersCharacter2p, 2);
        if (Player2 && (Input.GetKeyDown(KeyCode.RightArrow) || isFirstDownJoyR()) && PointersCharacter2p < Characters.Length - 1) PointerStting(Pointer_2p, ++PointersCharacter2p, 2);

        if (Player2 && (Input.GetKeyDown(KeyCode.Keypad8)|| Input.GetKeyDown(KeyCode.JoystickButton0))) CharacterChoiceEffect(Player2Img, PointersCharacter2p, 2);

        if (Player1 == false && Player2 == false)
            CharacterSelectEnd();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CS.BackGame();
        }
    }

    bool isFirstDownJoyR()
    {
        if(Input.GetAxis("Horizontal") == 1 && joypadkeyR == false)
        {
            joypadkeyR = true;
            Debug.Log("Right");
            return true;
        }

        else if(Input.GetAxis("Horizontal") == 0)
        {
            joypadkeyR = false;

        }
        return false;
    }

    bool isFirstDownJoyL()
    {
        if (Input.GetAxis("Horizontal") == -1 && joypadkeyL == false)
        {
            joypadkeyL = true;
            Debug.Log("Left");
            return true;
        }

        else if (Input.GetAxis("Horizontal") == 0)
        {
            joypadkeyL = false;
        }
        return false;
    }

    void PointerStting(GameObject Pointer, int reSettingPoint, int player)
    {
        Pointer.transform.position = Characters[reSettingPoint].transform.position;
        if (player == 1)
        {
            Player1Img.sprite = CharactersImg[reSettingPoint];
            Player1NameImg.sprite = CharactersName[reSettingPoint];
        }
        else
        {
            Player2Img.sprite = CharactersImg[reSettingPoint];
            Player2NameImg.sprite = CharactersName[reSettingPoint];
        }

    }


    public void PointerReSet()
    {
        PointersCharacter1p = 0;
        PointersCharacter2p = Characters.Length - 1;

        PointerStting(Pointer_1p, PointersCharacter1p, 1);
        PointerStting(Pointer_2p, PointersCharacter2p, 2);
        StartChoice();
    }

    public void StartChoice()
    {
        SoundManager.instance.PlayChoice();
        Player1 = true;
        Player2 = true;
    }

    public void CharacterSelectEnd()
    {
     //   Debug.Log("");
        SoundManager.instance.PlayChoice();
        CS.LoadGame();
        Player1 = true;
        Player2 = true;
        SelectDataManager.instance.Player1Char = PointersCharacter1p;
        SelectDataManager.instance.Player2Char = PointersCharacter2p;
        Select.StartBackGroundChoice();
    }

    void CharacterChoiceEffect(Image img,int pointer,int player)
    {
        if (player == 1) Player1 = false;
        else Player2 = false;

        GameObject obj = Instantiate(EffectImg[pointer], img.transform);
        obj.SetActive(true);
        obj.transform.position = img.transform.position;
        obj.transform.rotation = img.transform.rotation;


        Debug.Log(Player1+ " : " + Player2);
        StartCoroutine(ImgBigger(obj, Player1 == Player2));
    }

    IEnumerator ImgBigger(GameObject obj, bool ChoiceEnd)
    {
        while (obj.transform.localScale.x <= 1.5f)
        {
            obj.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(obj);

        if (ChoiceEnd)
        {
            
            //GameManager.Instance.[0] = (Player.Names)PointersCharacter1p;
            //PlayerDataManager.Instance.PlayersName[1] = (Player.Names)PointersCharacter2p;
            //CS.LoadGame();
            //Select.StartBackGroundChoice();
        }
    }
}
