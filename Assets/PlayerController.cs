using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D MovementFilter { get => movementFilter; set => movementFilter = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    private void FixedUpdate(){
        if(movementInput !=Vector2.zero){
         bool success = TryMove(movementInput);
        
         if(!success && movementInput.x > 0){
            success = TryMove(new Vector2(movementInput.x,0));

            if(!success && movementInput.y > 0){
                 success = TryMove(new Vector2(0,movementInput.y));
            }
         }
         animator.SetBool("IsMoving",success);
        } else{
            animator.SetBool("IsMoving",false);
        }
        //Set direction of sprite to movement direction
        if(movementInput.x <0 ){
            spriteRenderer.flipX = true;
            }else if (movementInput.x > 0){
            spriteRenderer.flipX = false;
        }       
        
    }
    private bool TryMove(Vector2 directon){
         int count = rb.Cast(
                 directon,
                 MovementFilter,
                 castCollisions,
                 moveSpeed * Time.fixedDeltaTime + collisionOffset);   
            
            if(count == 0){
            rb.MovePosition(rb.position + directon * moveSpeed * Time.fixedDeltaTime);
            return true;
            }else {
                return false;
            }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    public int numRods = 0;
    
}
