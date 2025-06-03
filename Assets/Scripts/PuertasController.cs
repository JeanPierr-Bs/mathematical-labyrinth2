using System;
using System.Reflection;
using UnityEngine;

public class PuertasController : MonoBehaviour
{
    //Para Puerta Uno
    public Transform VectorC = null; // La flecha de respuesta que moverás
    public Transform VectorD = null; // La flecha de respuesta que moverás
    public Transform VectorE = null; // La flecha de respuesta que moverás

    public Transform Puerta; // La puerta que se abrirá
    public Transform Key; // La puerta que se abrirá

    //Pasar a dos vectores que se ocultan y se muestran
    //public Vector3[] posicionesRespuesta; // Las 3 posiciones para el vector C
    
    public int indiceRtaCorrecta = 2; // Index de la opción correcta (0, 1 o 2)

    private int indexEnviado;
    private Puertas opcionPuerta;
    RotarLlave rotarLlave = new RotarLlave();

    //public Vector2 vectorA = new Vector2(3f, 2f);
    //public Vector2 vectorB = new Vector2(1f, 2f);

    public Vector3 vectorA = new Vector3(3f, 0f, 2f);
    public Vector3 vectorB = new Vector3(1f, 0f, 2f);

    public void EscogerOpcion(int index, Puertas puerta)
    {
        indexEnviado = index;
        opcionPuerta = puerta;
        //vectorC.position = posicionesRespuesta[index];

        switch (opcionPuerta)
        {
            case Puertas.Uno:
                switch (index)
                {
                    case 0:
                        VectorC.gameObject.SetActive(false);
                        VectorD.gameObject.SetActive(true);
                        VectorE.gameObject.SetActive(false);
                        break;
                    case 1:
                        VectorC.gameObject.SetActive(false);
                        VectorD.gameObject.SetActive(false);
                        VectorE.gameObject.SetActive(true);
                        break;
                    case 2: //Respuesta OK
                        VectorC.gameObject.SetActive(true);
                        VectorD.gameObject.SetActive(false);
                        VectorE.gameObject.SetActive(false);
                        break;
                }
                rotarLlave = Key.GetComponent<RotarLlave>();                
                break;
            case Puertas.Dos:
                rotarLlave = Key.GetComponent<RotarLlave>();                
                break;
            case Puertas.Tres:
                rotarLlave = Key.GetComponent<RotarLlave>();                
                break;
            default:
                break;
        }

        if (indexEnviado == indiceRtaCorrecta)
        {
            GirarLlave();
            Invoke("AbrirPuerta", 3f);            
        }
        else {
            DetenerLlave();
            CerrarPuerta();
            Debug.Log("¡Respuesta InCorrecta!");
        }
    }

    private void GirarLlave()
    {
        if (Key)
        {
            rotarLlave.IniciarRotacion();
        }
    }

    private void DetenerLlave()
    {
        if (!Key)
        {
            rotarLlave.DetenerRotacion();
        }
    }

    private void AbrirPuerta()
    {
        if (Puerta.GetComponent<Collider>().enabled)
        {
            switch (opcionPuerta)
            {
                case Puertas.Uno:
                    //Puerta.Translate(Vector3.forward * 4f); // Mueve la puerta a la derecha                    

                    // Sumar los vectores
                    Vector3 vectorResultado = vectorA + vectorB;
                    Vector3 vector = new(0, vectorResultado.y, vectorResultado.z);

                    // Aplicar el movimiento a la puerta
                    Puerta.Translate(vector);

                    break;
                case Puertas.Dos:
                    Puerta.Translate(Vector3.back * 4f); // Mueve la puerta a la derecha
                    break;
                case Puertas.Tres:
                    Puerta.Translate(Vector3.back * 4f); // Mueve la puerta a la derecha
                    break;
                default:
                    break;
            }

            Puerta.GetComponent<Collider>().enabled = false; // Desactiva colisión

            rotarLlave.DetenerRotacion();
            Key.gameObject.SetActive(false);
            //Debug.Log("¡Respuesta correcta! Puerta abierta.");
        }
    }

    private void CerrarPuerta()
    {
        if (!Puerta.GetComponent<Collider>().enabled)
        {
            switch (opcionPuerta)
            {
                case Puertas.Uno:
                    Puerta.Translate(Vector3.forward * -4f); // Mueve la puerta a la derecha
                    break;
                case Puertas.Dos:
                    Puerta.Translate(Vector3.back * -4f); // Mueve la puerta a la derecha
                    break;
                case Puertas.Tres:
                    Puerta.Translate(Vector3.back * -4f); // Mueve la puerta a la derecha
                    break;
                default:
                    break;
            }

            Puerta.GetComponent<Collider>().enabled = true; // Desactiva colisión
            Key.gameObject.SetActive(true);
            //Debug.Log("¡Respuesta incorrecta! Puerta cerrada.");
        }
    }
}
