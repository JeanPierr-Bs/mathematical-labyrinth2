using UnityEngine;

public class CollectioanbleItems : MonoBehaviour
{
    public GameObject handTarget;
    private GameObject pickedObject = null;
    public float throwForce = 5f;
    public Texture2D puntero;

    private void Update()
    {
        if (pickedObject != null)
        {
            // Mantener la posición del objeto en la mano
            pickedObject.transform.position = handTarget.transform.position;

            if (Input.GetKey("r"))
            {
                // Soltar el objeto
                pickedObject.transform.SetParent(null, true);
                pickedObject.transform.rotation = Quaternion.Euler(90, 0, 0);

                //Si es un objecto
                var physics = pickedObject.GetComponent<CustomPhysicsObject>();
                if (physics != null)
                {
                    physics.simulatePhysics = true;
                    
                    Vector3 throwDirection = handTarget.transform.forward + Vector3.up * 0.5f;
                    physics.velocity = throwDirection * throwForce;
                }

                //Si es un arma
                var shootScript = pickedObject.GetComponentInParent<Shoot>();
                if (shootScript != null)
                {
                    Debug.Log("Arma soltada");
                    shootScript.isHeld = false;
                }
                // Si es un láser
                var laserScript = pickedObject.GetComponent<LaserController>();
                if (laserScript != null)
                {
                    laserScript.isHeld = false;
                    laserScript.followTarget = null;
                    Debug.Log("¡Láser soltado!");
                }

                pickedObject = null;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Collectionable") && pickedObject == null)
        {
            if (Input.GetKey("e"))
            {
                pickedObject = other.gameObject;

                var physics = pickedObject.GetComponent<CustomPhysicsObject>();
                if (physics != null)
                {
                    physics.simulatePhysics = false;
                }
                
                pickedObject.transform.SetParent(handTarget.transform, false);
                pickedObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
                pickedObject.transform.position = handTarget.transform.position;

                // <<<<<<<<<<<<<< Aquí usamos GetComponentInParent
                var shootScript = pickedObject.GetComponentInParent<Shoot>();
                if (shootScript != null)
                {
                    shootScript.isHeld = true;
                    Debug.Log("¡Arma recogida!");
                }
                // Verificar si es un láser
                var laserScript = pickedObject.GetComponent<LaserController>();
                if (laserScript != null)
                {
                    laserScript.isHeld = true;
                    laserScript.followTarget = handTarget.transform; // <- importante
                    Debug.Log("¡Láser en mano!");
                }
            }
        }
    }
    void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width / 3, puntero.height / 3);
        GUI.DrawTexture(rect, puntero);
    }
}
