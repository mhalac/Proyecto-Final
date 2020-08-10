using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConstructorEscenas 
{
	public string NombreDeEscena {get; set;}
	public List <GameObject> ObjetosDeEscena {get; set;}
	public List <Vector3> PosicionesDeEscena {get; set;}

	//Funciones para MODIFICAR VALORES
	public ConstructorEscenas(string nombre , List<GameObject> objetos , List<Vector3> posiciones)
	{
		this.NombreDeEscena = nombre;
		this.ObjetosDeEscena = objetos;
		this.PosicionesDeEscena = posiciones;
	}

	public void ModificadorDeObjetos(List <GameObject> Objetos)
	{
		this.ObjetosDeEscena = new List<GameObject>(Objetos);
	}

	public void ModificadorDePosiciones(List <Vector3> Pos)
	{
		this.PosicionesDeEscena = new List<Vector3>(Pos);
	}

	//Funciones para LEER VALORES

	public string ConseguirNombreDeEscena()
	{
		string NombreDelConstructor = "";
		NombreDelConstructor = this.NombreDeEscena;
		return NombreDeEscena;
	}

	public string ConseguirListaYPos()
	{
		string ObjetosYPos = "";
		ObjetosYPos += "Cantidad de Objetos: " + this.ObjetosDeEscena.Count + " ";
		ObjetosYPos += "CantidadDePosQueHay: " + this.PosicionesDeEscena.Count + " ";
		return ObjetosYPos;
	}
}
