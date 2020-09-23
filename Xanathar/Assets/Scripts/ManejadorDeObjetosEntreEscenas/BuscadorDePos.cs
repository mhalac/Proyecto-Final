﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscadorDePos : MonoBehaviour {

	GameObject PosInicial;
	GameObject PosFinal;
	GameObject ContenedorPos;

	GameObject PosNivelFuego;
	GameObject PosNivelTierra;

	ManejadorDeEscenas Manejador;

	GameObject Jugador;
	float PosicionX;
	float PosicionY;
	float PosicionZ;
	float RotacionY;
	// Use this for initialization
	void Start ()
	{
		
	}

	public void ManejadorDePos()
	{
		PosInicial = GameObject.Find("PosicionInicial");
		PosFinal = GameObject.Find("PosicionFinal");
		Jugador = GameObject.FindGameObjectWithTag("Personaje");

		if(PosInicial != null && PosFinal != null)
		{
			if(ManejadorDeEscenas.EntrasteOSaliste == true)
			{
				PosicionX = Mathf.RoundToInt(PosInicial.transform.position.x);
				PosicionY = Mathf.RoundToInt(PosInicial.transform.position.y);
				PosicionZ = Mathf.RoundToInt(PosInicial.transform.position.z);
		
				RotacionY = Mathf.RoundToInt(PosInicial.transform.eulerAngles.y);
			}
			else
			{
				PosicionX = Mathf.RoundToInt(PosFinal.transform.position.x);
				PosicionY = Mathf.RoundToInt(PosFinal.transform.position.y);
				PosicionZ = Mathf.RoundToInt(PosFinal.transform.position.z);
		
				RotacionY = Mathf.RoundToInt(PosFinal.transform.eulerAngles.y);
			}

			Jugador.transform.position = new Vector3(PosicionX,PosicionY,PosicionZ);
			Jugador.transform.rotation = Quaternion.Euler(0f,RotacionY,0f);
		}
		else
		{
			PosNivelFuego = GameObject.Find("PosicionFuego");
			PosNivelTierra = GameObject.Find("PosicionTierra");

			if(ManejadorDeEscenas.EntreNivelFuego == true)
			{
				PosicionX = Mathf.RoundToInt(PosNivelFuego.transform.position.x);
				PosicionY = Mathf.RoundToInt(PosNivelFuego.transform.position.y);
				PosicionZ = Mathf.RoundToInt(PosNivelFuego.transform.position.z);
		
				RotacionY = Mathf.RoundToInt(PosNivelFuego.transform.eulerAngles.y);

				Jugador.transform.position = new Vector3(PosicionX,PosicionY,PosicionZ);
				Jugador.transform.rotation = Quaternion.Euler(0f,RotacionY,0f);

				ManejadorDeEscenas.EntreNivelFuego = false;
			}
			else if(ManejadorDeEscenas.EntreNivelTierra == true)
			{
				PosicionX = Mathf.RoundToInt(PosNivelTierra.transform.position.x);
				PosicionY = Mathf.RoundToInt(PosNivelTierra.transform.position.y);
				PosicionZ = Mathf.RoundToInt(PosNivelTierra.transform.position.z);
		
				RotacionY = Mathf.RoundToInt(PosNivelTierra.transform.eulerAngles.y);

				Jugador.transform.position = new Vector3(PosicionX,PosicionY,PosicionZ);
				Jugador.transform.rotation = Quaternion.Euler(0f,RotacionY,0f);

				ManejadorDeEscenas.EntreNivelTierra = false;
			}
			else
			{
				ContenedorPos = GameObject.Find("ContenedorPos");

				PosicionX = Mathf.RoundToInt(ContenedorPos.transform.position.x);
				PosicionY = Mathf.RoundToInt(ContenedorPos.transform.position.y);
				PosicionZ = Mathf.RoundToInt(ContenedorPos.transform.position.z);
		
				RotacionY = Mathf.RoundToInt(ContenedorPos.transform.eulerAngles.y);

				Jugador.transform.position = new Vector3(PosicionX,PosicionY,PosicionZ);
				Jugador.transform.rotation = Quaternion.Euler(0f,RotacionY,0f);
			}
		}
	}
}
