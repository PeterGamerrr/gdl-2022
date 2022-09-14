using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveSpeedMul;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float velPower;

    [SerializeField] Animator animator;
    [SerializeField] int footstepDelay;

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

    }


    void Update()
    {
        FlipTowardsMouse();
        UpdateAnimations();
    }



    private void FixedUpdate()
    {
        MovePlayer();
    }


    void UpdateAnimations()
    {
        if (moveDir.x >= 0.05
            || moveDir.y >= 0.05
            || moveDir.x <= -0.05
            || moveDir.y <= -0.05)
        {
            animator.SetBool("IsMoving", true);
          //  StartCoroutine(PlayFootStepSound());
        }
        if (moveDir.x <= 0.05
            && moveDir.y <= 0.05
            && moveDir.x >= -0.05
            && moveDir.y >= -0.05)
        {
            animator.SetBool("IsMoving", false);
           // StopCoroutine(PlayFootStepSound());
        }
    }



    public void HandleInput(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();

        if (moveDir.magnitude > 1)
        {
            moveDir = moveDir / moveDir.magnitude;
        }
    }



    void MovePlayer()
    {


        float targetSpeedHorizontal = moveDir.x * moveSpeed;
        float targetSpeedVertical = moveDir.y * moveSpeed;
        float speedDifX = targetSpeedHorizontal - rb.velocity.x;
        float speedDifY = targetSpeedVertical - rb.velocity.y;
        float accelRateX = (Mathf.Abs(speedDifX) > 0.01f) ? acceleration : decceleration;
        float accelRateY = (Mathf.Abs(speedDifY) > 0.01f) ? acceleration : decceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);
        float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);

        movementVel.x = movementX * Vector2.right.x;
        movementVel.y = movementY * Vector2.up.y;


        rb.AddForce(movementVel);


    }



    void FlipTowardsMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (mousePos.x < transform.position.x && transform.localScale.x > 0 || mousePos.x > transform.position.x && transform.localScale.x < 0)
        {
            newScale = gameObject.transform.localScale;
            newScale.x *= -1;
            gameObject.transform.localScale = newScale;
        }

    }

}
 