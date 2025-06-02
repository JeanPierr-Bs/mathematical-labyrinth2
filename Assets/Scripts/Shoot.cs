using UnityEngine;
using UnityEngine.XR;

public class Shoot : MonoBehaviour
{
    public GameObject shoot;
    public Transform spawnPoint;

    public float shotForce = 1500;
    public float shotRate = 0.5f;

    private float shotRateTime = 0;
    public bool isHeld = false; // <- Flag para controlar si se puede disparar

    private void Update()
    {
        if (!isHeld) return; // Solo se puede disparar si está en la mano;

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shotRateTime)
            {
                Quaternion customRotation = spawnPoint.rotation * Quaternion.Euler(90, 0, 0);
                GameObject newShoot = Instantiate(shoot, spawnPoint.position, customRotation);

                //Vector3 parabolaDirection = (spawnPoint.forward + spawnPoint.up * parabolaHeight).normalized;
                Rigidbody rb = newShoot.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.linearVelocity = spawnPoint.forward * (shotForce * Time.fixedDeltaTime); // velocidad directa hacia adelante
                //newShoot.GetComponent<Rigidbody>().AddForce();

                shotRateTime = Time.time + shotRate;

                Destroy(newShoot, 5);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(spawnPoint.position, spawnPoint.forward * 2);
        }
    }
}
