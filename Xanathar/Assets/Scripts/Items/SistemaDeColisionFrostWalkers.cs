using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaDeColisionFrostWalkers : MonoBehaviour
{
    public List <GameObject> EnemigosAlcanzados;
    public List <float> VelocidadEnemigosAlcanzados;

    void OnParticleCollision(GameObject Collision)
    {
        Debug.Log("Colision Realizada");
        bool EnemigoIgual = false;

        if(EnemigosAlcanzados.Count == 0)
        {
            if(Collision.GetComponent<NavMeshAgent>() != null)
            {
                EnemigosAlcanzados.Add(Collision);
                VelocidadEnemigosAlcanzados.Add(Collision.GetComponent<NavMeshAgent>().speed);
                Collision.GetComponent<NavMeshAgent>().speed -= 4;
            }
        }

        for(int i = 0; i < EnemigosAlcanzados.Count; i++)
        {
            if(EnemigosAlcanzados[i] == null)
            {
                EnemigosAlcanzados.RemoveAt(i);
                VelocidadEnemigosAlcanzados.RemoveAt(i);
                break;
            }

            if(Collision.name == EnemigosAlcanzados[i].name)
            {
                EnemigoIgual = true;
            }
        }

        if(EnemigoIgual != true)
        {
            EnemigosAlcanzados.Add(Collision);
            VelocidadEnemigosAlcanzados.Add(Collision.GetComponent<NavMeshAgent>().speed);
            Collision.GetComponent<NavMeshAgent>().speed -= 4;
            EnemigoIgual = false;
        }
    }

    public IEnumerator EsperarParaVolver()
    {
        yield return new WaitForSeconds(3f);

        for(int i = 0; i < EnemigosAlcanzados.Count; i++)
        {
            if(EnemigosAlcanzados[i] == null)
            {
                EnemigosAlcanzados.RemoveAt(i);
                VelocidadEnemigosAlcanzados.RemoveAt(i);
            }
            else
            {
                EnemigosAlcanzados[i].GetComponent<NavMeshAgent>().speed = VelocidadEnemigosAlcanzados[i];
            }
        }
        
        EnemigosAlcanzados.Clear();
        VelocidadEnemigosAlcanzados.Clear();

        StartCoroutine(LanzarCorrutina());
        
    }

    public IEnumerator LanzarCorrutina()
    {
        yield return new WaitForSeconds(1f);

        if(FindObjectOfType<EstadisticasDePersonaje>().PermitirFrostWalkers == false)
        {
            yield return null;
        }
        else
        {
            StartCoroutine(EsperarParaVolver());
        }
    }
}
