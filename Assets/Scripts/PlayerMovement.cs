using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltRunSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
 
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }
   void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {

        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f,jumpSpeed);
        }
    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * fltRunSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool bolPlayerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", bolPlayerHasHorizontalSpeed);
    }
    void FlipSprite()
    {
        bool bolPlayerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(bolPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f); 
        }

    }
}