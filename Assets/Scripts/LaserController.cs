using TMPro;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    //public Transform pivotX;            // Parte que rota en X (cañón)
    //public Transform laserOrigin;       // Origen del rayo
    //public LineRenderer lineRenderer;   // Línea visual del rayo
    //public float maxLaserDistance = 50f;
    //public TextMeshProUGUI angleText;   // UI que muestra ángulo Y

    //public Transform followTarget;      // Referencia a mano o cámara
    //public bool isHeld = false;

    //private void Update()
    //{
    //    if (isHeld && followTarget != null)
    //    {
    //        // Pegarse completamente al target
    //        transform.position = followTarget.position;
    //        transform.rotation = followTarget.rotation;
    //        DrawLaser();
    //        UpdateUI();
    //    }
    //    else
    //    {
    //        lineRenderer.enabled = false;
    //    }
    //}

    //void DrawLaser()
    //{
    //    lineRenderer.enabled = true;

    //    lineRenderer.SetPosition(0, laserOrigin.position);
    //    RaycastHit hit;
    //    Vector3 direction = laserOrigin.forward;

    //    if (Physics.Raycast(laserOrigin.position, direction, out hit, maxLaserDistance))
    //    {
    //        lineRenderer.SetPosition(1, hit.point);
    //    }
    //    else
    //    {
    //        lineRenderer.SetPosition(1, laserOrigin.position + direction * maxLaserDistance);
    //    }
    //}

    //void UpdateUI()
    //{
    //    float angleX = transform.eulerAngles.x;
    //    //float angleY = transform.eulerAngles.y;
    //    //float angleY = transform.rotation.y;
    //    //float angleX = transform.rotation.x;
    //    if (angleText != null)
    //        angleText.text = "Ángulo X: " + Mathf.Round(angleX);
    //}

    public Transform pivotX; // El cañón o parte que rota en X (vertical)
    public Transform laserOrigin; // Punto de origen del rayo
    public LineRenderer lineRenderer;
    public float maxLaserDistance = 50f;
    public TextMeshProUGUI angleText;
    public Transform followTarget; // Mano o punto de la cámara para seguir el ángulo vertical

    public bool isHeld = false;

    void Update()
    {
        if (isHeld && followTarget != null)
        {
            // Mantener la posición del láser fija (como arma en mano)
            transform.position = followTarget.position;
            transform.rotation = followTarget.rotation;

            // Solo aplicar rotación en X (vertical)
            float verticalAngle = followTarget.eulerAngles.x;

            // Clamp opcional si querés limitar el rango de movimiento
            if (verticalAngle > 180) verticalAngle -= 360; // Para que vaya de -180 a 180

            pivotX.localRotation = Quaternion.Euler(verticalAngle, 0f, 0f);

            DrawLaser();
            UpdateUI(verticalAngle);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void DrawLaser()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, laserOrigin.position);

        RaycastHit hit;
        Vector3 direction = laserOrigin.forward;

        if (Physics.Raycast(laserOrigin.position, direction, out hit, maxLaserDistance))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, laserOrigin.position + direction * maxLaserDistance);
        }
    }

    void UpdateUI(float angleX)
    {
        angleText.text = "Ángulo X (Inclinación): " + Mathf.Abs(Mathf.Round(-angleX)) + "°";
    }
}
