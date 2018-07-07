using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    Rigidbody rbody;

    Vector3 moveInput;
    Vector3 lookDirection;
    float turnSpeed = 10;
    public float baseMoveSpeed = 1;
    float currentMoveSpeed;

    bool isDiving = false;
    float diveDuration = 0.3f;
    float timeDiveEnds = 0;
    float diveSpeedMultiplier = 2;

    // called when you capture one of the hiders
    public event Action HiderTagged;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody>();
        currentMoveSpeed = baseMoveSpeed;
        lookDirection = transform.forward;
	}
	
	// Update is called once per frame
    void Update() {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if(moveInput.sqrMagnitude != 0 && !isDiving)
        {
            // look direction is the direction you last moved in
            // only changed when movement buttons are pushed
            // otherwise it would look up when no buttons are being pushed.
            lookDirection = moveInput;
        }

        if(Input.GetButtonDown("Jump"))
        {
            // spacebar
            // dive
            //rbody.AddForce(lookDirection * 500);
            if (!isDiving)
            {
                isDiving = true;
                timeDiveEnds = Time.time + diveDuration;
            }
        }
    }
	void FixedUpdate () {

        if (!isDiving)
        {
            // Normal Movement
            // rotate towards the direction you are moving
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveInput, Time.fixedDeltaTime * currentMoveSpeed);
            transform.forward = Vector3.RotateTowards(transform.forward, lookDirection, turnSpeed * Time.fixedDeltaTime, 1);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + lookDirection, Time.fixedDeltaTime * currentMoveSpeed * diveSpeedMultiplier);
            transform.forward = Vector3.RotateTowards(transform.forward, lookDirection, turnSpeed * Time.fixedDeltaTime, 1);
            if (Time.time > timeDiveEnds)
            {
                isDiving = false;
            }
        }
    }

    void CaughtHider(GameObject hider)
    {
        if(HiderTagged != null)
        {
            HiderTagged();
        }
        Destroy(hider);
    }

    void OnTriggerEnter(Collider col)
    {
        if (isDiving)
        {
            if (col.GetComponent<AIMovement>() != null)
            {
                CaughtHider(col.gameObject);
            }
        }
    }
}
