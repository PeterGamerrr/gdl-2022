using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float velPower;

    private Rigidbody2D rb;

    //movement
    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveDir;
    private Vector2 movementVel;

    //rotation
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        RotateTowardsMouse();
    }



    private void FixedUpdate()
    {
        MovePlayer();
    }




    void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2 (horizontalInput, verticalInput);
    }



    void MovePlayer()
    {
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);

        /*        float targetSpeed = horizontalInput * moveSpeed;
                float speedDif = targetSpeed - rb.velocity.magnitude;
                float accelRate = (Mathf.Abs(speedDif) > 0.01f) ? acceleration : decceleration;

                float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        *//*        movementVel.x = movement * Vector2.right;
                movementVel.y = movement * Vector2.up;*//*


                rb.AddForce(movement * Vector2.right);*/
    }


    void RotateTowardsMouse()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
    }

}
 