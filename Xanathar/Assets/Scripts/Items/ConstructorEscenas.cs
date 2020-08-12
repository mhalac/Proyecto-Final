using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConstructorEscenas 
{
	public List <GameObject> ObjetosDeEscena {get; set;}
	public List <Vector3> PosicionesDeEscena {get; set;}

	//Funciones para MODIFICAR VALORES
	public ConstructorEscenas(List<GameObject> objetos , List<Vector3> posiciones)
	{
		this.ObjetosDeEscena = objetos;
		this.PosicionesDeEscena = posiciones;
	}

	public void ModificadorDeObjetos(List <GameObject> Objetos)
	{
		this.ObjetosDeEscena = new List<GameObject>(Objetos);
		Debug.Log(this.ObjetosDeEscena.Count);
	}

	public void ModificadorDePosiciones(List <Vector3> Pos)
	{
		this.PosicionesDeEscena = new List<Vector3>(Pos);
		Debug.Log(this.PosicionesDeEscena.Count);
	}

	public void ReemplazarConstructorConOtro(ConstructorEscenas ConstructorEjemplo)
	{
		this = ConstructorEjemplo;
	}

	//Funciones para LEER VALORES Y CONSEGUIR VALORES

	public string ConseguirListaYPos()
	{
		string ObjetosYPos = "";
		ObjetosYPos += "Cantidad de Objetos: " + this.ObjetosDeEscena.Count + " ";
		ObjetosYPos += "CantidadDePosQueHay: " + this.PosicionesDeEscena.Count + " ";
		return ObjetosYPos;
	}

	public int ConseguirLargoDeLista(int IndexDeObjetos)
	{
		IndexDeObjetos = this.ObjetosDeEscena.Count;
		return IndexDeObjetos;
	}

	public List<GameObject> ConseguirListaDeObjetos(List<GameObject> ListaDeObjetos)
	{
		for(int i = 0; i < this.ObjetosDeEscena.Count; i++)
		{
			ListaDeObjetos.Add(this.ObjetosDeEscena[i]);
		}
		return ListaDeObjetos;
	}

	public List<Vector3> ConseguirListaDePosiciones(List<Vector3> ListaDePos)
	{
		for(int i = 0; i < this.PosicionesDeEscena.Count; i++)
		{
			ListaDePos.Add(this.PosicionesDeEscena[i]);
		}
		return ListaDePos;
	}
}
