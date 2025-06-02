using System;
using System.Collections;
using UnityEngine;

public class RotarLlave : MonoBehaviour
{
    //public Puertas Puerta; //0, 1 o 2 Puerta actual
    public Vector3 velocidadRotacion = new Vector3(0, 0, 90);
    private bool girar = false;


    void Update()
    {
        if (girar)
        {
            transform.Rotate(velocidadRotacion * Time.deltaTime, Space.Self);
        }
    }

    // Método público para activar la rotación
    public void IniciarRotacion()
    {
        girar = true;        
    }

    public void DetenerRotacion()
    {
        girar = false;
    }

}
