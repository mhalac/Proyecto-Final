using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoTierra3 : MonoBehaviour
{
    [Header("Transforms Seleccionables")]
    public Transform Heredar;
    public Transform PuntoVision;

    public AudioSource Swing;

    public Transform PuntoAtaque;

    [Header("Variables")]
    public float RangoVision;
    public float RangoIdle;
    public float RangoAtaque;
    public float Damage;
    public string[] Estados = { "Idle", "Searching", "Chasing", "Attacking" };
    public string EstadoActual;
    public bool PermitirAtaque = false;
    public bool PermitirColision = false;

    [Header("Colision")]
    public SphereCollider ColisionPuño;

    [Header("Parametros")]
    EstadisticasDePersonaje Estadisticas;
    public NavMeshAgent Agente;
    public Animator Animador;
    Vector3 PosicionInicial;
    Vector3 Destino;
    GameObject Personaje;
    // Start is called before the first frame update
    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();

        ColisionPuño.enabled = false;
        PosicionInicial = Heredar.position;

        EstadoActual = Estados[0];

        Animador.SetBool("Idle", true);
        Animador.SetBool("Caminando", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EstadoActual != Estados[2] && Agente.remainingDistance < Mathf.Epsilon)
        {
            Animador.SetBool("Idle", true);
            Animador.SetBool("Caminando", false);
        }


        if (EstadoActual == Estados[3])
        {
            ApuntarParaMatar();

            if (PermitirAtaque == false)
            {
                Atacar();
            }
        }
        else if (BuscarPersonaje())
        {

            EstadoActual = Estados[2];
            Acercar();
        }
        else if (EstadoActual == Estados[2])
        {

            Buscar();
        }

    }

    public bool BuscarPersonaje()
    {
        Collider[] Obj = Physics.OverlapSphere(PuntoVision.position, RangoVision);

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

    public void Acercar()
    {
        Destino = Personaje.transform.position;
        Agente.destination = Destino;
        Agente.isStopped = false;

        Animador.SetBool("Caminando", true);
        Animador.SetBool("Idle", false);

        ARangoDeAtaque();
    }

    public void Buscar()
    {
        Agente.isStopped = false;
        Destino = Personaje.transform.position;
        Agente.destination = Destino;
        EstadoActual = Estados[1];
    }


    public bool ARangoDeAtaque()
    {
        Collider[] Obj = Physics.OverlapSphere(PuntoAtaque.position, RangoAtaque);

        for (int i = 0; i < Obj.Length; i++)
        {
            if (Obj[i].gameObject.tag == "Personaje")
            {
                EstadoActual = Estados[3];
                return true;
            }
        }

        return false;
    }

    public void ApuntarParaMatar()
    {
        Vector3 Direccion = (Personaje.transform.position - transform.position).normalized;
        Quaternion Mirar = Quaternion.LookRotation(Direccion);

        transform.rotation = Quaternion.Lerp(transform.rotation, Mirar, Time.fixedDeltaTime);
    }

    public void Atacar()
    {
        PermitirAtaque = true;
        Agente.isStopped = true;
        Swing.Play();
        Animador.SetBool("Atacando", true);
        Animador.SetBool("Caminando", false);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(PuntoVision.position, RangoVision);

        Gizmos.color = Color.white;
        Vector3 AreaCubo = new Vector3(RangoIdle * 2, 2, RangoIdle * 2);
        Gizmos.DrawWireCube(PosicionInicial, AreaCubo);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PuntoAtaque.position, RangoAtaque);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(Destino, 0.5f);
    }

    //Funciones para animacion
    public void IrAPosRandom()
    {
        Animador.SetBool("Caminando", true);
        Animador.SetBool("Idle", false);

        EstadoActual = Estados[0];

        if (Agente.remainingDistance > 1)
        {
            Agente.destination = Destino;
        }
        else
        {
            if (FindObjectOfType<PositionManager>().EstaOcupado(Destino))
            {
                FindObjectOfType<PositionManager>().Llegue(Destino);
            }
            Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(PosicionInicial, RangoIdle, Heredar.position);
            Agente.destination = Destino;
        }
    }

    public void ActivarPuño()
    {
        ColisionPuño.enabled = true;
    }

    public void DesactivarPuño()
    {
        ColisionPuño.enabled = false;
    }

    public void PararDeAtacar()
    {
        if (!ARangoDeAtaque())
        {
            Animador.SetBool("Atacando", false);
            Animador.SetBool("Caminando", true);
        }

        EstadoActual = Estados[1];

        PermitirAtaque = false;
        Agente.isStopped = false;
        PermitirColision = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Personaje" && PermitirColision == false)
        {
            PermitirColision = true;

            var Dir = transform.forward;
            Dir.y = 3f;
            float fuerza = 10;

            StartCoroutine(Knockback(Dir, fuerza));

            Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
            Estadisticas.RecibirDaño(Damage);
        }
    }

    IEnumerator Knockback(Vector3 Dir, float Fuerza)
    {
        for (int i = 0; i < 20; i++)
        {
            Personaje.GetComponent<CharacterController>().Move(Dir * Time.fixedDeltaTime * Fuerza);
            yield return null;
        }

        yield return null;
    }
}
