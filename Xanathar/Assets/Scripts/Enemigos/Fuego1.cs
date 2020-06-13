using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fuego1 : MonoBehaviour {

    public Transform vision;
    
   
    public float visionOffset;
    public Transform RayPos;
    private RaycastHit ray;
    public float velocidad;
    private float distancia;
    private NavMeshAgent agente;
    public float rotacion;
    public int radioHuir;
    public int radioDisparar;
    private int PMask;
    private GameObject personaje;
	// Use this for initialization
	void Start () {

        agente = GetComponent<NavMeshAgent>();
        PMask = LayerMask.NameToLayer("Personaje");
        
           
    }
	
	// Update is called once per frame
	void Update () {
        
        
        //Busca a el personaje
        Collider[] obj = Physics.OverlapSphere(RayPos.position, radioDisparar);
        for (int i = 0; obj.Length > i;i++)
        {
            if (obj[i].gameObject.layer == PMask)
            {
                personaje = obj[i].gameObject;
                var direccion = personaje.transform.position - transform.position ;
                
                distancia = Vector3.Distance(personaje.transform.position, this.gameObject.transform.position);
                Debug.DrawRay(RayPos.position,direccion , Color.green);   
                Estadentro();
            }
            else
                agente.isStopped = true;
        }
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

    private void Estadentro()
    {
        var direccion2 = personaje.transform.position - transform.position ;
        if(!Physics.Raycast(RayPos.position,direccion2,radioDisparar,PMask))
        {
            agente.destination = personaje.transform.position;
        }
        

        if (personaje != null && distancia < radioDisparar && distancia > radioHuir)
        {
            agente.isStopped = false;
            Acercar();
        }
    
        
        else if (personaje != null && distancia < radioHuir)
        {
            agente.isStopped = true;
            Disparar();
        }
    }
    void OnDrawGizmosSelected()
    {
     
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(RayPos.position, radioHuir);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(RayPos.position, radioDisparar);
    }
}
