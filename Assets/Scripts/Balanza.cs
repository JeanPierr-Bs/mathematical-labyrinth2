using System.Collections.Generic;
using UnityEngine;

public class Balanza : MonoBehaviour
{
    public float fuerzaTotalActual = 0f;
    public float umbralFuerza = 10f;// 3924f;
    public Puertas puerta; // Referencia a la puerta que se va a abrir

    private List<Caja> cajasEnBalanza = new List<Caja>();
    private float gravedad = 9.81f;

    private void OnTriggerEnter(Collider other)
    {
        Caja caja = other.GetComponent<Caja>();
        if (caja != null && !cajasEnBalanza.Contains(caja))
        {
            cajasEnBalanza.Add(caja);
            ActualizarFuerzaTotal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Caja caja = other.GetComponent<Caja>();
        if (caja != null && cajasEnBalanza.Contains(caja))
        {
            cajasEnBalanza.Remove(caja);
            ActualizarFuerzaTotal();
        }
    }

    private void ActualizarFuerzaTotal()
    {
        float masaTotal = 0f;
        foreach (var caja in cajasEnBalanza)
        {
            masaTotal += caja.masa;
        }

        fuerzaTotalActual = masaTotal * gravedad;
        //Debug.Log("Esta es la suma de las cajas " + masaTotal);
        //Debug.Log("Esta es la suma de las cajas " + fuerzaTotalActual);

        if (fuerzaTotalActual >= umbralFuerza)
        {
            PuertasController controller = GetComponentInParent<PuertasController>();
            controller.EscogerOpcion(0, puerta);
        }
        else
        {
            PuertasController controller = GetComponentInParent<PuertasController>();
            controller.EscogerOpcion(1, puerta);
        }
    }
}
