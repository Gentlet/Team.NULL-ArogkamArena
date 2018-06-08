using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class BackGroundSelect : MonoBehaviour {
    public GameObject[] Maps;
    public Sprite[] Mapimg;
    public Sprite[] Names;
    public Image MapName;
    public GameObject Pointer;
    public ChangeScene CS;
    public Characterselect charsel;
    private int PointNum;

    public Image BackGroundImg;

    private bool CanChoice;

	// Use this for initialization
	void Start () {
        CanChoice = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (CanChoice == true && PointNum > 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))) PointerSet(-1);
        if (CanChoice == true && PointNum < Maps.Length - 1 && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))) PointerSet(1);

        if (CanChoice == true && Input.GetKeyDown(KeyCode.Space)) ChoiceEnd();
        if (CanChoice == true && Input.GetKeyDown(KeyCode.Escape))
        {
            CanChoice = false;
            charsel.PointerReSet();
          //  CS.BackGame();
        }

    }

    public void StartBackGroundChoice()
    {
        CanChoice = true;
    }

    void PointerSet(int num)
    {
        PointNum += num;
        SoundManager.instance.PlayChoice();
        MapName.sprite = Names[PointNum];
        Pointer.transform.position = new Vector2(Maps[PointNum].transform.position.x, Maps[PointNum].transform.position.y + 10);

        BackGroundImg.sprite = Mapimg[PointNum];
    }

    void ChoiceEnd()
    {
        CanChoice = false;
        SceneManager.LoadScene("GameScene");
    }
}
