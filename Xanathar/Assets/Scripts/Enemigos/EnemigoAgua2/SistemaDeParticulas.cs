using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDeParticulas : MonoBehaviour
{
    public static bool JugadorTocoBaba = false;
    float VelocidadDeMovimientoActual;

    void OnParticleCollision(GameObject Collision)
    {
        //Debug.Log("Colision emitida");

        
        if(JugadorTocoBaba == false)
        {
            JugadorTocoBaba = true;
            EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
            VelocidadDeMovimientoActual = Estadisticas.VelocidadDeMovimiento;

            if(!Input.GetKey(KeyCode.LeftShift))
            {
                VelocidadDeMovimientoActual += 6;
            }

            Estadisticas.VelocidadDeMovimiento -= 3;

            StartCoroutine(Esperar());
        }
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1f);

        EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
        Estadisticas.VelocidadDeMovimiento += 3;
        JugadorTocoBaba = false;
    }
}