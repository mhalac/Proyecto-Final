using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaAgua1 : MonoBehaviour
{
    public Agua1 Padre;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Personaje")
        {
            EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();

            Padre = FindObjectOfType<Agua1>();

            Estadisticas.RecibirDaño(Padre.Damage);
            Destroy(gameObject);
        }
    }
}
