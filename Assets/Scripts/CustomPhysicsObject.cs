using UnityEngine;

public class CustomPhysicsObject : MonoBehaviour
{
    public Vector2 velocity;
    public float gravity = -9.8f;
    public bool simulatePhysics = true;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded = false;

    void Update()
    {
        if (!simulatePhysics) return;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }

        transform.position += (Vector3)(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
