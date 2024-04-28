using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private bool moveLeft = true;
    private float movetime;
    private float vRange;
    private float speed; // This will now be set only once

    void Start()
    {
        InitializeFish();
    }

    void Update()
    {
        HandleMovementTimer();
        CheckAndFlipAtBoundaries();
        MoveFish();
    }

    private void InitializeFish()
    {
        vRange = Random.Range(-0.002f, 0.002f); // Randomize vertical movement range
        movetime = Random.Range(1f, 3f); // Randomize direction change timer
        speed = Random.Range(0.002f, 0.008f); // Set a fixed speed for each fish upon creation
        UpdateScale(); // Initialize scale based on direction
    }

    private void HandleMovementTimer()
    {
        movetime -= Time.deltaTime;
        if (movetime < 0)
        {
            movetime = Random.Range(1f, 3f); // Reset timer for direction change
            moveLeft = !moveLeft; // Change direction
            UpdateScale(); // Update scale to reflect new direction
        }
    }

    private void CheckAndFlipAtBoundaries()
    {
        Vector3 currentPos = transform.position;
        if (currentPos.x < -2.22f && moveLeft)
        {
            moveLeft = false;
            UpdateScale();
        }
        if (currentPos.x > 2.27f && !moveLeft)
        {
            moveLeft = true;
            UpdateScale();
        }
        CheckVerticalBounds(currentPos);
    }

    private void MoveFish()
    {
        float horizontalMove = (moveLeft ? -1 : 1) * speed;
        Vector3 movement = new Vector3(horizontalMove, vRange, 0);
        transform.position += movement;
    }

    private void UpdateScale()
    {
        transform.localScale = new Vector3(moveLeft ? -1 : 1, 1, 1);
    }

    private void CheckVerticalBounds(Vector3 currentPos)
    {
        if ((currentPos.y < -1.12f && vRange < 0) || (currentPos.y > 1.12f && vRange > 0))
        {
            vRange = -vRange; // Invert vertical movement if hitting the vertical bounds
        }
    }
   private void OnMouseDown()
{
    Debug.Log("Fish clicked");
    if (GameManager.Instance != null)
    {
        GameManager.Instance.FishCaught();
        Destroy(gameObject);
    }
}
}