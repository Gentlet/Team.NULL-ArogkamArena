using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingletonGameObject<CameraManager>
{
    [SerializeField]
    private Camera maincamera;

    public GameObject Player1;
    public GameObject Player2;

    void Start()
    {
        Player1 = GameManager.Instance.GetAnotherPlayer("Player2").gameObject;
        Player2 = GameManager.Instance.GetAnotherPlayer("Player1").gameObject;
    }

    void Update()
    {
        SetCamera();
    }

    void SetCamera()
    {
        if (-9f <= (Player1.transform.position.x + Player2.transform.position.x) / 2f && (Player1.transform.position.x + Player2.transform.position.x) / 2f <= 9f)
            maincamera.transform.position = new Vector3((Player1.transform.position.x + Player2.transform.position.x) / 2f, maincamera.transform.position.y, maincamera.transform.position.z);

        float distance = Vector2.Distance(Player1.transform.position, Player2.transform.position);

        if (0.33f * distance <= 10f && 0.33f * distance >= 5f)
            maincamera.orthographicSize = 0.33f * distance;
    }

    //[SerializeField]
    //private Camera maincamera;

    //[SerializeField]
    //private Vector2 offset;
    //[SerializeField]
    //private Vector2 smoothTime;
    //[SerializeField]
    //private bool activate;

    //private Unit player1;
    //private Unit player2;

    //private Vector2 velocity;
    //private Vector3 camerapos;

    //private void Start()
    //{
    //    player1 = GameManager.Instance.GetAnotherPlayer("Player2");
    //    player2 = GameManager.Instance.GetAnotherPlayer("Player1");
    //}

    //private void Update()
    //{
    //    if (-8.8f <= (player1.transform.position.x + player2.transform.position.x) / 2f && (player1.transform.position.x + player2.transform.position.x) / 2f <= 8.8f)
    //        camerapos = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2f, maincamera.transform.position.y, maincamera.transform.position.z);

    //    float distance = Vector2.Distance(player1.transform.position, player2.transform.position);

    //    if (0.4f * distance <= 10f && 0.4f * distance >= 5f)
    //    {
    //        if (19f <= Mathf.Abs(maincamera.transform.position.x) + (((0.4f * distance) / 4.5f) * 8f))
    //        {
    //            Debug.Log(19f - (Mathf.Abs(maincamera.transform.position.x) + (((0.4f * distance) / 4.5f) * 8f)));

    //            camerapos = new Vector3(19f - (Mathf.Abs(maincamera.transform.position.x) + (((0.4f * distance) / 4.5f) * 8f)), maincamera.transform.position.y, -10);
    //        }

    //        maincamera.orthographicSize = 0.4f * distance;
    //    }

    //    if (activate)
    //    {
    //        float posX = Mathf.SmoothDamp(maincamera.transform.position.x, camerapos.x + offset.x, ref velocity.x, smoothTime.x);
    //        float posY = Mathf.SmoothDamp(maincamera.transform.position.y, camerapos.y + offset.y, ref velocity.y, smoothTime.y);
    //        Camera.main.transform.position = new Vector3(posX, posY, Camera.main.transform.position.z);
    //    }


    //    //camerapos = (player1.transform.position + player2.transform.position) / 2f;
    //    //camerapos.y = 0;

    //    //float distance = Vector2.Distance(player1.transform.position, player2.transform.position);

    //    //if ((Mathf.Abs(maincamera.transform.position.x) + Mathf.Abs((((maincamera.orthographicSize * 2f) / 9f) * 8f))) <= 19f)
    //    //{
    //    //    maincamera.orthographicSize = ((distance / 14f) < 1f ? 1f : (distance / 14f)) * 5f;

    //    //    Debug.Log((Mathf.Abs(maincamera.transform.position.x) + Mathf.Abs((((maincamera.orthographicSize * 2f) / 9f) * 8f))));

    //    //    if (activate)
    //    //    {
    //    //        float posX = Mathf.SmoothDamp(maincamera.transform.position.x, camerapos.x + offset.x, ref velocity.x, smoothTime.x);
    //    //        float posY = Mathf.SmoothDamp(maincamera.transform.position.y, camerapos.y + offset.y, ref velocity.y, smoothTime.y);
    //    //        Camera.main.transform.position = new Vector3(posX, posY, Camera.main.transform.position.z);
    //    //    }
    //    //}
    //}

    //#region Properties
    //public bool Activate
    //{
    //    get
    //    {
    //        return activate;
    //    }
    //    set
    //    {
    //        activate = value;
    //    }
    //}
    //#endregion
}
