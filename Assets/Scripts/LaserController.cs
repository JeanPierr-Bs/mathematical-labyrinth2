using UnityEngine;
using TMPro;

public class LaserController : MonoBehaviour
{
    public Transform pivotX;            // Parte que rota en X (ca��n)
    public Transform laserOrigin;       // Origen del rayo
    public LineRenderer lineRenderer;   // L�nea visual del rayo
    public float maxLaserDistance = 50f;
    public TextMeshProUGUI angleText;   // UI que muestra �ngulo Y

    public Transform followTarget;      // Referencia a mano o c�mara
    public bool isHeld = false;

    private void Update()
    {
        if (isHeld && followTarget != null)
        {
            //FollowHand();
            //RotateWithCamera();
            // Pegarse completamente al target
            transform.position = followTarget.position;
            transform.rotation = followTarget.rotation;
            DrawLaser();
            UpdateUI();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    //void FollowHand()
    //{
    //    // Pegamos el objeto al target (mano/c�mara)
    //    transform.position = followTarget.position;
    //    transform.rotation = followTarget.rotation;
    //}

    //void RotateWithCamera()
    //{
    //    // Rotar el objeto base en Y
    //    Vector3 camEuler = followTarget.eulerAngles;
    //    transform.rotation = Quaternion.Euler(0, camEuler.y, 0);

    //    // Rotar el ca��n en X
    //    pivotX.localRotation = Quaternion.Euler(camEuler.x, 0, 0);
    //}

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

    void UpdateUI()
    {
        float angleY = transform.eulerAngles.y;
        if (angleText != null)
            angleText.text = "�ngulo Y: " + Mathf.Round(angleY) + "�";
    }
}
