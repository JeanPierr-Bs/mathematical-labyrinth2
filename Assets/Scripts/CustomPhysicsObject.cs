using UnityEngine;

public class CustomPhysicsObject : MonoBehaviour
{
    //public Vector3 velocity;
    //public float gravity = -9.8f;
    //public bool simulatePhysics = true;
    //public float groundCheckDistance = 0.1f;
    //public LayerMask groundLayer;
    //private bool isGrounded = false;
    public Vector3 velocity;
    public float gravity = -9.8f;
    public bool simulatePhysics = true;
    public float groundCheckDistance = 0.1f;
    public float wallCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private bool isGrounded = false;

    void Update()
    {
        if (!simulatePhysics) return;

        // Detección de suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity = Vector3.zero;
        }

        // Guardamos el movimiento original
        Vector3 finalVelocity = velocity;

        // Detección de paredes en los 4 ejes horizontales
        if (Physics.Raycast(transform.position, Vector3.forward, wallCheckDistance, wallLayer) && finalVelocity.z > 0)
        {
            finalVelocity.z = 0;
        }
        if (Physics.Raycast(transform.position, Vector3.back, wallCheckDistance, wallLayer) && finalVelocity.z < 0)
        {
            finalVelocity.z = 0;
        }
        if (Physics.Raycast(transform.position, Vector3.right, wallCheckDistance, wallLayer) && finalVelocity.x > 0)
        {
            finalVelocity.x = 0;
        }
        if (Physics.Raycast(transform.position, Vector3.left, wallCheckDistance, wallLayer) && finalVelocity.x < 0)
        {
            finalVelocity.x = 0;
        }

        // Movimiento final
        transform.position += finalVelocity * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * groundCheckDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * groundCheckDistance);
    }
}
