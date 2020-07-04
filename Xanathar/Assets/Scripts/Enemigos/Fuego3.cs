using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fuego3 : MonoBehaviour
{


    public bool TerminoAnimacion;
    public bool ListoParaDisparar;
    private RaycastHit hit;

    [Header("Transforms Seleccionables")]
    public GameObject balaPrefab;
    public Transform RayPos;
    public Transform VisionDisparo;

    [Header("Parametros")]
    public float DelayTeleport;
    public float AreaIdle;
    public string Elemento;
    public string NombreHijo;
    public float Damage;
    public float rotacion;
    public int radioDisparar;
    public float BalaVelocidad;
    public float Vida;
    public Vector3 Escala;
    public float AnimatorSpeed;
    public string Estado;

    private string[] Estados = { "Idle", "Shooting" };
    public AnimationClip Ataque;
    private float DelayTeleportInicial;
    private Vector3 posicionSpawn;

    public bool TermineDeAparecer;
    private float DelayInicial;
    public Transform PuntoDisparo;
    public Transform Heredar2;
    private Transform Heredar;
    private Vector3 destino;

    public float delay;

    public Animator anim;
    private int PMask;
    private GameObject personaje;
    private int EnemigoMask;
    private bool PuedoRestar;

    // Use this for initialization
    void Start()
    {
        //Guardas la posicion de spawn, obtenes el navmesh, asignas la FOV del enemigo sumando su radio asi se genera en el borde del enemigo
        //hacemos que su estado sea el [0], que es Idle
        // Y tmb generas un vector3 de las posiciones donde se van a generar los lugares a los que va a ir mientras este en idle
        Escala = transform.localScale;
        anim = GetComponentInChildren<Animator>();
        Heredar = Heredar2;
        posicionSpawn = Heredar.position;
        PMask = LayerMask.NameToLayer("Personaje");
        VisionDisparo.position = new Vector3(Heredar.position.x, Heredar.position.y, Heredar.position.z + radioDisparar);
        Estado = Estados[0];
        DelayInicial = delay;
        delay = 0;
        EnemigoMask = LayerMask.NameToLayer("Enemigo");
        DelayTeleportInicial = DelayTeleport;
        TermineDeAparecer = true;
        PuedoRestar = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.IgnoreLayerCollision(gameObject.layer, PMask, true);
        //print(posicionSpawn);
        print(anim.speed);

        if (transform.Find(NombreHijo) != null)
        {
            Heredar = transform.Find(NombreHijo).GetComponent<Transform>();
        }

        //Primero revisamos si el jugador esta en nuestra area en general, de vision y de rango general

        if (BuscarPersonaje() && PuedoVer())
        {
            Estadentro(false);
            //Apuntar(personaje.transform.position);
        }

        else if (DelayTeleport < Mathf.Epsilon)
        {
            delay = DelayInicial;
            Estado = Estados[0];
            ListoParaDisparar = false;

            Idle();
        }
        else if (PuedoRestar)
        {
            DelayTeleport -= Time.deltaTime;

        }



    }



    private void Idle()
    {
        float RandomX = Random.Range(posicionSpawn.x - AreaIdle, AreaIdle + posicionSpawn.x);
        float RandomZ = Random.Range(posicionSpawn.z - AreaIdle, AreaIdle + posicionSpawn.z);
        bool EstaOcupado = false;
        Vector3 Rpos = new Vector3(RandomX, transform.position.y, RandomZ);
        Collider[] Obj = Physics.OverlapSphere(Rpos, 1.3f);
        foreach (Collider o in Obj)
        {
            if (o.gameObject.tag == "Entorno" || o.gameObject.tag == "Enemigo")
            {
                EstaOcupado = true;
            }
        }
        if (!EstaOcupado)
        {
            transform.position = Rpos;
            anim.speed = 5;
            anim.SetBool("Tepeo", true);

            DelayTeleport = DelayTeleportInicial;
            DelayTeleport -= Time.deltaTime;
            var euler = transform.eulerAngles;
            euler = Vector3.zero;
            euler.y = Random.Range(0.0f, 360.0f);
            transform.eulerAngles = euler;
            TermineDeAparecer = false;
        }
    }
    private void IdleTarget()
    {
        int IndiceDeEmergencia = 0;
        bool VisionObstruida = false;
        while (true)
        {
            IndiceDeEmergencia++;
            if (IndiceDeEmergencia > 100)
            {
                //Debug.LogError("El objeto: "+gameObject.name + "Se rompio, saliendo");
                break;
            }
            float RandomX = Random.Range(posicionSpawn.x - AreaIdle, AreaIdle + posicionSpawn.x);
            float RandomZ = Random.Range(posicionSpawn.z - AreaIdle, AreaIdle + posicionSpawn.z);
            bool EstaOcupado = false;
            Vector3 Rpos = new Vector3(RandomX, transform.position.y, RandomZ);
            Collider[] Obj = Physics.OverlapSphere(Rpos, 1.3f);
            foreach (Collider o in Obj)
            {
                if ((o.gameObject.tag == "Entorno" || o.gameObject.tag == "Enemigo" || Vector3.Distance(personaje.transform.position, Rpos) < 7.4f || Vector3.Distance(personaje.transform.position, Rpos) > radioDisparar) || VisionObstruida)
                {

                    EstaOcupado = true;
                }
            }
            if (!EstaOcupado)
            {
                anim.SetBool("Tepeo", false);
                TerminoAnimacion = false;
                transform.position = Rpos;
                DelayTeleport = DelayTeleportInicial;
                anim.speed = 5;

                TermineDeAparecer = false;
                transform.LookAt(personaje.transform.position);
                break;

            }
        }
    }

    IEnumerator Idlex()
    {   
        yield return null;
    }
    protected bool PuedoVer()
    {
        //hace un raycast al jugador y devuelve true si no hay nada entre el enemigo y el personaje


        var direccion2 = personaje.transform.position - Heredar.position;
        if (Physics.Raycast(RayPos.position, direccion2, out hit, radioDisparar, EnemigoMask) && hit.collider.gameObject.tag != "Personaje")
        {
            return false;
        }
        else
            //Apuntar(personaje.transform.position);
            return true;
    }
    private bool BuscarPersonaje()
    {
        //genera una esfera logica alrededor tuyo para buscar al personaje y devuelve true si lo encontro/



        Collider[] obj = Physics.OverlapSphere(VisionDisparo.position, radioDisparar);
        for (int i = 0; obj.Length > i; i++)
        {
            if (obj[i].gameObject.layer == PMask)
            {
                personaje = obj[i].gameObject;
                var direccion = personaje.transform.position - Heredar.position;

                return true;
            }

        }
        return false;
    }


    public void Apagar()
    {
        PuedoRestar = true;
        anim.SetBool("Atacando", false);
        anim.speed = 5;
    }

    private void Disparar()
    {
        Estado = Estados[1];
        //Apuntar(personaje.transform.position);
        delay -= Time.deltaTime;
        if (Vector3.Distance(transform.position, personaje.transform.position) > 4)
        {
            Vector3 direction = (personaje.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(Heredar.rotation, lookRotation, Time.fixedDeltaTime * rotacion);
        }
        Vector3 dir = (personaje.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, dir, out hit, radioDisparar))
        {
            if (hit.transform.gameObject.tag == "Enemigo")
            {
                IdleTarget();

            }
        }

        if (delay <= 0)
        {
            delay = DelayInicial;
            PuedoRestar = false;

            anim.SetBool("Atacando", true);

        }
        if (ListoParaDisparar)
        {
            ListoParaDisparar = false;
            var direccion = (personaje.transform.position - PuntoDisparo.position).normalized;

            GameObject bala = Instantiate(balaPrefab, PuntoDisparo.position, Quaternion.identity);
            bala.GetComponent<ProyectilBase>().Lanzar(direccion, BalaVelocidad, Damage);
            StartCoroutine(Disparo());

        }
        //if(Disparando && anim.animation)
    }
    /*public IEnumerator AnimacionSpawn()
    {
        anim.SetBool("Aparecio",true);
        while (!TermineDeAparecer)
        {
            yield return null;
        }
        print("Spawnie");
        anim.SetBool("Aparecio",false);
    }*/
    IEnumerator ReproducirAnimacionDeTp()
    {
        anim.SetBool("Tepeo", true);
        anim.speed = 5;
        while (!TerminoAnimacion)
        {
            yield return null;
        }
        anim.SetBool("Tepeo", false);
        TermineDeAparecer = false;
        transform.localScale = Vector3.zero;
        IdleTarget();
    }

    IEnumerator Disparo()
    {
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(ReproducirAnimacionDeTp());
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

        //La funcion se llama cuando lo tenes a rango visual y fisico, preguntas si te tenes que acercar o no
        // en el caso de que si, tu estado pasa a chasing y te acercas, caso contrario, lo cagas a tiros. 

        Estado = Estados[1];

        Disparar();


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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(VisionDisparo.position, radioDisparar);

    }
}
