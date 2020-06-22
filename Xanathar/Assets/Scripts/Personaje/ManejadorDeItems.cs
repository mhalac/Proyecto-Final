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
	int layermaskFinal;
	//Rango Del raycast
	float Rango = 5;

	//Variables para ocultar y mostrar el HUD
	static bool OcultadorDeHud = false;
	private bool OcultadorDeMensaje = false;
	public CanvasGroup HUDPasivas;
	public CanvasGroup HUDEstadisticas;
	public CanvasGroup MensajeNotificador;

	//Referencias a los slots de Activas del HUD
	private GameObject SlotActivaDeFuego;
	private GameObject SlotActivaDeViento;
	private GameObject SlotActivaDeTierra;
	private GameObject SlotActivaDeAgua;

	//Notifico las variables para reemplazar su texto mas adelante
	private GameObject NombreNotificador;
	private GameObject CategoriaNotificador;
	private GameObject DescripcionNotificador;


	//Instanciador de donde nuestros objetos apareceran
	private GameObject Instanciador;

	//Lista De Activas que nos permite chequear si los slots estan llenos
	//Fuego , Viento , Tierra, Agua
	static bool[] ArrayDeActivas = new bool[]{ false, false, false, false };

	int OrdenDeLayers = 0;

	GameObject[] Activas;

	// Use this for initialization
	void Start ()
	{
		MensajeNotificador.alpha = 0f;
		//Fusiono las layermasks en una sola
		layermaskFinal = layerMask1 | layerMask2 | layerMask3 | layerMask4;

		//Lleno mi Array de los items activos
		Activas = Resources.LoadAll<GameObject>("Activas");

		//Encuentro el Slot para referenciarlo y acceder a sus variables mas adelante
		SlotActivaDeFuego = GameObject.Find("ActivaFuego");
		SlotActivaDeViento = GameObject.Find("ActivaViento");
		SlotActivaDeTierra = GameObject.Find("ActivaTierra");
		SlotActivaDeAgua = GameObject.Find("ActivaAgua");

		//Encuentro los campos notifcadores para acceder a sus variables mas adelante
		NombreNotificador = GameObject.Find("NombreDelItem");
		CategoriaNotificador = GameObject.Find("CategoriaDelItem");
		DescripcionNotificador = GameObject.Find("DescripcionDelItem");

		Instanciador = GameObject.Find("Instanciador");
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
		if(Input.GetKeyDown(KeyCode.P) && OcultadorDeMensaje == true)
		{
			OcultadorDeMensaje = false;
			MensajeNotificador.alpha = 0f;
		}
		
		if(Input.GetKeyDown(KeyCode.I))
		{
			OcultadorDeHud = !OcultadorDeHud;
		}

		if(OcultadorDeHud == true)
		{
			HUDPasivas.alpha = 0f;
			HUDEstadisticas.alpha = 0f;
		}
		else
		{
			HUDPasivas.alpha = 1f;
			HUDEstadisticas.alpha = 1f;
		}
		
	}

	private void RaycastItems()
	{
		if(Input.GetKeyDown(KeyCode.E) && Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward , out DondeToco , Rango , layermaskFinal))
		{
			EquipadorActivas();
		}
	}

	private void BuscadorDeLayer()
	{
		int BuscadorDeLayer = DondeToco.collider.gameObject.layer;

		switch (BuscadorDeLayer)
		{
			case 14:
			OrdenDeLayers = 3;
			break;

			case 15:
			OrdenDeLayers = 0;
			break;

			case 16:
			OrdenDeLayers = 2;
			break;

			case 17:
			OrdenDeLayers = 1;
			break;

			default:
			print("ERROR LAYER NO ENCONTRADA");
			break;
		}
	}

	private void EquipadorActivas()
	{
		GameObject[] GuardadorDeItems = new GameObject[2];
		BuscadorDeLayer();

		string Buscador = DondeToco.collider.tag;
		GameObject ContenedorDeGameObjects = null;

		for(int i = 0; i < Activas.Length; i++)
		{
			if(Activas[i].tag != DondeToco.collider.gameObject.tag && Activas[i].layer == DondeToco.collider.gameObject.layer)
			{
				GuardadorDeItems[1] = Activas[i];
			}
		}

		for(int i = 0; i < Activas.Length; i++)
		{
			if(Activas[i].tag == DondeToco.collider.tag)
			{
				ContenedorDeGameObjects = Activas[i];
			}
		}

		switch (Buscador)
		{
			case "ActivaDeFuego1":

			if(ArrayDeActivas[OrdenDeLayers] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[OrdenDeLayers] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeFuego2":

			if(ArrayDeActivas[OrdenDeLayers] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[OrdenDeLayers] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeViento1":

			if(ArrayDeActivas[OrdenDeLayers] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[OrdenDeLayers] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeViento2":

			if(ArrayDeActivas[OrdenDeLayers] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[OrdenDeLayers] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeTierra1":

			if(ArrayDeActivas[OrdenDeLayers] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[OrdenDeLayers] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeTierra2":

			if(ArrayDeActivas[OrdenDeLayers] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[OrdenDeLayers] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeAgua1":

			if(ArrayDeActivas[OrdenDeLayers] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[OrdenDeLayers] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
			}
			break;

			case "ActivaDeAgua2":

			if(ArrayDeActivas[OrdenDeLayers] == false)
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				ArrayDeActivas[OrdenDeLayers] = true;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				SlotActivaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
			}
			break;

		}

		Text TextoNombre = NombreNotificador.GetComponent<Text>();
		TextoNombre.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Nombre;

		Text TextoCategoria = CategoriaNotificador.GetComponent<Text>();
		TextoCategoria.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Categoria;

		Text TextoDescripcion = DescripcionNotificador.GetComponent<Text>();
		TextoDescripcion.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Descripcion;

		OcultadorDeMensaje = true;

		MensajeNotificador.alpha = 1f;

	}
}