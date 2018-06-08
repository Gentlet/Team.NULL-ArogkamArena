using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    public int SceneNumber;
    public GameObject GO;
    RectTransform RT;
    public Characterselect CS;
    public int situation; // 0 메인 화면
                          // 1 캐릭터 선택
                          // 2 맵 선택
                          // 3 옵션
    public void LoadGame()
    {
        if (situation >= 2)
            return;
        if (situation == 1)
            if (CS.Player1p || CS.Player2p)
                return;
        situation += 1;
        GO.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50000, 0));
        Invoke("StopMove", 1f);
        

    }
    private void Start()
    {
        if(GO != null)
         RT = GO.GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        if (GO == null)
            return;
     if(GO.transform.localPosition.x > 0 && situation == 0)
        {
            GO.transform.localPosition = new Vector3();
        }
     else if(RT.localPosition.x < -2292.982 && situation == 1)
        {
            RT.localPosition = new Vector3(x: -2292.982f, y: 0, z: 0);
        }
     else if(RT.localPosition.x > -2292.982 && situation == 1)
        {
            GO.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 0));
        }
        //  -2292.982
    }
    public void BackGame()
    {
        if (situation <= 0)
            return;
        GO.GetComponent<Rigidbody2D>().AddForce(new Vector2(50000, 0));
        Invoke("StopMove", 1f);
        situation -= 1;
    }

    public void StopMove()
    {
       // GO.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void ChangeScene2Setting()
    {
        SceneManager.LoadScene("SettingScene");
    }
    
    public void ChangeScene2Main()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && situation ==3)
        {
            ChangeScene2Main();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
         //   BackGame();
        }
    }
}
