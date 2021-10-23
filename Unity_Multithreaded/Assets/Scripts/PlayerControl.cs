﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float jumpForce;

    public Rigidbody rig;
    public Transform frog;
    public Transform groundDetector;
    public LayerMask ground;

    private GameStatusManager gameStatusManager;

    private float horizontal;
    private float vertical;
    private bool jump;
    private bool isGameEnd = false;    // win or lose


    private void Start()
    {
        gameStatusManager = GameObject.Find("GameStatusManager").GetComponent<GameStatusManager>();
    }

    private void Update()
    {
        if (isGameEnd) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Quit the game.");
            gameStatusManager.ManuallyQuitGame();
        }


        horizontal = Input.GetAxis("Vertical");
        vertical = -Input.GetAxis("Horizontal");

        jump = Input.GetKeyDown(KeyCode.Space);

        // jump
        bool isGrounded = (Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground)); ;
        if (jump && isGrounded)
        {
            rig.AddForce(Vector3.up * jumpForce);
        }
    }

    private void FixedUpdate()
    {
        if (isGameEnd) return;

        // movement and rotation
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction.Normalize();

        Vector3 newRotationDirection = Vector3.RotateTowards(frog.forward, direction, rotateSpeed * Time.deltaTime, 0.0f);

        Vector3 velocity = direction * speed * Time.deltaTime;
        velocity.y = rig.velocity.y;
        rig.velocity = velocity;

        frog.rotation = Quaternion.LookRotation(newRotationDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameEnd) return;

        if (collision.gameObject.layer == 8)
        {
            Debug.Log("The frog die.");
            gameStatusManager.Lose();
            isGameEnd = true;
        }
        else if (collision.gameObject.layer == 10)
        {
            Debug.Log("You win!");
            gameStatusManager.Win();
            isGameEnd = true;
        }
    }
}
