using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balanza : MonoBehaviour
{
    public float fuerzaTotalActual = 0f;
    public float umbralFuerza = 10f;// 3924f;
    public Puertas puerta; // Referencia a la puerta que se va a abrir
    public Text textSuma;
    public Text textFA;

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

        /*F = m * g*/
        fuerzaTotalActual = masaTotal * gravedad;

        textSuma.text = masaTotal.ToString();
        textFA.text = fuerzaTotalActual.ToString();

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
