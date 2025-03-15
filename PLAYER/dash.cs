using System.Collections;
using UnityEngine;

public class dash : MonoBehaviour
{
    [SerializeField] float startDashTime = 1f;
    [SerializeField] float dashSpeed = 1f;

    Rigidbody2D rb;

    float currentDashTime;

    bool canDash = true;
    private bool playerCollision;

    public float dashCooldown = 2.0f; // Set the cooldown time to 2 seconds
    private float lastDashTime;
    private Vector2 dashDirection; // The direction for dashing based on joystick input

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void DashButtonClicked()
    {
        if (canDash )
        {
            if (Time.time > lastDashTime + dashCooldown)
            {
                // Use the stored dashDirection to perform the dash
                //StartCoroutine(Dash(dashDirection));
                StartCoroutine(Dash(dashDirection != Vector2.zero ? dashDirection : rb.velocity.normalized));
                lastDashTime = Time.time;
            }
            else
            {
                Debug.Log("Dash on cooldown");
                
            }
        }
    }

    // Set the dash direction based on joystick input
    public void SetDashDirection(Vector2 direction)
    {
        dashDirection = direction.normalized;
    }

    IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        playerCollision = false;
        currentDashTime = startDashTime; // Reset the dash timer.

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime; // Lower the dash timer each frame.

            rb.velocity = direction * dashSpeed; // Dash in the specified direction.

            yield return null; // Returns out of the coroutine this frame so we don't hit an infinite loop.
        }


        canDash = true;
        playerCollision = true;
    }
}
