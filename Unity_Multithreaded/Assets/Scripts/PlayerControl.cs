using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;

    public Rigidbody rig;
    public Transform frog;

    private float horizontal;
    private float vertical;



    private void Update()
    {
        horizontal = Input.GetAxis("Vertical");
        vertical = -Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
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
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("The frog die.");
        }
    }
}
