using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyArray
{
    Up,
    Down,
    Right,
    Left,

    Weak,
    Strong,
    Skill1,
    SKill2,
    special,
}

public enum keyState
{
    None = 48,
    KeyDown,
    KeyStay,
    KeyUp,
}

public class KeyManager : SingletonGameObject<KeyManager> {

    private string player1;
    private string player2;

    private void Awake()
    {
        KeysReset();
    }

    private void Update()
    {
        KeysReset();

        TestKeyInput();
    }

    private void KeysReset()
    {
        player1 = "000000000";
        player2 = "000000000";
    }

    public string GetKeys(string tag)
    {

        if (tag == "Player1")
            return player1;

        else if (tag == "Player2")
            return player2;

        Debug.LogError("'" + tag + "' is not right tag");

        return null;
    }

    private void TestKeyInput()
    {
        #region KeyStay
        if (Input.GetKey(KeyCode.W))
        {
            player1 = player1.Change((int)KeyArray.Up, (int)keyState.KeyStay);
        }

        if (Input.GetKey(KeyCode.S))
        {
            player1 = player1.Change((int)KeyArray.Down, (int)keyState.KeyStay);
        }

        if (Input.GetKey(KeyCode.A))
        {
            player1 = player1.Change((int)KeyArray.Left, (int)keyState.KeyStay);
        }

        if (Input.GetKey(KeyCode.D))
        {
            player1 = player1.Change((int)KeyArray.Right, (int)keyState.KeyStay);
        }


        if (Input.GetKey(KeyCode.H))
        {
            player1 = player1.Change((int)KeyArray.Weak, (int)keyState.KeyStay);
        }

        if (Input.GetKey(KeyCode.J))
        {
            player1 = player1.Change((int)KeyArray.Strong, (int)keyState.KeyStay);
        }

        if (Input.GetKey(KeyCode.Y))
        {
            player1 = player1.Change((int)KeyArray.Skill1, (int)keyState.KeyStay);
        }

        if (Input.GetKey(KeyCode.U))
        {
            player1 = player1.Change((int)KeyArray.SKill2, (int)keyState.KeyStay);
        }

        if (Input.GetKey(KeyCode.I))
        {
            player1 = player1.Change((int)KeyArray.special, (int)keyState.KeyStay);
        }
        #endregion

        #region KeyDown

        if (Input.GetKeyDown(KeyCode.W))
        {
            player1 = player1.Change((int)KeyArray.Up, (int)keyState.KeyDown);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player1 = player1.Change((int)KeyArray.Down, (int)keyState.KeyDown);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            player1 = player1.Change((int)KeyArray.Left, (int)keyState.KeyDown);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            player1 = player1.Change((int)KeyArray.Right, (int)keyState.KeyDown);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            player1 = player1.Change((int)KeyArray.Weak, (int)keyState.KeyDown);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            player1 = player1.Change((int)KeyArray.Strong, (int)keyState.KeyDown);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            player1 = player1.Change((int)KeyArray.Skill1, (int)keyState.KeyDown);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            player1 = player1.Change((int)KeyArray.SKill2, (int)keyState.KeyDown);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            player1 = player1.Change((int)KeyArray.special, (int)keyState.KeyDown);
        }
        #endregion

        #region KeyUp

        if (Input.GetKeyUp(KeyCode.W))
        {
            player1 = player1.Change((int)KeyArray.Up, (int)keyState.KeyUp);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            player1 = player1.Change((int)KeyArray.Down, (int)keyState.KeyUp);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            player1 = player1.Change((int)KeyArray.Left, (int)keyState.KeyUp);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            player1 = player1.Change((int)KeyArray.Right, (int)keyState.KeyUp);
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            player1 = player1.Change((int)KeyArray.Weak, (int)keyState.KeyUp);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            player1 = player1.Change((int)KeyArray.Strong, (int)keyState.KeyUp);
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            player1 = player1.Change((int)KeyArray.Skill1, (int)keyState.KeyUp);
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            player1 = player1.Change((int)KeyArray.SKill2, (int)keyState.KeyUp);
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            player1 = player1.Change((int)KeyArray.special, (int)keyState.KeyUp);
        }
        #endregion

        DebugManager.Instance.listInit("KeyInput", player1);
    }

    #region Properties
#endregion
}
