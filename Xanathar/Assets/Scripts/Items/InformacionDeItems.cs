using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformacionDeItems : MonoBehaviour {

	public string Nombre;
	public string Descripcion;
	public string Categoria;
	public string Elemento;
	public Sprite Icono;
	
	//Esta funcion no la uso pero la tengo en caso de hacer algo parecido, no tener que volver a hacerla de cero
	/*
	IEnumerator PararDeMoverse()
	{
		yield return new WaitForSeconds(1f);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.drag = 0;
		CorrutinaObjetos = false;
		CorrutinaPiso = false;
	}
	*/
}
