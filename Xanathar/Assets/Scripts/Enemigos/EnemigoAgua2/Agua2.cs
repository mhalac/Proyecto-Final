using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agua2 : MonoBehaviour
{
    [Header("Transforms Seleccionables")]

    public AudioSource DisparoSound;
    public Transform PuntoDeDisparo;
    public Transform PuntoDeVision;
    public Transform Heredar;

    [Header("Variables")]
    public float RangoDeVision;
    public float AreaIdle;
    public float AnguloDeDisparo;
    public float Damage;
    public string EstadoActual;
    public string [] Estados = {"Idle" , "Searching" , "Attacking"};
    public bool PermitirDisparo = false;

    [Header("Parametros")]
    public NavMeshAgent Agente;
    public ParticleSystem Particulas;
    public Animator Animador;
    public GameObject PrefabBala;
    private GameObject Personaje;
    private Transform Personaje2;
    private Vector3 Destino;
    private Vector3 PosicionInicial;

    /*
    public Transform Objetivo;
    public Transform PuntoDeDisparo;
    public GameObject Bala;
    public float AnguloDeDisparo;
    */


    // Start is called before the first frame update
    void Start()
    {
        PosicionInicial = Heredar.position;
        Agente = GetComponent<NavMeshAgent>();

        EstadoActual = Estados[0];

        Animador.SetBool("Idle" , true);
        Animador.SetBool("Visto" , false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Agente.velocity.magnitude < 1f && EstadoActual != Estados[2])
        {
            FindObjectOfType<PositionManager>().Llegue(Destino);
            Idle();
        }

        if(PuedoVer())
        {
            Apuntar();
            
            if(PermitirDisparo == false)
            {
                PermitirAtaque();
            }
        }
        else if(EstadoActual == Estados[1])
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

    public bool PuedoVer()
    {
        Collider [] Obj = Physics.OverlapSphere(PuntoDeVision.position , RangoDeVision);

        for(int i = 0; i < Obj.Length; i++)
        {
            if(Obj[i].gameObject.tag == "Personaje")
            {
                Personaje = Obj[i].gameObject;
                Personaje2 = Obj[i].transform;
                return true;
            }
        }

        return false;
    }

    public void Buscar()
    {
        Agente.isStopped = false;

        Destino = Personaje.transform.position;
        Agente.destination = Destino;
        EstadoActual = Estados[1];
    }

    public void Apuntar()
    {
        Agente.isStopped = true;

        EstadoActual = Estados[2];
        Vector3 Direccion = (Personaje.transform.position - PuntoDeVision.transform.position).normalized;

        Quaternion Mirar = Quaternion.LookRotation(Direccion);
        transform.rotation = Quaternion.Lerp(transform.rotation , Mirar , Time.fixedDeltaTime);
    }

    public Vector3 TrayectoriaBala(Transform Objetivo , float Angulo)
    {
        var Dir = Objetivo.position - transform.position;
        var DiferenciaAltura = Dir.y;
        Dir.y = 0;

        var Distancia = Dir.magnitude;
        var A = Angulo * Mathf.Deg2Rad;
        Dir.y = Distancia * Mathf.Tan(A);

        Distancia += DiferenciaAltura / Mathf.Tan(A);

        var Velocidad = Mathf.Sqrt(Distancia * Physics.gravity.magnitude / Mathf.Sin(2 * A));
        return Velocidad * Dir.normalized;
    }

    public void DispararCañon()
    {
        Particulas.Play();
        var BalaDisparo = Instantiate(PrefabBala , PuntoDeDisparo.position , Quaternion.identity);
        DisparoSound.Play();
        BalaDisparo.GetComponent<Rigidbody>().velocity = TrayectoriaBala(Personaje2 , AnguloDeDisparo);
        Destroy(BalaDisparo , 10);
    }

    public void PermitirAtaque()
    {
        PermitirDisparo = true;

        Animador.SetBool("Idle" , false);
        Animador.SetBool("Visto" , true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PuntoDeVision.position , RangoDeVision);

        Gizmos.color = Color.white;
        Vector3 AreaCubo = new Vector3(AreaIdle * 2 , 2 , AreaIdle * 2);
        Gizmos.DrawWireCube(PosicionInicial , AreaCubo);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(Destino , 0.5f);
    }
}
