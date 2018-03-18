using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingletonGameObject<CameraManager>
{

    [SerializeField]
    private Camera maincamera;

    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private Vector2 smoothTime;
    [SerializeField]
    private bool activate;

    private Unit player1;
    private Unit player2;

    private Vector2 velocity;
    private Vector2 camerapos;

    private void Start()
    {
        player1 = GameManager.Instance.GetAnotherPlayer("Player2");
        player2 = GameManager.Instance.GetAnotherPlayer("Player1");
    }

    private void Update()
    {
        if (-8.8f <= (player1.transform.position.x + player2.transform.position.x) / 2f && (player1.transform.position.x + player2.transform.position.x) / 2f <= 8.8f)
            maincamera.transform.position = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2f, maincamera.transform.position.y, maincamera.transform.position.z);

        float distance = Vector2.Distance(player1.transform.position, player2.transform.position);

        if ((10f / 30f) * distance <= 10f && (10f / 30f) * distance >= 5f)
            maincamera.orthographicSize = (10f / 30f) * distance;


        //camerapos = (player1.transform.position + player2.transform.position) / 2f;
        //camerapos.y = 0;

        //float distance = Vector2.Distance(player1.transform.position, player2.transform.position);

        //if ((Mathf.Abs(maincamera.transform.position.x) + Mathf.Abs((((maincamera.orthographicSize * 2f) / 9f) * 8f))) <= 19f)
        //{
        //    maincamera.orthographicSize = ((distance / 14f) < 1f ? 1f : (distance / 14f)) * 5f;



        //    Debug.Log((Mathf.Abs(maincamera.transform.position.x) + Mathf.Abs((((maincamera.orthographicSize * 2f) / 9f) * 8f))));

        //    if (activate)
        //    {
        //        float posX = Mathf.SmoothDamp(maincamera.transform.position.x, camerapos.x + offset.x, ref velocity.x, smoothTime.x);
        //        float posY = Mathf.SmoothDamp(maincamera.transform.position.y, camerapos.y + offset.y, ref velocity.y, smoothTime.y);
        //        Camera.main.transform.position = new Vector3(posX, posY, Camera.main.transform.position.z);
        //    }
        //}
    }

    #region Properties
    public bool Activate
    {
        get
        {
            return activate;
        }
        set
        {
            activate = value;
        }
    }
    #endregion
}
