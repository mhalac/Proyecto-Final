using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformacionDeItems : MonoBehaviour {

	bool Corrutina = false;
	Rigidbody rb;
	public float AreaDeAdios;
	public string Nombre;
	public string Descripcion;
	public string Categoria;
	public string Elemento;
	public Sprite Icono;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	void Update()
	{
		Collider[] hitcollider = Physics.OverlapSphere (transform.position , AreaDeAdios);

		foreach(Collider hit in hitcollider)
		{
			if(hit.gameObject != this.gameObject && hit.tag == "Items")
			{
				Vector3 Retroceder = this.transform.position - 	hit.gameObject.transform.position;
				rb.velocity = Vector3.Lerp(rb.velocity , Retroceder , Time.deltaTime * 2);

				if(Corrutina == false)
				{
					StartCoroutine(PararDeMoverse());
					Corrutina = true;
				}
			}
		}
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(gameObject.transform.position, AreaDeAdios);
	}

	IEnumerator PararDeMoverse()
	{
		yield return new WaitForSeconds(0.5f);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.drag = 0;
		Corrutina = false;
		//print("Esto se ejecuto");
	}
}
