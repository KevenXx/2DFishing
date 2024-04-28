using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kura : MonoBehaviour
{
     private Animator animator;
    public float speed = 2.0f;
    public float safeDistance = 3.0f;
    public LayerMask Water;
    public float wanderSpeed = 1.0f;
    public float maxChangeTime = 2.0f;
    
    private Vector2 movementDirection;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private float timer;
    private GameObject player;
    private Rigidbody2D rb;
    public Collider2D kuraCollider; // Added Collider2D variable

    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        kuraCollider = GetComponent<Collider2D>(); // Initialize the Collider2D
        ChangeDirection();
        animator = GetComponent<Animator>();
        if(kuraCollider != null){
        minWalkPoint =kuraCollider.bounds.min;
        maxWalkPoint =kuraCollider.bounds.max;
        }
      
    }
        private void UpdateAnimation(Vector2 direction)
    {
        // If moving predominantly to the left
        if (direction.x < 0)
        {
            animator.SetBool("levo", true);
            animator.SetBool("desno", false);
        }
        // If moving predominantly to the right
        else if (direction.x > 0)
        {
            animator.SetBool("levo", false);
            animator.SetBool("desno", true);
        }
        else
        {
            // If not moving horizontally or vertical movement is stronger, reset both parameters
            animator.SetBool("levo", false);
            animator.SetBool("desno", false);
        }
    
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < safeDistance)
        {
            MoveAwayFromPlayer();
        }
        else
        {
            Wander();
        }
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 directionToMove = (transform.position - player.transform.position).normalized;
        Vector3 newPosition = transform.position + directionToMove * speed * Time.fixedDeltaTime;

        // Check for obstacle collision using Raycast or OverlapCircle before moving
        if (!Physics2D.Raycast(transform.position, directionToMove, safeDistance, Water) &&
            !Physics2D.OverlapCircle(newPosition, kuraCollider.bounds.extents.x, Water))
        {
            rb.MovePosition(newPosition);
        }
        // Optionally, implement behavior when stuck or near an obstacle
        UpdateAnimation(new Vector2(directionToMove.x, directionToMove.y));
    }

    private void Wander()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= maxChangeTime)
        {
            ChangeDirection();
            timer = 0;
        }

        rb.velocity = movementDirection * wanderSpeed;

        // Check for obstacles and redirect if necessary
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, 0.5f, Water);
        if (hit.collider != null)
        {
            ChangeDirection(); // Immediately change direction if about to hit an obstacle
        }
         UpdateAnimation(movementDirection);
    }

    private void ChangeDirection()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }
}