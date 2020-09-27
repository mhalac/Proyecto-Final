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
        
        if(EnemigosAlcanzados.Count == 0)
        {
            EnemigosAlcanzados.Add(Collision);
            VelocidadEnemigosAlcanzados.Add(Collision.GetComponent<NavMeshAgent>().speed);
            Collision.GetComponent<NavMeshAgent>().speed -= 4;
            //Collision.GetComponent<NavMeshAgent>().speed;
        }
        
        for(int i = 0; i < EnemigosAlcanzados.Count; i++)
        {
            if(EnemigosAlcanzados[i].gameObject.name != Collision.name)
            {
                EnemigosAlcanzados.Add(Collision);
                VelocidadEnemigosAlcanzados.Add(Collision.GetComponent<NavMeshAgent>().speed);
                Collision.GetComponent<NavMeshAgent>().speed -= 4;
            }
        }
    }

    public IEnumerator EsperarParaVolver()
    {
        yield return new WaitForSeconds(3f);

        for(int i = 0; i < EnemigosAlcanzados.Count; i++)
        {
            if(EnemigosAlcanzados[i] == null)
            {
                continue;
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
