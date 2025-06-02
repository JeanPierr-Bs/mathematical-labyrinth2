using System;
using System.Reflection;
using UnityEngine;

public class PuertasController : MonoBehaviour
{
    #region Variables Puerta Uno

    /*Posicion Vector Respuesta Por Defecto*/
    Vector3 vPositionRtaDF = new Vector3(-58.2340393f, 2.26958394f, 7.95028067f);
    Quaternion vRotationRtaDF = Quaternion.Euler(0.346169114f, 2.10772896f, 271.108032f);

    /*Posicion Vector Respuesta Correcta*/
    Vector3 vPositionRtaOK = new Vector3(-59.332962f, 1.47829986f, 7.9856782f);
    Quaternion vRotationRtaOK = Quaternion.Euler(0.346169055f, 2.10773039f, 297.003937f);

    /*Posicion Vector Respuesta UNO*/
    Vector3 vPositionRtaUNO = new Vector3(-57.5060005f, 1.33399999f, 7.91800022f);
    Quaternion vRotationRtaUNO = Quaternion.Euler(0.346169114f, 2.10772896f, 271.108032f);

    /*Posicion Vector Respuesta DOS*/
    Vector3 vPositionRtaDOS = new Vector3(-58.6920013f, 1.60300004f, 7.96400023f);
    Quaternion vRotationRtaDOS = Quaternion.Euler(0.346169114f, 2.10772896f, 271.108032f); 

    #endregion

    //Para Puerta Uno
    public Transform VectorC = null; // La flecha de respuesta que moverás

    public Transform Puerta; // La puerta que se abrirá
    public Transform Key; // La puerta que se abrirá

    //Pasar a dos vectores que se ocultan y se muestran
    //public Vector3[] posicionesRespuesta; // Las 3 posiciones para el vector C
    
    public int indiceRtaCorrecta = 2; // Index de la opción correcta (0, 1 o 2)

    private int indexEnviado;
    private Puertas opcionPuerta;
    RotarLlave rotarLlave = new RotarLlave();

    
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
                        VectorC.position = vPositionRtaOK;
                        VectorC.rotation = vRotationRtaOK;                
                        break;
                    case 1:
                        VectorC.position = vPositionRtaUNO;
                        VectorC.rotation = vRotationRtaUNO;                
                        break;
                    case 2:
                        VectorC.position = vPositionRtaDOS;
                        VectorC.rotation = vRotationRtaDOS;                
                        break;
                    default:
                        VectorC.position = vPositionRtaDF;
                        VectorC.rotation = vRotationRtaDF;
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
                    Puerta.Translate(Vector3.forward * 4f); // Mueve la puerta a la derecha                    
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

            Debug.Log("¡Respuesta correcta! Puerta abierta.");
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
            Debug.Log("¡Respuesta incorrecta! Puerta cerrada.");
        }
    }
}
