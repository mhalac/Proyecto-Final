using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Viento2 : MonoBehaviour
{
    private RaycastHit Hit;
    public AudioSource Disparo;

    [Header("Transforms Seleccionables")]
    public Transform Heredar;
    public Transform Vision;
    public Transform PuntoDisparo;
    public Transform PuntoFinal;

    [Header("Variables")]
    public float RangoDeVision;
    public float RangoDeAtaque;
    public float AreaIdle;
    public float Damage;
    private int Pmask;
    private int EMask;
    public string [] Estados = {"Idle" , "Searching" , "Chasing" , "Attacking"};
    public string EstadoActual;
    public bool PuedoDisparar = false;
    public bool AtaqueActivado = false;

    [Header("Parametros")]
    public NavMeshAgent Agente;
    public GameObject Particulas;
    private GameObject Personaje;
    public Animator Animador;
    private Vector3 PosicionInicial;
    private Vector3 UltimaPosicion;
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

        Particulas.SetActive(false);
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
            Apuntar();

            if(PuedoDisparar == false)
            {
                Disparar();
            }
        }
        else if(BuscarPersonaje() && PuedoVer())
        {
            if(PuedoDisparar == false)
            {
                Acercar();
            }
            else
            {
                Apuntar();
            }
        }
        else if(EstadoActual == Estados[2])
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

    public bool BuscarPersonaje()
    {
        Collider [] Obj = Physics.OverlapSphere(Vision.position , RangoDeVision);

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

    public bool PuedoVer()
    {
        var Direccion = Personaje.transform.position - Heredar.position;

        if(Physics.Raycast(Vision.position , Direccion , out Hit , RangoDeVision , EMask) && Hit.collider.gameObject.layer != Pmask)
        {
            return false;
        }
        else
        {
            return true;
        }
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

    public void Acercar()
    {
        Agente.destination = Personaje.transform.position;
        EstadoActual = Estados[2];
    }

    public void Buscar()
    {
        Agente.isStopped = false;
        Agente.destination = Personaje.transform.position;
        EstadoActual = Estados[1];
    }

    public void Apuntar()
    {
        BuscarPersonaje();

        EstadoActual = Estados[3];
        Vector3 Direccion = (Personaje.transform.position - Vision.transform.position).normalized;
        Quaternion Mirar = Quaternion.LookRotation(Direccion);

        transform.rotation = Quaternion.Lerp(transform.rotation , Mirar , Time.fixedDeltaTime * 2);
    }

    public void Disparar()
    {
        PuedoDisparar = true;

        Animador.SetBool("Flotando" , false);
        Animador.SetBool("Atacando" , true);
    }

    public void OndasDaño()
    {
        Collider [] Obj = Physics.OverlapCapsule(PuntoDisparo.position , PuntoFinal.position , 0.2f);

        for(int i = 0; i < Obj.Length; i++)
        {
            if(Obj[i].gameObject.layer == Pmask)
            {
                //Debug.Log("Te di puto");

                EstadisticasDePersonaje EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
                EstadisticasDePersonaje.RecibirDaño(Damage);
            }
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
