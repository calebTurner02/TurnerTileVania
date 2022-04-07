using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltRunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 5f;
    [SerializeField] float fltClimbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;


    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float fltGravityScaleAtStart;

    bool bolIsAlive = true;

 
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        fltGravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if(!bolIsAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if(!bolIsAlive) {return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }

   void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(!bolIsAlive) {return;}

        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;} 
       



        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f,fltJumpSpeed);
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

    void ClimbLadder()
    {

         if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
         {

             myRigidBody.gravityScale = fltGravityScaleAtStart; 
             myAnimator.SetBool("IsClimbing", false);
             return;
         }
         

        Vector2 ClimbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * fltClimbSpeed);
        myRigidBody.velocity = ClimbVelocity;
        myRigidBody.gravityScale = 0;

        bool bolPlayerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", bolPlayerHasVerticalSpeed);
    }

    void Die() 
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            bolIsAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;
        }
    }
        
        
    
}
