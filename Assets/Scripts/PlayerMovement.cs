﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PlayerMovement : MonoBehaviour
{
    // JPB: TODO: Make these configuration variables
    private const bool NICLS_COURIER = true;

    protected float maxTurnSpeed = NICLS_COURIER ? 50f : 45f;
    protected const float maxForwardSpeed = 10f;
    protected const float maxBackwardSpeed = 4f;

    protected const float rotDampingTime = 0.05f;

    protected const float joystickDeadZone = 0.02f;

    private int freeze_level = 0;

    private Rigidbody playerBody;
    public GameObject playerPerspective;

    public GameObject handlebars;
    protected const float maxHandlebarRotationX = 20f;
    protected const float maxHandlebarRotationY = 15f;

    private bool smoothMovement = false;

    void Start() {
        playerBody = GetComponent<Rigidbody>();
        smoothMovement = Config.Get(() => Config.smoothMovement, false);
    }

    public float horizontalInput;
    public float verticalInput;

    public Vector3 dampedHorizInput;
    public Vector3 horizVel = Vector3.zero;

    void Update()
    {
        if (smoothMovement)
        {
            // This is only in Update because we want it locked to frame rate.
            // Because MoveRotation is used, the rotation doesn't occur until the next FixedUpdate
            // Also, adjusting the velocity doesn't change any position until the next FixedUpdate
            if (!IsFrozen())
            {
                horizontalInput = InputManager.GetAxis("Horizontal");
                verticalInput = InputManager.GetAxisRaw("Vertical");

                // Rotate the bike handlebars
                //handlebars.transform.localRotation = Quaternion.Euler(horizontalInput * maxHandlebarRotationX, dampedHorizInput * maxHandlebarRotationY, 0);

                // Rotate the player's perspective
                //playerPerspective.transform.localRotation = Quaternion.Euler(0, 0, -dampedHorizInput * 5f);

                // Rotate the player
                dampedHorizInput = Vector3.SmoothDamp(dampedHorizInput, Vector3.up * horizontalInput, ref horizVel, rotDampingTime);
                Quaternion deltaRotation = Quaternion.Euler(dampedHorizInput * maxTurnSpeed * Time.smoothDeltaTime);
                playerBody.MoveRotation(playerBody.rotation * deltaRotation);

                // Move the player
                if (verticalInput > joystickDeadZone)
                    playerBody.velocity = Vector3.ClampMagnitude(playerBody.transform.forward * (verticalInput - Mathf.Abs(dampedHorizInput.y) * 0.2f) * maxForwardSpeed, maxForwardSpeed);
                else if (verticalInput < -joystickDeadZone)
                    playerBody.velocity = Vector3.ClampMagnitude(playerBody.transform.forward * (verticalInput - Mathf.Abs(dampedHorizInput.y) * 0.2f) * maxBackwardSpeed, maxBackwardSpeed);
                else
                    playerBody.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        if (!smoothMovement)
        {
            horizontalInput = InputManager.GetAxis("Horizontal");
            verticalInput = InputManager.GetAxisRaw("Vertical");
            if (!IsFrozen())
            {
                // Rotate the bike handlebars
                //handlebars.transform.localRotation = Quaternion.Euler(horizontalInput * maxHandlebarRotationX, horizontalInput * maxHandlebarRotationY, 0);

                // Rotate the player's perspective
                //playerPerspective.transform.localRotation = Quaternion.Euler(0, 0, -horizontalInput * 5f);

                // Rotate the player
                if (Mathf.Abs(horizontalInput) > joystickDeadZone)
                {
                    Quaternion deltaRotation = Quaternion.Euler(Vector3.up * horizontalInput * maxTurnSpeed * Time.fixedDeltaTime);
                    playerBody.MoveRotation(playerBody.rotation * deltaRotation);
                }

                // Move the player
                if (verticalInput > joystickDeadZone)
                    playerBody.velocity = Vector3.ClampMagnitude(playerBody.transform.forward * verticalInput * maxForwardSpeed, maxForwardSpeed);
                else if (verticalInput < -joystickDeadZone)
                    playerBody.velocity = Vector3.ClampMagnitude(playerBody.transform.forward * verticalInput * maxBackwardSpeed, maxBackwardSpeed);
                else
                    playerBody.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public bool IsFrozen()
    {
        return freeze_level > 0;
    }

    // JPB: TODO: Fix this whole system
    public bool IsDoubleFrozen()
    {
        return freeze_level > 1;
    }

    public void Freeze()
    {
        freeze_level++;
    }

    public void Unfreeze()
    {
        freeze_level--;
    }

    public void Zero()
    {
        freeze_level = 0;
    }
}
