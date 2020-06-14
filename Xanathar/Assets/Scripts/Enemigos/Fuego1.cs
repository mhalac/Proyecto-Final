using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fuego1 : MonoBehaviour {

 
    private RaycastHit hit;
    private bool Visto;
    private Transform UltimaPosicion;

    public float AreaIdle;
    public Transform RayPos;
    private RaycastHit ray;
    public Transform VisionDisparo;
    private string Estado;
    private NavMeshAgent agente;
    public float rotacion;
    private bool PerdidoDeVista;
    private string[] Estados = {"Idle","Chasing","Searching","Shooting"};
    public int AlcanzeMaximo;
    public Transform RangoMinimo;
    private Transform posicionSpawn ;
    private bool Moviendose;
    
    public Vector3 destino;

    public int radioDisparar;
    private int PMask;
    private GameObject personaje;
    private Vector3 posicionRandom;
	// Use this for initialization
	void Start () {
        posicionSpawn = transform;
        agente = GetComponent<NavMeshAgent>();
        PMask = LayerMask.NameToLayer("Personaje");
        VisionDisparo.position = new Vector3(VisionDisparo.position.x , VisionDisparo.position.y,VisionDisparo.position.z+ radioDisparar);
        RangoMinimo.position = new Vector3(RangoMinimo.position.x, RangoMinimo.position.y,RangoMinimo.position.z+ AlcanzeMaximo);
        Estado = Estados[0];
        posicionRandom = new Vector3(posicionSpawn.transform.position.x,posicionSpawn.transform.position.y,posicionSpawn.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        if(personaje != null)
        {
            print(personaje.gameObject.name);
        }
        
        //print(BuscarPersonaje() + "<Buscar - Puedo verlo> " + PuedoVer() +  "estado: "+ Estado);
        if(BuscarPersonaje() && !PuedoVer() && Estado == Estados[3])
        {
            Estado = Estados[2];
            print("Se actualizo la ultima posicion");
            UltimaPosicion = personaje.transform;
            Buscar();
        }
        if(agente.remainingDistance < Mathf.Epsilon && Estado == Estados[2])
        {
            Estado = Estados[0];
        }
        if(Estado == Estados[1] && !PuedoVer() && BuscarPersonaje())
        {
            Estado = Estados[2];
        }
       if(BuscarPersonaje() && Estado == Estados[1] && !PuedoVer())
        {
            print("Se actualizo la ultima posicion");
            UltimaPosicion = personaje.transform;
            Buscar();
        }

        if(BuscarPersonaje() && Estado != Estados[2])
        {
            if(PuedoVer())
            {
                Estadentro(TengoQueAcercarme());
            }
        }
       
    
        if(!BuscarPersonaje() && Estado != Estados[2])
        { 
            agente.isStopped = true;
            Estado = Estados[0];
        }
        if(Estado == Estados[0])
        {
           print("Idle");
            Idle();
      }
    }
    
    private void Idle()
    {
        agente.isStopped = false;
       
        if(!Moviendose)
        {
            float RandomX = 0;
            float RandomZ = 0;
            bool seCreo = false;
            bool hayPared = false;
            //genera numero random entre tu posicion y el rango espesificado de idle
            while(!seCreo)
            {
                RandomX = Random.Range(posicionRandom.x - AreaIdle,AreaIdle + posicionRandom.z);
                RandomZ = Random.Range(posicionRandom.x - AreaIdle,AreaIdle + posicionRandom.z);
                destino = new Vector3(RandomX,transform.position.y,RandomZ);
                // revisas que el punto para ir no este en una pared
                
                Collider[] obj = Physics.OverlapSphere(destino, 2f);
                for(int i = 0; i < obj.Length;i++)
                {
                    if(obj[i].tag == "Obstaculo")
                    {
                        hayPared = true;
                    }

                }
                if(!hayPared)
                {
                    seCreo = true;
                }
                
               
            }
            
            destino = new Vector3(RandomX,transform.position.y,RandomZ);
            agente.destination = destino;
            Moviendose = true;
        }
        if(agente.remainingDistance < Mathf.Epsilon)
        {
            Moviendose = false;
            StartCoroutine("Esperar");
        }
    }
    
    private void Buscar()
    {
        agente.isStopped = false;
        Estado = Estados[2];
       
        agente.destination = UltimaPosicion.position;
        
    }
    protected bool PuedoVer()
    {
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
        Collider[] obj = Physics.OverlapSphere(VisionDisparo.position, radioDisparar);
        for (int i = 0; obj.Length > i;i++)
        {
            if (obj[i].gameObject.layer == PMask)
            {
                personaje = obj[i].gameObject;
                var direccion = personaje.transform.position - transform.position ;
                
                
                Debug.DrawRay(transform.position, direccion * hit.distance, Color.yellow); 
                return true;
            }
            
        }
        return false;
    }

    private bool TengoQueAcercarme()
    {
        Collider[] objdisparo = Physics.OverlapSphere(RangoMinimo.position, AlcanzeMaximo);
        
        for (int i = 0; objdisparo.Length > i;i++)
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
    }

    private void Estadentro(bool TengoQueAcercarme)
    {
       // print("Tengo que acercarme? : " + TengoQueAcercarme);
        
        var direccion2 = personaje.transform.position - transform.position;
        Debug.DrawRay(transform.position, direccion2 , Color.yellow);
        if(Estado != Estados[2])
        {
            Estado = Estados[1];
        }

       
        if (TengoQueAcercarme)
        {
            agente.isStopped = false;
            Acercar();
        }
        if(!BuscarPersonaje() && Estado == Estados[1])
        {
            Buscar();
        }
        
        /*if (Physics.Raycast(transform.position, direccion2, out hit, radioDisparar) && hit.collider.gameObject.tag != "Personaje")
        {  
            print("No veo me acerco");
            agente.isStopped = false;
            Acercar();       
        
        }*/
       
        
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
        
        Vector3 cubo = new Vector3(AreaIdle, 2,AreaIdle);
        Gizmos.DrawWireCube(transform.position,cubo);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destino, 2f);
        Gizmos.DrawWireSphere(RangoMinimo.position, AlcanzeMaximo);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(VisionDisparo.position, radioDisparar);
        
    }
     IEnumerator Esperar()
    {
        
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        
        yield return new WaitForSeconds(3);

        
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
