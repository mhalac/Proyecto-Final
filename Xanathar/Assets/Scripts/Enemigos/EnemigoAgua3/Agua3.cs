using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agua3 : MonoBehaviour
{

    public AudioSource chorro;

    [Header("Transforms Seleccionables")]
    public Transform PuntoDeVision;
    public Transform PuntoDeAtaque;
    public Transform Heredar;

    [Header("Variables")]
    public float RangoDeVision;
    public float RangoAtaque;
    public float AreaIdle;
    public float Damage;
    public string EstadoActual;
    public string[] Estados = { "Idle", "Searching", "Chasing", "Attacking" };
    public bool PermitirDisparo = false;

    [Header("Parametros")]
    private NavMeshAgent Agente;
    public ParticleSystem ParticulasDisparo;
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

        Animador.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Agente.velocity.magnitude < 1f && EstadoActual != Estados[3])
        {
            FindObjectOfType<PositionManager>().Llegue(Destino);
            Idle();
        }



        if (EstoyARango())
        {
            Apuntar();
            //Ruedas.Stop();
            if (PermitirDisparo == false)
            {
                Disparar();
            }
        }
        else if (BuscarPersonaje())
        {
            if (PermitirDisparo == true)
            {
                DejarDeDisparar();
            }
            else
            {
                Acercar();
            }
        }
        else if (EstadoActual == Estados[2])
        {
            Buscar();
            VolverAlIdle();
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
        if (Agente.remainingDistance > Mathf.Epsilon)
        {
            Agente.destination = Destino;
        }
        else
        {
            if (FindObjectOfType<PositionManager>().EstaOcupado(Destino))
            {
                FindObjectOfType<PositionManager>().Llegue(Destino);
            }

            Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(PosicionInicial, AreaIdle, Heredar.position);
            Agente.destination = Destino;
        }
    }

    public bool BuscarPersonaje()
    {
        Collider[] Obj = Physics.OverlapSphere(PuntoDeVision.position, RangoDeVision);

        for (int i = 0; i < Obj.Length; i++)
        {
            if (Obj[i].gameObject.tag == "Personaje")
            {
                Personaje = Obj[i].gameObject;
                return true;
            }
        }

        return false;
    }

    public bool EstoyARango()
    {
        Collider[] Obj = Physics.OverlapSphere(PuntoDeAtaque.position, RangoAtaque);

        for (int i = 0; i < Obj.Length; i++)
        {
            if (Obj[i].gameObject.tag == "Personaje")
            {
                Personaje = Obj[i].gameObject;
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
        Vector3 Direccion = (Personaje.transform.position - transform.position).normalized;
        Quaternion Mirar = Quaternion.LookRotation(Direccion);

        transform.rotation = Quaternion.Lerp(transform.rotation, Mirar, Time.fixedDeltaTime * 2);
    }

    public void Disparar()
    {
        PermitirDisparo = true;
        Animador.SetBool("Idle", false);
        Animador.SetBool("PreparandoArma", true);
        if (!chorro.isPlaying) chorro.Play();
        ParticulasDisparo.Play();
    }

    public void DejarDeDisparar()
    {
        ParticulasDisparo.Stop();
        Agente.isStopped = false;
        chorro.Stop();
        Animador.SetBool("PreparandoArma", false);
        Animador.SetBool("GuardandoArma", true);

        PermitirDisparo = false;
    }

    public void VolverAlIdle()
    {
        Agente.isStopped = false;
        EstadoActual = Estados[0];
       
        
        Animador.SetBool("Idle", true);
        Animador.SetBool("GuardandoArma", false);

        PermitirDisparo = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(PuntoDeVision.position, RangoDeVision);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PuntoDeAtaque.position, RangoAtaque);

        Gizmos.color = Color.black;
        Vector3 Areacubo = new Vector3(AreaIdle * 2, 2, AreaIdle * 2);
        Gizmos.DrawWireCube(PosicionInicial, Areacubo);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Destino, 0.5f);
    }
}
