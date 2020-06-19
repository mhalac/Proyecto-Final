using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorDeItems : MonoBehaviour {

	//Variable que guarda informacion del objeto del raycast
	RaycastHit DondeToco;
	//Layermask para ignorar la colision con el jugador
	int layerMask = 1 << 13;
	//Rango Del raycast
	float Rango = 5;

	//Variables para ocultar y mostrar el HUD
	static bool OcultadorDeHud = false;
	public GameObject HUDPasivas;
	public GameObject HUDEstadisticas;

	private GameObject Prueba1;

	private MusicaExplosiva musicaExplosiva;

	private GameObject ActivaDeFuego;
	private GameObject ActivaDeViento;
	private GameObject ActivaDeTierra;
	private GameObject ActivaDeAgua;

	// Use this for initialization
	void Start ()
	{
		Prueba1 = Resources.Load<GameObject>("Activas/ActivaDeFuego1");
		musicaExplosiva = GetComponent<MusicaExplosiva>();

		ActivaDeFuego = GameObject.Find("ActivaFuego");
		ActivaDeViento = GameObject.Find("ActivaViento");
		ActivaDeTierra = GameObject.Find("ActivaTierra");
		ActivaDeAgua = GameObject.Find("ActivaAgua");
	}
	
	// Update is called once per frame
	void Update ()
	{
		ActivadorDeHUD();
		RaycastItems();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(Camera.main.transform.position , Camera.main.transform.forward * Rango);
	}

	private void ActivadorDeHUD()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			OcultadorDeHud = !OcultadorDeHud;
		}

		if(OcultadorDeHud == true)
		{
			HUDPasivas.SetActive(true);
			HUDEstadisticas.SetActive(true);
		}
		else
		{
			HUDPasivas.SetActive(false);
			HUDEstadisticas.SetActive(false);
		}
		
	}

	private void RaycastItems()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward , out DondeToco , Rango , layerMask))
			{
				SlotsVacios();
			}
		}
	}

	private void SlotsVacios()
	{
		string Comparador = DondeToco.collider.tag;

		switch(Comparador)
		{
			case "ActivaDeFuego1":
			Destroy(DondeToco.collider.gameObject);
			ActivaDeFuego.GetComponent<Image>().sprite = Prueba1.GetComponent<MusicaExplosiva>().Icono;
			break;
		}
	}
}
