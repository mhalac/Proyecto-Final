using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuego1 : MonoBehaviour {


    private float distancia;
    private Rigidbody rb;
    public int radioHuir;
    public int radioDisparar;
    private int PMask;
    private GameObject personaje;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        PMask = LayerMask.NameToLayer("Personaje");
    }
	
	// Update is called once per frame
	void Update () {

        
        //Busca a el personaje
        Collider[] obj = Physics.OverlapSphere(transform.position, radioDisparar);
        for (int i = 0; obj.Length > i;i++)
        {
            if (obj[i].gameObject.layer == PMask)
            {
                personaje = obj[i].gameObject;
                 distancia = Vector3.Distance(personaje.transform.position, this.gameObject.transform.position);
                Estadentro();
            }
            else
                personaje = null;
        }
        
       

    }
    private void Huir()
    {
        print("huyo");
    }

    private void Estadentro()
    {
        if (personaje != null)
        {
            print("EXSISTE");
        }
        
        if (personaje != null && distancia < radioDisparar && distancia > radioHuir)
        {
            print("bang");
        }

        else if (personaje != null && distancia < radioHuir)
        {
            Huir();
        }
    }
    void OnDrawGizmosSelected()
    {
     
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioHuir);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioDisparar);
    }
}
