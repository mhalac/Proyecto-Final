using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformacionDeItems : MonoBehaviour {

	public float AreaDeAdios;
	public string Nombre;
	public string Descripcion;
	public string Categoria;
	public string Elemento;
	public Sprite Icono;
	
	
	void Update()
	{
		Collider[] hitcollider = Physics.OverlapSphere (transform.position , AreaDeAdios);

		foreach(Collider hit in hitcollider)
		{
			if(hit.gameObject != this.gameObject)
			{
				print("hola");
			}
		}
	}
	

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;

		Gizmos.DrawWireSphere(gameObject.transform.position, AreaDeAdios);
	}
}
