using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorDeItems : MonoBehaviour {

	//Variable que guarda informacion del objeto del raycast
	RaycastHit DondeToco;
	//Layermask para ignorar la colision con el jugador
	int layerMask1 = 1 << 14;
	int layerMask2 = 1 << 15;
	int layerMask3 = 1 << 16;
	int layerMask4 = 1 << 17;
	int layermasFinal;
	//Rango Del raycast
	float Rango = 5;

	//Variables para ocultar y mostrar el HUD
	static bool OcultadorDeHud = false;
	public GameObject HUDPasivas;
	public GameObject HUDEstadisticas;


	//Referencias a las Activas del juego
	private GameObject ActivaDeFuego1;
	private GameObject ActivaDeFuego2;
	private GameObject ActivaDeViento1;
	private GameObject ActivaDeViento2;
	private GameObject ActivaDeTierra1;
	private GameObject ActivaDeTierra2;
	private GameObject ActivaDeAgua1;
	private GameObject ActivaDeAgua2;

	//Referencias a los slots de Activas del HUD
	private GameObject SlotActivaDeFuego;
	private GameObject SlotActivaDeViento;
	private GameObject SlotActivaDeTierra;
	private GameObject SlotActivaDeAgua;

	//Lista De Activas que nos permite chequear si los slots estan llenos
	static bool[] ArrayDeActivas = new bool[]{ false, false, false, false };

	// Use this for initialization
	void Start ()
	{
		layermasFinal = layerMask1 | layerMask2 | layerMask3 | layerMask4;
		//Encuentro los prefabs de las activas y accedo a sus variables mas adelante
		ActivaDeFuego1 = Resources.Load<GameObject>("Activas/ActivaDeFuego1");
		ActivaDeFuego2 = Resources.Load<GameObject>("Activas/ActivaDeFuego2");

		ActivaDeViento1 = Resources.Load<GameObject>("Activas/ActivaDeViento1");
		ActivaDeViento2 = Resources.Load<GameObject>("Activas/ActivaDeViento2");

		ActivaDeTierra1 = Resources.Load<GameObject>("Activas/ActivaDeTierra1");
		ActivaDeTierra2 = Resources.Load<GameObject>("Activas/ActivaDeTierra2");

		ActivaDeAgua1 = Resources.Load<GameObject>("Activas/ActivaDeAgua1");
		ActivaDeAgua2 = Resources.Load<GameObject>("Activas/ActivaDeAgua2");

		//Encuentro el Slot para referenciarlo y acceder a sus variables mas adelante
		SlotActivaDeFuego = GameObject.Find("ActivaFuego");
		SlotActivaDeViento = GameObject.Find("ActivaViento");
		SlotActivaDeTierra = GameObject.Find("ActivaTierra");
		SlotActivaDeAgua = GameObject.Find("ActivaAgua");
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
		if(Input.GetKeyDown(KeyCode.E) && Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward , out DondeToco , Rango , layermasFinal))
		{
			EquipadorActivas();
		}
	}

	private void EquipadorActivas()
	{
		string Comparador = DondeToco.collider.tag;
		switch(Comparador)
		{
			case "ActivaDeFuego1":

			if(ArrayDeActivas[0] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeFuego.GetComponent<Image>().sprite = ActivaDeFuego1.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[0] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeFuego.GetComponent<Image>().sprite = ActivaDeFuego1.GetComponent<InformacionDeItems>().Icono;
				Instantiate(ActivaDeFuego2 , transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeFuego2":

			if(ArrayDeActivas[0] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeFuego.GetComponent<Image>().sprite = ActivaDeFuego2.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[0] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeFuego.GetComponent<Image>().sprite = ActivaDeFuego2.GetComponent<InformacionDeItems>().Icono;
				Instantiate(ActivaDeFuego1 , transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeViento1":

			if(ArrayDeActivas[1] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeViento.GetComponent<Image>().sprite = ActivaDeViento1.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[1] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeViento.GetComponent<Image>().sprite = ActivaDeViento1.GetComponent<InformacionDeItems>().Icono;
				Instantiate(ActivaDeViento2 , transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeViento2":

			if(ArrayDeActivas[1] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeViento.GetComponent<Image>().sprite = ActivaDeViento2.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[1] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeViento.GetComponent<Image>().sprite = ActivaDeViento2.GetComponent<InformacionDeItems>().Icono;
				Instantiate(ActivaDeViento1 , transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeTierra1":
			if(ArrayDeActivas[2] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeTierra.GetComponent<Image>().sprite = ActivaDeTierra1.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[2] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeTierra.GetComponent<Image>().sprite = ActivaDeTierra1.GetComponent<InformacionDeItems>().Icono;
				Instantiate(ActivaDeTierra2 , transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeTierra2":
			if(ArrayDeActivas[2] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeTierra.GetComponent<Image>().sprite = ActivaDeTierra2.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[2] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeTierra.GetComponent<Image>().sprite = ActivaDeTierra2.GetComponent<InformacionDeItems>().Icono;
				Instantiate(ActivaDeTierra1 , transform.position , Quaternion.identity);
			}
			break;


			case "ActivaDeAgua1":

			if(ArrayDeActivas[3] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeAgua.GetComponent<Image>().sprite = ActivaDeAgua1.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[3] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeAgua.GetComponent<Image>().sprite = ActivaDeAgua1.GetComponent<InformacionDeItems>().Icono;
				Instantiate(ActivaDeAgua2 , transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeAgua2":

			if(ArrayDeActivas[3] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeAgua.GetComponent<Image>().sprite = ActivaDeAgua2.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[3] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeAgua.GetComponent<Image>().sprite = ActivaDeAgua2.GetComponent<InformacionDeItems>().Icono;
				Instantiate(ActivaDeAgua1 , transform.position , Quaternion.identity);
			}
			break;
		}
	}
}