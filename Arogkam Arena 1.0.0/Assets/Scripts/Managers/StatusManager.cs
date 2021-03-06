﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Status
{
    [SerializeField]
    private string name;
    [HideInInspector]
    public float hp;
    [HideInInspector]
    public float stamina;

    public float speed;

    public float jump_force;

    [HideInInspector]
    public int bustmodstate;

    #region Properties
    public string Name
    {
        get
        {
            if (name == "")
                name = "not defined";

            return name;
        }
    }
#endregion
}

public class StatusManager : SingletonGameObject<StatusManager> {

    [SerializeField]
    private Status[] status;

    public Status GetStatus(string name)
    {

        for (int i = 0; i < status.Length; i++)
        {
            if (status[i].Name == name)
            {
                return status[i];
            }
        }

        Debug.LogError("'" + name + "' Status is not defined or can not found");

        return new Status();
    }
}
