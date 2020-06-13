using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fuego1 : MonoBehaviour {

 
    private RaycastHit hit;
    private bool Visto;
    private Transform UltimaPosicion;

    public Transform RayPos;
    private RaycastHit ray;
    public Transform VisionDisparo;
    
    private NavMeshAgent agente;
    public float rotacion;
    
    
    public int AlcanzeMaximo;
    public Transform RangoMinimo;

    public int radioDisparar;
    private int PMask;
    private GameObject personaje;
	// Use this for initialization
	void Start () {

        agente = GetComponent<NavMeshAgent>();
        PMask = LayerMask.NameToLayer("Personaje");
        VisionDisparo.position = new Vector3(VisionDisparo.position.x , VisionDisparo.position.y,VisionDisparo.position.z+ radioDisparar);
        RangoMinimo.position = new Vector3(RangoMinimo.position.x, RangoMinimo.position.y,RangoMinimo.position.z+ AlcanzeMaximo);
   
    }
	
	// Update is called once per frame
	void Update () {
     
        
        if(BuscarPersonaje())
        {
            Estadentro(TengoQueAcercarme());
        }
        else
            agente.isStopped = true;
        
        
       
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
        Apuntar(personaje.transform);
        print("bang bang");
    }

    private void Estadentro(bool TengoQueAcercarme)
    {
        print(TengoQueAcercarme);
        var direccion2 = personaje.transform.position - transform.position;
        Debug.DrawRay(transform.position, direccion2 , Color.yellow);


       
        if (TengoQueAcercarme)
        {
            agente.isStopped = false;
            Acercar();
        }
        if (Physics.Raycast(transform.position, direccion2, out hit, radioDisparar) && hit.collider.gameObject.tag != "Personaje")
        {  
            print("No veo me acerco");
            agente.isStopped = false;
            Acercar();       
        
        }

        
        else if (personaje != null && !TengoQueAcercarme)
        {
            agente.isStopped = true;
            Disparar();
        }
    }
    void OnDrawGizmosSelected()
    {
     
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(RangoMinimo.position, AlcanzeMaximo);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(VisionDisparo.position, radioDisparar);
    }
}
