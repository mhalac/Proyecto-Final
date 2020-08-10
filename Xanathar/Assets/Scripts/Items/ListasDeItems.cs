using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListasDeItems : MonoBehaviour {

	public static List <GameObject> ObjetosEnEscena;
	public static List <Vector3> PosicionesEnEscena;
	public List <ConstructorEscenas> ListaContenedoraDelConstructor = new List<ConstructorEscenas>();

	//Constructores De Los Niveles
	ConstructorEscenas SubNivelFuego1;
	ConstructorEscenas SubNivelFuego2;
	ConstructorEscenas SubNivelFuego3;
	ConstructorEscenas SubNivelFuego4;



	// Use this for initialization
	void Start ()
	{

		ObjetosEnEscena = new List<GameObject>(1);
		PosicionesEnEscena = new List<Vector3>(1);

		SubNivelFuego2 = new ConstructorEscenas("SubNivelFuego2" , ObjetosEnEscena , PosicionesEnEscena);

		Debug.Log(SubNivelFuego2.ConseguirListaYPos());
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			List <GameObject> ListaDeObjetos = new List <GameObject>(GameObject.FindGameObjectsWithTag("Items"));

			List <Vector3> ListaDePos = new List<Vector3>();

			for(int i = 0; i < ListaDeObjetos.Count; i++)
			{
				ListaDePos.Add(ListaDeObjetos[i].transform.position);
			}

			SubNivelFuego2.ModificadorDeObjetos(ListaDeObjetos);
			SubNivelFuego2.ModificadorDePosiciones(ListaDePos);

			Debug.Log(SubNivelFuego2.ConseguirListaYPos());
		}
	}

	private void CrearLosConstructores()
	{
		
	}
}
