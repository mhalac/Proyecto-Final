using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Fuego2 : MonoBehaviour
{

    private RaycastHit hit;
    private Transform UltimaPosicion;

    [Header("Transforms Seleccionables")]
    public Transform Torso;
    public Transform ApuntadoTorso;

    private Animator animator;
    public Transform RayPos;
    public ParticleSystem FuegoAnim;
    public Transform VisionDisparo;
    public Transform RangoMinimo;

    [Header("Parametros")]

    public float Damage;
    public float AreaIdle;
    public string Elemento;
    public string NombreHijo;
    public float rotacion;
    public int AlcanzeMaximo;
    public int radioDisparar;
    public float BalaVelocidad;
    public float TiempoDisparando;
    public float DamagePorTiempo;
    public float delay;

    public float Vida;

    public string Estado;
    private NavMeshAgent agente;

    private string[] Estados = { "Idle", "Chasing", "Searching", "Shooting" };

    public Transform Cabeza;

    private Vector3 posicionSpawn;
    private bool Moviendose;
    private float TiempoDInicial;

    private float DelayInicial;

    private Transform Heredar;
    private Vector3 destino;



    private float DamagePorTiempoInicial;

    private Vector3 Apuntando;
    public bool Disparando;
    private Quaternion RotationDefault;
    private int PMask;
    private GameObject personaje;


    // Use this for initialization
    void Start()
    {
        //Guardas la posicion de spawn, obtenes el navmesh, asignas la FOV del enemigo sumando su radio asi se genera en el borde del enemigo
        //hacemos que su estado sea el [0], que es Idle
        // Y tmb generas un vector3 de las posiciones donde se van a generar los lugares a los que va a ir mientras este en idle

        personaje = GameObject.FindGameObjectWithTag("Personaje");
        DamagePorTiempoInicial = DamagePorTiempo;
        FuegoAnim.Stop();
        animator = GetComponentInChildren<Animator>();
        Elemento = GetComponent<LifeManager>().Elemento;
        TiempoDInicial = TiempoDisparando;
        if (transform.Find(NombreHijo) != null)
            Heredar = transform.Find(NombreHijo).GetComponent<Transform>();
        else
            Heredar = transform;
        posicionSpawn = Heredar.position;
        agente = GetComponent<NavMeshAgent>();
        PMask = LayerMask.NameToLayer("Personaje");
        VisionDisparo.position = new Vector3(Heredar.position.x, Heredar.position.y, Heredar.position.z + radioDisparar);
        RangoMinimo.position = new Vector3(Heredar.position.x, Heredar.position.y, Heredar.position.z + AlcanzeMaximo);
        Estado = Estados[0];
        DelayInicial = delay;
        agente.avoidancePriority = Random.Range(0, 99);
    }

    // Update is called once per frame
    void Update()
    {


        //Physics.IgnoreLayerCollision(gameObject.layer, PMask, true);
        //print(posicionSpawn);    
        if (transform.Find(NombreHijo) != null)
        {
            Heredar = transform.Find(NombreHijo).GetComponent<Transform>();
        }


        if (agente.velocity.magnitude < 2f && Estado != Estados[3])
        {


            FindObjectOfType<PositionManager>().Llegue(destino);
            Idle();
        }

        //Primero revisamos si el jugador esta en nuestra area en general, de vision y de rango general

        if (BuscarPersonaje() && PuedoVer())
        {
            Estadentro(TengoQueAcercarme());
        }

        // Si el personaje no esta en rango o no lo podes ver pueden pasar una de dos cosas
        // O antes lo estabas persiguiendo o disparando y lo perdiste de vista, en cuyo caso tenes que buscarlo
        // o por descarte, no estabas haciendo nada y por ende seguis en Idle haciendo nada.

        else if (Estado == Estados[1] || Estado == Estados[3])
        {
            FuegoAnim.Stop();
            Buscar();
        }

        else if (agente.remainingDistance < Mathf.Epsilon)
        {
            FuegoAnim.Stop();
            Idle();
        }

    }

    private void IrAPosRandom()
    {

        FuegoAnim.Stop();
        if (agente.remainingDistance > Mathf.Epsilon)
        {
            agente.destination = destino;

        }
        else
        {
            if (FindObjectOfType<PositionManager>().EstaOcupado(destino))
            {
                FindObjectOfType<PositionManager>().Llegue(destino);
            }
            destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(posicionSpawn, AreaIdle, Heredar.position);
            agente.destination = destino;
            Cabeza.rotation = transform.rotation;


        }
    }

    private void Idle()
    {
        agente.isStopped = false;
        Estado = Estados[0];

        IrAPosRandom();
    }

    private void Buscar()
    {
        //El enemigo se va a dirigir a la ultima posicion de donde estaba el jugador
        UltimaPosicion = personaje.transform;
        agente.isStopped = false;
        Estado = Estados[2];


        agente.SetDestination(UltimaPosicion.position);

    }
    protected bool PuedoVer()
    {
        //hace un raycast al jugador y devuelve true si no hay nada entre el enemigo y el personaje


        var direccion2 = (personaje.transform.position - RayPos.position).normalized;
        if (Physics.Raycast(RayPos.position, direccion2, out hit, radioDisparar) && hit.collider.gameObject.tag != "Personaje")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool BuscarPersonaje()
    {
        //genera una esfera logica alrededor tuyo para buscar al personaje y devuelve true si lo encontro/


        if (Vector3.Distance(personaje.transform.position, VisionDisparo.position) < radioDisparar)
        {
            Collider[] obj = Physics.OverlapSphere(VisionDisparo.position, radioDisparar);
            for (int i = 0; obj.Length > i; i++)
            {
                if (obj[i].gameObject.layer == PMask)
                {
                    //Debug.DrawRay(RayPos.position, direccion * hit.distance, Color.red);
                    return true;
                }

            }
        }

        return false;
    }
    //Revisa si una vez dentro del rango de vision se tiene que acercar para dispararle o no
    private bool TengoQueAcercarme()
    {
        Collider[] objdisparo = Physics.OverlapSphere(RangoMinimo.position, AlcanzeMaximo);

        for (int i = 0; objdisparo.Length > i; i++)
        {
            if (objdisparo[i].gameObject.layer == PMask)
            {
                return false;
            }
            if (objdisparo[i].gameObject.tag == "Enemigo")
            {
                float Dist = Vector3.Distance(objdisparo[i].gameObject.transform.position, Heredar.position);
                if (Dist < 2f)
                {

                    return false;
                }
            }
        }
        return true;
    }
    private void Acercar()
    {
        agente.destination = personaje.transform.position;
    }


    private Quaternion Apuntar(Transform Desde, Vector3 Hasta, float velocidad)
    {
        Vector3 direction = (Hasta - Desde.position).normalized;
        Quaternion rotar = Quaternion.Slerp(Cabeza.rotation, Quaternion.LookRotation(direction), Time.deltaTime * velocidad);

        return rotar;
    }
    private Quaternion Apuntar(Transform Desde, float velocidad)
    {
        Vector3 direction = (personaje.transform.position - Desde.position).normalized;
        Quaternion rotar = Quaternion.Lerp(Desde.rotation, Quaternion.LookRotation(direction), Time.deltaTime * velocidad);
        return rotar;
    }

    private void Disparar()
    {
        // Summary:
        //     Enemigo goes bang bang

        Estado = Estados[3];
        if (TiempoDisparando < 0 || delay > 0)
            delay -= Time.deltaTime;

        // Raycasteamos y sacamos el punto de vision en base a la cara
        Vector3 direction2 = Vector3.zero;
        if (Physics.Raycast(RayPos.position, Cabeza.forward, out hit, Mathf.Infinity))
        {
            direction2 = (hit.point - Cabeza.position).normalized;
            direction2 = direction2 * hit.distance;
            Debug.DrawRay(RayPos.position, direction2);
        }

        agente.isStopped = true;
        //Mientras no estemos disparando y no este demasiado cerca
        if (!Disparando && Vector3.Distance(transform.position, personaje.transform.position) > 4)
        {
            Cabeza.rotation = Apuntar(RayPos, personaje.transform.position, 3);
            Torso.rotation = Apuntar(ApuntadoTorso, 3.4f);

        }
        //Mientras estamos disparando y no esta cerca
        else
        {
            Cabeza.rotation = Apuntar(RayPos, personaje.transform.position, 2f);
            Torso.rotation = Apuntar(ApuntadoTorso, 2f);
        }


        //reiniciamos el contador de damage por el tiempo
        if (DamagePorTiempo < Mathf.Epsilon)
        {
            DamagePorTiempo = DamagePorTiempoInicial;
        }

        //Shoot shoot mano
        if (delay <= Mathf.Epsilon && TiempoDisparando > Mathf.Epsilon)
        {
            TiempoDisparando -= Time.deltaTime;
            Collider[] objs = null;
            //Debug.DrawRay(RayPos.position, Apuntando * hit.distance, Color.yellow);
            if (Physics.Raycast(RayPos.position, direction2, out hit, Mathf.Infinity))
            {
                objs = Physics.OverlapCapsule(RayPos.position, hit.point, 0.2f);
            }
            Disparando = true;

            //animator.speed = 1 / Mathf.Infinity;
            if (objs.Length > 0)
            {
                foreach (Collider o in objs)
                {
                    if (o.CompareTag("Personaje") && DamagePorTiempo == DamagePorTiempoInicial)
                    {
                        EstadisticasDePersonaje EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
                        EstadisticasDePersonaje.RecibirDaño(Damage);
                        ManejadorDeItems pj = FindObjectOfType<ManejadorDeItems>();
                        pj.ManejadorDeVida();

                        DamagePorTiempo -= Time.deltaTime;
                        //animator.speed = 1;
                    }
                }
            }
            DamagePorTiempo -= Time.deltaTime;
            if (!FuegoAnim.isPlaying) FuegoAnim.Play();


        }
        else if (TiempoDisparando < Mathf.Epsilon && delay < Mathf.Epsilon)
        {
            Disparando = false;
            TiempoDisparando = TiempoDInicial;
            delay = DelayInicial;
            animator.enabled = true;
            animator.speed = 1;
            FuegoAnim.Stop();
        }

    }
    private void Mori()
    {
        if (Vida < Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }

    private void Estadentro(bool TengoQueAcercarme)
    {


        var direccion2 = (personaje.transform.position - Heredar.position).normalized;

        //La funcion se llama cuando lo tenes a rango visual y fisico, preguntas si te tenes que acercar o no
        // en el caso de que si, tu estado pasa a chasing y te acercas, caso contrario, lo cagas a tiros. 

        if (TengoQueAcercarme)
        {
            Estado = Estados[1];
            agente.isStopped = false;
            Cabeza.rotation = Apuntar(Cabeza, personaje.transform.position, 10f);
            Acercar();
        }
        else
        {
            Estado = Estados[3];
            agente.isStopped = true;
            Disparar();
        }


    }
    private void Ventaja(float damage)
    {
        Vida = Vida - damage * 2;
    }
    private void Desventaja(float damage)
    {
        Vida = Vida - damage * 0.5f;
    }

    public void RecibirDamage()
    {

        EstadisticasDePersonaje Stats = GameObject.Find("Jugador").GetComponent<EstadisticasDePersonaje>();

        // Tenes que checkear las ventajas o debilidades manualmente, para eso revisas si tiene algun tipo de damage de ese
        //elemento y si es asi lo aplicas

        //Haces un switch para ir de una a tu elemento asi no laguea y despues te sacas vida en base a los damages que tenes
        // y las ventajas o desventajas se aplican distinto dependiendo tu elemento
        Vida -= Stats.DañoDePersonajeNormal;
        switch (Elemento)
        {

            case "Fuego":

                if (Stats.DañoElementalAgua > Mathf.Epsilon)
                {
                    Ventaja(Stats.DañoElementalAgua);
                }
                if (Stats.DañoElementalAire > Mathf.Epsilon)
                {
                    Desventaja(Stats.DañoElementalAire);
                }
                if (Stats.DañoElementalTierra > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalTierra;
                }

                if (Stats.DañoElementalFuego > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalFuego;
                }
                break;

            case "Viento":
                if (Stats.DañoElementalAgua > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalAgua;
                }
                if (Stats.DañoElementalAire > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalAire;
                }
                if (Stats.DañoElementalTierra > Mathf.Epsilon)
                {
                    Desventaja(Stats.DañoElementalTierra);
                }

                if (Stats.DañoElementalFuego > Mathf.Epsilon)
                {
                    Ventaja(Stats.DañoElementalFuego);
                }
                break;

            case "Agua":
                if (Stats.DañoElementalAgua > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalAgua;
                }
                if (Stats.DañoElementalAire > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalAire;
                }
                if (Stats.DañoElementalTierra > Mathf.Epsilon)
                {
                    Ventaja(Stats.DañoElementalTierra);
                }
                if (Stats.DañoElementalFuego > Mathf.Epsilon)
                {
                    Desventaja(Stats.DañoElementalFuego);
                }
                break;

            case "Tierra":
                if (Stats.DañoElementalAgua > Mathf.Epsilon)
                {
                    Desventaja(Stats.DañoElementalAgua);
                }
                if (Stats.DañoElementalAire > Mathf.Epsilon)
                {
                    Ventaja(Stats.DañoElementalAire);
                }
                if (Stats.DañoElementalTierra > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalTierra;
                }
                if (Stats.DañoElementalFuego > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalFuego;
                }
                break;


        }
        print("Yo: " + gameObject.name + " Y mi vida es de: " + Vida);
        Mori();


    }

    void OnDrawGizmosSelected()

    {
        Gizmos.color = Color.cyan;
        Vector3 cubo = new Vector3(AreaIdle * 2, 2, AreaIdle * 2);
        Gizmos.DrawWireCube(posicionSpawn, cubo);
        Heredar = transform.Find(NombreHijo).GetComponent<Transform>();
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destino, 0.5f);
        Gizmos.DrawWireSphere(RangoMinimo.position, AlcanzeMaximo);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(VisionDisparo.position, radioDisparar);

    }
}
