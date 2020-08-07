using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformacionDeItems : MonoBehaviour {

	bool CorrutinaObjetos = false;
	bool CorrutinaPiso = false;
	Rigidbody rb;
	public float AreaDeAdios;
	public string Nombre;
	public string Descripcion;
	public string Categoria;
	public string Elemento;
	public Sprite Icono;
	public bool PararCorrutina = false;

	//
	public float velocidad;
	public float angularVelocity;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	
	void Update()
	{
		/*
		Collider[] hitcollider = Physics.OverlapSphere (transform.position , AreaDeAdios);

		foreach(Collider hit in hitcollider)
		{
			if(hit.gameObject != this.gameObject && hit.tag == "Items")
			{
				Vector3 Retroceder = this.transform.position - 	hit.gameObject.transform.position;
				rb.velocity = Vector3.Lerp(rb.velocity , Retroceder , Time.deltaTime * 10);

				if(CorrutinaObjetos == false)
				{
					StartCoroutine(PararDeMoverse());
					CorrutinaObjetos = true;
				}
			}
				
			if(hit.gameObject != this.gameObject && hit.gameObject.layer == 10)
			{
				Vector3 MoverseArriba = this.transform.position - hit.gameObject.transform.position;
				rb.velocity = Vector3.Lerp(transform.up , MoverseArriba, Time.deltaTime * 10);

				if(CorrutinaPiso == false)
				{
					StartCoroutine(PararDeMoverse());
					CorrutinaPiso = true;
				}
			}
		}
		*/
	}
	
	/*
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(gameObject.transform.position, AreaDeAdios);
	}
	*/

	IEnumerator PararDeMoverse()
	{
		yield return new WaitForSeconds(1f);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.drag = 0;
		CorrutinaObjetos = false;
		CorrutinaPiso = false;
	}
}
