using UnityEditor;
using UnityEngine;

public class CollectioanbleItems : MonoBehaviour
{
    public GameObject handTarget;
    private GameObject pickedObject = null;
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

                var physics = pickedObject.GetComponent<CustomPhysicsObject>();
                if (physics != null)
                {
                    physics.simulatePhysics = true;
                    physics.velocity = Vector2.zero;
                }

                var shootScript = pickedObject.GetComponentInParent<Shoot>();
                if (shootScript != null)
                {
                    Debug.Log("Arma soltada");
                    shootScript.isHeld = false;
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
                    Debug.Log("¡Arma recogida correctamente!");
                    shootScript.isHeld = true;
                }
                else
                {
                    Debug.LogWarning("No se encontró el script 'Shoot' en el objeto recogido");
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
