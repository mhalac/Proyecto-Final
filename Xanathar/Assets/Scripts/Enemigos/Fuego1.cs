using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fuego1 : MonoBehaviour
{


    private RaycastHit hit;
    private bool Visto;
    private Transform UltimaPosicion;
    

    public GameObject balaPrefab;

    public float AreaIdle;
    public Transform RayPos;
    private RaycastHit ray;
    public Transform VisionDisparo;
    private string Estado;
    private NavMeshAgent agente;
    public float rotacion;
    private bool PerdidoDeVista;
    private string[] Estados = { "Idle", "Chasing", "Searching", "Shooting" };
    public int AlcanzeMaximo;
    public Transform RangoMinimo;
    private Transform posicionSpawn;
    private bool Moviendose;

    private float DelayInicial;
    public float BalaVelocidad;
    public Vector3 destino;

    public float delay;
    public int radioDisparar;
    private int PMask;
    private GameObject personaje;
    private Vector3 posicionRandom;
    // Use this for initialization
    void Start()
    {
        //Guardas la posicion de spawn, obtenes el navmesh, asignas la FOV del enemigo sumando su radio asi se genera en el borde del enemigo
        //hacemos que su estado sea el [0], que es Idle
        // Y tmb generas un vector3 de las posiciones donde se van a generar los lugares a los que va a ir mientras este en idle
        
        posicionSpawn = transform;
        agente = GetComponent<NavMeshAgent>();
        PMask = LayerMask.NameToLayer("Personaje");
        VisionDisparo.position = new Vector3(VisionDisparo.position.x, VisionDisparo.position.y, VisionDisparo.position.z + radioDisparar);
        RangoMinimo.position = new Vector3(RangoMinimo.position.x, RangoMinimo.position.y, RangoMinimo.position.z + AlcanzeMaximo);
        Estado = Estados[0];
        posicionRandom = new Vector3(posicionSpawn.transform.position.x, posicionSpawn.transform.position.y, posicionSpawn.transform.position.z);
        DelayInicial = delay;
        
    }

    // Update is called once per frame
    void Update()
    {

        //Si el personaje esta en rango pero no lo puede ver y previamente estaba disparandole lo empieza a buscar
        if (BuscarPersonaje() && !PuedoVer() && Estado == Estados[3])
        {
            Estado = Estados[2];
            UltimaPosicion = personaje.transform;
            Buscar();
        }
        
        //Si estaba buscando y llega al punto donde lo iba a buscar se va IDLE de vuelta
        if (agente.remainingDistance < Mathf.Epsilon && Estado == Estados[2])
        {
            transform.rotation = UltimaPosicion.rotation;
            Estado = Estados[0];
        }
         
        if (Estado == Estados[1] && !PuedoVer() && BuscarPersonaje())
        {
            print("Corri2");
            Estado = Estados[2];
            UltimaPosicion = personaje.transform;
            Buscar();
        }
        
        // Si lo estaba persiguiendo
        if (BuscarPersonaje() && Estado == Estados[1] && !PuedoVer())
        {
            UltimaPosicion = personaje.transform;
            Buscar();
        }
        // Si esta en tu rango y no lo estabas buscando, y tambien lo podes ver,
        if (BuscarPersonaje() && Estado != Estados[2])
        {
            if (PuedoVer())
            {
                Estadentro(TengoQueAcercarme());
            }
        }


        if (!BuscarPersonaje() && Estado != Estados[2])
        {
            agente.isStopped = true;
            Estado = Estados[0];
        }
        if (Estado == Estados[0])
        {

            Idle();
        }
    }

    private void Idle()
    {
        agente.isStopped = false;

        if (!Moviendose)
        {
            float RandomX = 0;
            float RandomZ = 0;
            bool seCreo = false;
            bool hayPared = false;
            //genera numero random entre tu posicion y el rango espesificado de idle
            while (!seCreo)
            {
                RandomX = Random.Range(posicionRandom.x - AreaIdle, AreaIdle + posicionRandom.z);
                RandomZ = Random.Range(posicionRandom.x - AreaIdle, AreaIdle + posicionRandom.z);
                destino = new Vector3(RandomX, transform.position.y, RandomZ);
                // revisas que el punto para ir no este en una pared

                Collider[] obj = Physics.OverlapSphere(destino, 2f);
                for (int i = 0; i < obj.Length; i++)
                {
                    if (obj[i].tag == "Obstaculo")
                    {
                        hayPared = true;
                    }

                }
                if (!hayPared)
                {
                    seCreo = true;
                }
            }

            destino = new Vector3(RandomX, transform.position.y, RandomZ);
            agente.destination = destino;
            Moviendose = true;
        }
        if (agente.remainingDistance < Mathf.Epsilon)
        {
            Moviendose = false;

        }
    }

    private void Buscar()
    {
        //El enemigo se va a dirigir a la ultima posicion de donde estaba el jugador
        agente.isStopped = false;
        Estado = Estados[2];
        agente.destination = UltimaPosicion.position;

    }
    protected bool PuedoVer()
    {
        //hace un raycast al jugador y devuelve true si no hay nada entre el enemigo y el personaje
        
        var direccion2 = personaje.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direccion2, out hit, radioDisparar) && hit.collider.gameObject.tag != "Personaje")
        {
            return false;
        }
        else
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
                var direccion = personaje.transform.position - transform.position;


                Debug.DrawRay(transform.position, direccion * hit.distance, Color.yellow);
                return true;
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

        }
        return true;
    }
    private void Acercar()
    {

        //gameObject.transform.localPosition = Vector3.MoveTowards(transform.position, personaje.transform.position,velocidad * Time.deltaTime);
        agente.destination = personaje.transform.position;
    }
    private void Apuntar(Transform enemigo)
    {
        Vector3 direction = (enemigo.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotacion);
    }
    private void Disparar()
    {
        Estado = Estados[3];
        Apuntar(personaje.transform);
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            var direccion = personaje.transform.position - transform.position;
            delay = DelayInicial;
            GameObject bala = Instantiate(balaPrefab, RayPos.position, Quaternion.identity);
            //Vector3 PosicionDisparada = personaje.transform.position;

            bala.GetComponent<ProyectilBase>().Lanzar(direccion, BalaVelocidad);

        }
    }

    private void Estadentro(bool TengoQueAcercarme)
    {

       
        var direccion2 = personaje.transform.position - transform.position;
        Debug.DrawRay(transform.position, direccion2, Color.yellow);
        // Cambia tu estado a "Chasing" pq si esta adentro y lo podes ver es que lo tenes que estar periguiendo 
        if (Estado != Estados[2])
        {
            Estado = Estados[1];
        }

        //Si lo estas persiguiendo pero no esta a tu rango de disparo te empezas a acercar para dispararle
        if (TengoQueAcercarme)
        {
            agente.isStopped = false;
            Acercar();
        }
        //Si se fue de tu rango pero lo estabas persiguiendo lo empezas a buscar
        if (!BuscarPersonaje() && Estado == Estados[1])
        {
            Buscar();
        }

        //si no te tenes que acercar entonces lo acribillas y tu estado es disparando
        else if (personaje != null && !TengoQueAcercarme)
        {
            Estado = Estados[0];
            agente.isStopped = true;
            Disparar();
        }
    }
    void OnDrawGizmosSelected()
    {


        Gizmos.color = Color.blue;

        //Vector3 cubo = new Vector3(AreaIdle, 2,AreaIdle);
        //Gizmos.DrawWireCube(transform.position,cubo);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destino, 2f);
        Gizmos.DrawWireSphere(RangoMinimo.position, AlcanzeMaximo);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(VisionDisparo.position, radioDisparar);

    }
}
