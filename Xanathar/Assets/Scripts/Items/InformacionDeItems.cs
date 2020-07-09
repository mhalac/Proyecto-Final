using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformacionDeItems : MonoBehaviour {

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
				print("hola");
				Vector3 Retroceder = this.transform.position - 	hit.gameObject.transform.position;
				rb.velocity = Vector3.Lerp(rb.velocity , Retroceder , Time.deltaTime);
			}
		}
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(gameObject.transform.position, AreaDeAdios);
	}

	private void Cosas()
	{
		print("asder");
		rb.AddForce(transform.forward * 0.1f);
		StartCoroutine(AddDrag());
	}

	IEnumerator AddDrag()
	{
		float CurrentDrag = 0;
		float MaxDrag = 1;

		while (CurrentDrag < MaxDrag)
		{
			CurrentDrag += Time.deltaTime;
			rb.drag = CurrentDrag;
			yield return null;
		}

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.drag = 0;
	}
}
