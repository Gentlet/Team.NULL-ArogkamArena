using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KGMovement : UnitMovement {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    protected void Update () {
        Movement();
        //Vector2 vel = Unit.Rigid.velocity;

        //vel.x = 0;

        //Unit.Rigid.velocity = vel;
	}
}
