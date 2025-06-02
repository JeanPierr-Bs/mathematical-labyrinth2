using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movimiento")]
    public float movementVelocity = 5f;

    [Header("Cámara Head Bobbing")]
    public float bobFrequency = 10f; // Frecuencia del movimiento
    public float bobHorizontalAmplitude = 0.05f; // Movimiento lateral
    public float bobVerticalAmplitude = 0.03f; // Movimiento vertical

    private float bobTimer = 0f;
    private Vector3 cameraInitialLocalPos;

    [Header("Salto")]
    public float forceJump = 8f;
    public float gravity = 20f;

    [Header("Rotación")]
    public Transform cameraFPS;
    public float mouseSensibility = 2f;
    public float verticalLimit = 80f;

    private float rotationX = 0f;
    private CharacterController controller;
    private Vector3 movementDiretion;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        if (cameraFPS != null)
            cameraInitialLocalPos = cameraFPS.localPosition;
    }

    void Update()
    {
        // Movimiento horizontal
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direccionHorizontal = transform.right * h + transform.forward * v;
        direccionHorizontal *= movementVelocity;

        // Saltar si está en el suelo
        if (controller.isGrounded)
        {
            verticalVelocity = -1f; // Mantenlo pegado al suelo

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = forceJump;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Combinar el movimiento horizontal con el vertical
        movementDiretion = direccionHorizontal + Vector3.up * verticalVelocity;
        controller.Move(movementDiretion * Time.deltaTime);

        // Rotación de cámara y personaje
        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalLimit, verticalLimit);

        cameraFPS.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
        // Determina si hay movimiento del jugador
        bool isMoving = controller.velocity.magnitude > 0.1f && controller.isGrounded;

        if (isMoving)
        {
            bobTimer += Time.deltaTime * bobFrequency;

            float bobOffsetY = Mathf.Sin(bobTimer) * bobVerticalAmplitude;
            float bobOffsetX = Mathf.Cos(bobTimer * 2) * bobHorizontalAmplitude;

            cameraFPS.localPosition = cameraInitialLocalPos + new Vector3(bobOffsetX, bobOffsetY, 0);
        }
        else
        {
            // Resetear cuando no se mueve
            bobTimer = 0f;
            cameraFPS.localPosition = Vector3.Lerp(cameraFPS.localPosition, cameraInitialLocalPos, Time.deltaTime * 5f);
        }
    }
}
