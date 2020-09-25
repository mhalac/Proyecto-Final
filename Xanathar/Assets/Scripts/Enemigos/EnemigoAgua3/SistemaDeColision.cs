using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDeColision : MonoBehaviour
{
    public bool TeDieron = false;
    public Agua3 Padre;
    void OnParticleCollision(GameObject Collision)
    {
        if(Collision.gameObject.tag == "Personaje" && TeDieron == false)
        {
            TeDieron = true;

            EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
            Estadisticas.RecibirDaño(Padre.Damage);

            StartCoroutine(EsperarParaDaño());
        }
    }

    IEnumerator EsperarParaDaño()
    {
        yield return new WaitForSeconds(1.5f);

        TeDieron = false;
    }
}
