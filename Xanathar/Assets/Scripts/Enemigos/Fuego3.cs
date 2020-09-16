using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fuego3 : MonoBehaviour
{

    public bool TargetLock;
    public bool TerminoAnimacion;
    public bool ListoParaDisparar;
    private RaycastHit hit;

    [Header("Transforms Seleccionables")]
    public GameObject balaPrefab;
    public Transform RayPos;
    public Transform VisionDisparo;

    public Transform CentroDelCuerpo;
    [Header("Parametros")]
    public GameObject Piso;
    public float DelayTeleport;
    public float AreaIdle;
    public string NombreHijo;
    public float Damage;
    public float rotacion;
    public int radioDisparar;
    public float BalaVelocidad;
    public float Vida;
    public Vector3 Escala;

    

    private string[] Estados = { "Idle", "Shooting" };
    private float DelayTeleportInicial;
    private Vector3 posicionSpawn;

    public bool TermineDeAparecer;
    private float DelayInicial;
    public Transform PuntoDisparo;
    public Transform Heredar2;
    private Transform Heredar;

    public float delay;

    public Animator anim;
    private int PMask;
    private GameObject personaje;
    private bool PuedoRestar;
    
    private Vector3 PInical;
    public string Estado;




    // Use this for initialization
    private void Start()
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
        DelayTeleportInicial = DelayTeleport;
        TermineDeAparecer = true;
        PuedoRestar = true;
        //anim.speed = 4;

    }

    // Update is called once per frame
    void Update()
    {
     

        if (transform.Find(NombreHijo) != null)
        {
            Heredar = transform.Find(NombreHijo).GetComponent<Transform>();
        }
        //Primero revisamos si el jugador esta en nuestra area en general, de vision y de rango general
        if (TargetLock && DelayTeleport < Mathf.Epsilon || !(BuscarPersonaje() || PuedoVer()) && Estado == Estados[1])
        {
            DelayTeleport = DelayTeleportInicial;
            
            IdleTarget();
        }

        else if (DelayTeleport < Mathf.Epsilon)
        {
            delay = DelayInicial;
            Estado = Estados[0];
            ListoParaDisparar = false;

            Idle();
        }
        if (BuscarPersonaje() && PuedoVer())
        {
            Estado = Estados[1];
            TargetLock = true;
            Disparar();
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
        Vector3 Rpos = new Vector3(RandomX, personaje.transform.position.y + .2f, RandomZ);
        Collider[] Obj = Physics.OverlapSphere(Rpos, .5f);
        foreach (Collider o in Obj)
        {
            if (o.gameObject.tag == "Entorno" || o.gameObject.tag == "Enemigo")
            {
                EstaOcupado = true;
            }
        }
        if (!EstaOcupado)
        {

            //anim.speed = 5;
            StartCoroutine(IdleTeleport(Rpos));


        }

    }

    private void IdleTarget()
    {

        int IndiceDeEmergencia = 0;
        bool VisionObstruida = false;
        TargetLock = true;

        while (true)
        {
            print("buscando");
            IndiceDeEmergencia++;
            if (IndiceDeEmergencia > 30)
            {
                Debug.LogError("El objeto: " + gameObject.name + "Se rompio, saliendo");
                Idle();
                TargetLock = false;
                break;
            }
            float RandomX = Random.Range(personaje.transform.position.x - radioDisparar, radioDisparar + personaje.transform.position.x);
            float RandomZ = Random.Range(personaje.transform.position.z - radioDisparar, radioDisparar + personaje.transform.position.z);
            bool EstaOcupado = false;
            Vector3 Rpos = new Vector3(RandomX, personaje.transform.position.y, RandomZ);
            Collider[] Obj = Physics.OverlapSphere(Rpos, 0.3f);
            var direccion = (personaje.transform.position - Rpos).normalized;
            if (Physics.Raycast(Rpos, direccion, out hit, radioDisparar))
            {
                if (hit.transform.gameObject.tag == "Personaje")
                {
                    VisionObstruida = false;
                }
                else
                    VisionObstruida = true;
            }
            foreach (Collider o in Obj)
            {
                if ((o.gameObject.tag == "Entorno" || o.gameObject.tag == "Enemigo" || Vector3.Distance(personaje.transform.position, Rpos) < 8f
                || Vector3.Distance(personaje.transform.position, Rpos) > radioDisparar) || VisionObstruida)
                {
                    EstaOcupado = true;
                }
            }
            if (!EstaOcupado)
            {
                StartCoroutine(IdleTarget(Rpos));
                TargetLock = true;
                break;

            }

        }



    }
    IEnumerator IdleTarget(Vector3 RPos)
    {

        anim.SetBool("Tepeo", true);

        while (!TerminoAnimacion)
        {
            yield return null;
        }

        transform.position = RPos;
        anim.SetBool("Tepeo", false);
        transform.localScale = Vector3.zero;
        DelayTeleport = DelayTeleportInicial;
        DelayTeleport -= Time.deltaTime;
        transform.LookAt(personaje.transform.position);
        TerminoAnimacion = false;
        Debug.DrawRay(transform.position, Vector3.up * 20, Color.magenta, 1);
    }

    IEnumerator IdleTeleport(Vector3 RPos)
    {
        anim.SetBool("Tepeo", true);
        
        while (!TerminoAnimacion)
        {
            yield return null;
        }

        transform.position = RPos;
        anim.SetBool("Tepeo", false);
        transform.localScale = Vector3.zero;
        DelayTeleport = DelayTeleportInicial;
        DelayTeleport -= Time.deltaTime;
        var euler = transform.eulerAngles;
        euler = Vector3.zero;
        euler.y = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = euler;
        TerminoAnimacion = false;
    }
    protected bool PuedoVer()
    {
        //hace un raycast al jugador y devuelve true si no hay nada entre el enemigo y el personaje

        personaje = GameObject.FindGameObjectWithTag("Personaje");
        var direccion2 = (personaje.transform.position - RayPos.position).normalized;
        Debug.DrawRay(RayPos.position, direccion2 * radioDisparar * 2, Color.blue);

        if (Physics.Raycast(RayPos.position, direccion2, out hit, radioDisparar * 2) && hit.collider.gameObject.tag == "Personaje")
        {
            return true;
        }
        else
        {

            return false;

        }
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

                return true;
            }

        }
        return false;
    }


    public void Apagar()
    {
        PuedoRestar = true;
        anim.SetBool("Atacando", false);
        //anim.speed = 5;
    }

    private void Disparar()
    {
        Estado = Estados[1];
        //Apuntar(personaje.transform.position);
        delay -= Time.deltaTime;
        if (Vector3.Distance(transform.position, personaje.transform.position) > 5)
        {
            Vector3 direction = (personaje.transform.position - CentroDelCuerpo.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(CentroDelCuerpo.rotation, lookRotation, Time.deltaTime * rotacion);
            Debug.DrawRay(CentroDelCuerpo.transform.position, direction * Vector3.Distance(CentroDelCuerpo.position, personaje.transform.position));
        }
        Vector3 dir = (personaje.transform.position - transform.position).normalized;
        if (Physics.Raycast(CentroDelCuerpo.position, dir, out hit, radioDisparar))
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

    IEnumerator ReproducirAnimacionDeTp()
    {
        anim.SetBool("Tepeo", true);
        //anim.speed = 5;
        while (!TerminoAnimacion)
        {
            yield return null;
        }
        anim.SetBool("Tepeo", false);
        TermineDeAparecer = false;

        //Vector3 c = new Vector3(Mathf.Epsilon, Mathf.Epsilon, Mathf.Epsilon);
        //transform.localScale = c;
        IdleTarget();
    }

    IEnumerator Disparo()
    {
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(ReproducirAnimacionDeTp());
    }
    private void Mori()
    {
        if (Vida < Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }



    void OnDrawGizmosSelected()

    {
        Gizmos.color = Color.cyan;


        Vector3 cubo = new Vector3(AreaIdle * 2, 2, AreaIdle * 2);
        Gizmos.DrawWireCube(posicionSpawn, cubo);
        Heredar = transform.Find(NombreHijo).GetComponent<Transform>();
        Gizmos.color = Color.red;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(VisionDisparo.position, radioDisparar);

    }
}
