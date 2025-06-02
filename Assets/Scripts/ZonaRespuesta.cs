using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class ZonaRespuesta : MonoBehaviour
{
    public Puertas Puerta; //0, 1 o 2 Puerta actual
    public int opcionIndex; // 0, 1 o 2 Opcion de las respuestas

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            //PuertasController controller = FindFirstObjectByType<PuertasController>();
            PuertasController controller = GetComponentInParent<PuertasController>();
            controller.EscogerOpcion(opcionIndex, Puerta);
        }
    }
}
