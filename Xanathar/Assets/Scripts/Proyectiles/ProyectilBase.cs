using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilBase : MonoBehaviour {

	private Rigidbody rb;
	private bool AlcanzoFinal = false;
	private int PMask;
	private Vector3 IrPosicion;
	private float velocidad;

	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.isKinematic = true;
		PMask = LayerMask.NameToLayer("Personaje");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!AlcanzoFinal)
		{
			Vector3 direction = (IrPosicion - transform.position).normalized;
        	Quaternion lookRotation = Quaternion.LookRotation(direction);
        	transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 9999);
		}
		Lanzar(IrPosicion,velocidad);
	}
	private bool Colisiono()
	{
		Collider[] obj = Physics.OverlapSphere(transform.position, 1f);
        for (int i = 0; obj.Length > i;i++)
        {
            if (obj[i].gameObject.layer == PMask)
            {
				return true;
            }
		}
		
		return false;	
	}

	public void Lanzar(Vector3 Objetivo, float speed)
	{
		float distancia = Vector3.Distance(Objetivo,transform.position);
		print("Distancia "+ distancia);
		if(!AlcanzoFinal)
		{
			Objetivo = new Vector3(Objetivo.x ,Objetivo.y,Objetivo.z);
			transform.position = Vector3.MoveTowards(transform.position, Objetivo,speed * Time.deltaTime);
			IrPosicion = Objetivo;
			velocidad = speed;
		}		
		Destroy(gameObject,5);
		
		if(Colisiono())
		{
			//Hacer damage cuando este hecho
			Destroy(gameObject);
		}
		if(distancia < 0.1)
		{
			print("Alcanzo Final");
			AlcanzoFinal = true;
			rb.isKinematic = true;
			
			//Objetivo = new Vector3(Objetivo.x,Objetivo.y,Objetivo.z + 99999);
			//print(Objetivo);
		}
		
			
	}
}
