using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Viento2 : MonoBehaviour
{
    [Header("Transforms Seleccionables")]
    public Transform Heredar;
    public Transform Vision;

    [Header("Variables")]
    public float RangoDeVision;
    public float RangoDeAtaque;
    public float AreaIdle;
    private int Pmask;
    private int EMask;
    private string [] Estados = {"Idle" , "Searching" , "Chasing" , "Attacking"};
    public string EstadoActual;

    [Header("Parametros")]
    public NavMeshAgent Agente;
    public Animator Animador;
    private Vector3 PosicionInicial;
    private Vector3 Destino;
    // Start is called before the first frame update
    void Start()
    {
        PosicionInicial = Heredar.position;
        EstadoActual = Estados[0];

        Agente = GetComponent<NavMeshAgent>();

        Pmask = LayerMask.NameToLayer("Personaje");
        EMask = LayerMask.NameToLayer("Enemigo");

        Animador.SetBool("Flotando" , true);
        Animador.SetBool("Atacando" , false);
    }

    // Update is called once per frame
    void Update()
    {
        //IrAPosRandom();
    }

    public void IrAPosRandom()
    {
        if(Agente.remainingDistance > Mathf.Epsilon)
        {
            Agente.destination = Destino;
        }
        else
        {
            if(FindObjectOfType<PositionManager>().EstaOcupado(Destino))
            {
                FindObjectOfType<PositionManager>().Llegue(Destino);
            }

            Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(PosicionInicial , AreaIdle , Heredar.position);
            Agente.destination = Destino;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Vision.position , RangoDeVision);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vision.position , RangoDeAtaque);

        Gizmos.color = Color.magenta;
        Vector3 AreaCubo = new Vector3(AreaIdle * 2 , 2 , AreaIdle * 2);
        Gizmos.DrawWireCube(PosicionInicial , AreaCubo);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(Destino , 0.5f);
    }
}
