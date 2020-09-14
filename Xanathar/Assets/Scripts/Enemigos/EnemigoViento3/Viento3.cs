using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Viento3 : MonoBehaviour
{
    private RaycastHit Hit;

    [Header("Transforms Seleccionables")]
    public Transform Heredar;
    public Transform Vision;
    public Transform PuntoDisparo;

    [Header("Variables")]
    public float RangoVision;
    public float RangoDeAtaque;
    public float AreaIdle;
    public float Damage;
    private int Pmask;
    private int Emask;
    public string [] Estados = {"Idle" , "Chasing" , "Searching" , "Attacking"};
    public string EstadoActual;
    public bool PuedoDispar;

    [Header("Parametros")]
    public NavMeshAgent Agente;
    public Animator Animador;
    public GameObject PrefabTornado;
    private Vector3 PosicionInicial;
    private Vector3 Destino;
    private GameObject Personaje;
    // Start is called before the first frame update
    void Start()
    {
        PosicionInicial = Heredar.position;
        Agente = GetComponent<NavMeshAgent>();

        Pmask = LayerMask.NameToLayer("Personaje");
        Emask = LayerMask.NameToLayer("Enemigo");

        EstadoActual = Estados[0];

        Animador.SetBool("Flotando" , true);
        Animador.SetBool("Atacando" , false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Agente.velocity.magnitude < 1f && EstadoActual != Estados[3])
        {
            FindObjectOfType<PositionManager>().Llegue(Destino);
            Idle();
        }

        if(EstoyARango())
        {
            //Debug.Log("Me preparo para cagarte a balazos");
            Apuntar();

            if(PuedoDispar == false)
            {
                Disparar();
            }
        }
        else if(BuscarPersonaje() && PuedoVer())
        {
            if(PuedoDispar == false)
            {
                Acercar();
            }
            else
            {
                Apuntar();
            }
        }
        else if(EstadoActual == Estados[3] || EstadoActual == Estados[1])
        {
            Buscar();
        }
    }

    public void Idle()
    {
        Agente.isStopped = false;
        EstadoActual = Estados[0];

        IrAPosRandom();
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

    public void Acercar()
    {
        Agente.destination = Personaje.transform.position;
        EstadoActual = Estados[1];
    }

    public void Buscar()
    {
        Agente.isStopped = false;
        //Agente.destination = Personaje.transform.position;
        Destino = Personaje.transform.position;
        Agente.destination = Destino;
        EstadoActual = Estados[2];
    }

    public void Apuntar()
    {
        BuscarPersonaje();

        Vector3 Direccion  = (Personaje.transform.position - Heredar.transform.position).normalized;
        Quaternion Mirar = Quaternion.LookRotation(Direccion);

        transform.rotation = Quaternion.Lerp(transform.rotation , Mirar , Time.fixedDeltaTime * 0.5f);
    }

    public void Disparar()
    {
        PuedoDispar = true;

        Animador.SetBool("Flotando" , false);
        Animador.SetBool("Atacando" , true);
    }

    public bool BuscarPersonaje()
    {
        Collider [] Obj = Physics.OverlapSphere(Vision.position , RangoVision);

        for(int i = 0; i < Obj.Length; i++)
        {
            if(Obj[i].gameObject.layer == Pmask)
            {
                Personaje = Obj[i].gameObject;
                return true;
            }
        }

        return false;
    }

    public bool EstoyARango()
    {
        Collider [] Obj = Physics.OverlapSphere(Vision.position , RangoDeAtaque);

        for(int i = 0; i < Obj.Length; i++)
        {
            if(Obj[i].gameObject.layer == Pmask)
            {
                EstadoActual = Estados[3];
                Agente.isStopped = true;
                return true;
            }
        }

        return false;
    }

    public bool PuedoVer()
    {
        var Direccion = Personaje.transform.position - Heredar.position;

        if(Physics.Raycast(Vision.position , Direccion , out Hit , RangoVision , Emask) && Hit.collider.gameObject.layer != Pmask)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public IEnumerator EsperarParaTirarTornado()
    {
        yield return new WaitForSeconds(4f);

        PuedoDispar = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Vision.position , RangoVision);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vision.position , RangoDeAtaque);

        Gizmos.color = Color.blue;
        Vector3 AreaCubo = new Vector3(AreaIdle * 2 , 2 , AreaIdle * 2);
        Gizmos.DrawWireCube(PosicionInicial , AreaCubo);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Destino , 0.5f);
    }
}
