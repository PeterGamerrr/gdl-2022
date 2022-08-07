using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveSpeedMul;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float velPower;

    [SerializeField] Animator animator;
    private Rigidbody2D rb;


    //movement
    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveDir;
    public Vector2 movementVel;

    //rotation
    private Vector2 direction;
    private Vector2 mousePos;
    private Vector3 newScale;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StatManager.Instance.UpGradeEvent.AddListener(OnUpgrade);

    }

    private void OnUpgrade()
    {
    }

    void Update()
    {
        HandleInput();
        //RotateTowardsMouse();
        FlipTowardsMouse();
        UpdateAnimations();
    }



    private void FixedUpdate()
    {
        MovePlayer();
    }


    void UpdateAnimations()
    {
        if (horizontalInput >= 0.05
            || verticalInput >= 0.05
            || horizontalInput <= -0.05
            || verticalInput <= -0.05)
        {
            animator.SetBool("IsMoving", true);
        }
        if (horizontalInput <= 0.05
            && verticalInput <= 0.05
            && horizontalInput >= -0.05
            && verticalInput >= -0.05)
        {
            animator.SetBool("IsMoving", false);
        }
    }



    void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2 (horizontalInput, verticalInput).normalized;
    }



    void MovePlayer()
    {
        movementVel = rb.position + moveDir * moveSpeed * Time.deltaTime * (float)Math.Pow(moveSpeedMul, StatManager.Instance.Speed);
        rb.MovePosition(movementVel);

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

    void FlipTowardsMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && transform.localScale.x > 0 || mousePos.x > transform.position.x && transform.localScale.x < 0)
        {
            newScale = gameObject.transform.localScale;
            newScale.x *= -1;
            gameObject.transform.localScale = newScale;
        }

    }

}
 