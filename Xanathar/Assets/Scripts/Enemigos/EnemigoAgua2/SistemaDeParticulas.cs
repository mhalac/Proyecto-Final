using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDeParticulas : MonoBehaviour
{
    public static bool JugadorTocoBaba = false;
    public float VelocidadDeMovimientoActual;
    public GameObject Personaje;

    void OnParticleCollision(GameObject Collision)
    {
        if(Personaje == null)
        {
            Personaje = Collision.gameObject;
        }

        Personaje.GetComponent<EstadisticasDePersonaje>().DetectoColisionParticulas();
    }
}