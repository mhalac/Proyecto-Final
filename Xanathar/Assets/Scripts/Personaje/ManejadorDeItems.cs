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

	//Referencias a los slots de Activas, pasivas y estadisticas del HUD
	private GameObject SlotActivaDeFuego;
	private GameObject SlotActivaDeViento;
	private GameObject SlotActivaDeTierra;
	private GameObject SlotActivaDeAgua;
	private GameObject SlotPasivaDeFuego;
	private GameObject SlotPasivaDeViento;
	private GameObject SlotPasivaDeTierra;
	private GameObject SlotPasivaDeAgua;
	private GameObject SlotEstadisticaDeFuego;
	private GameObject SlotEstadisticaDeViento;
	private GameObject SlotEstadisticaDeTierra;
	private GameObject SlotEstadisticaDeAgua;

	//Notifico las variables para reemplazar su texto mas adelante
	private GameObject NombreNotificador;
	private GameObject CategoriaNotificador;
	private GameObject DescripcionNotificador;

	int OrdenDeLayers = 0;

	GameObject[] Objetos;

	GameObject[] ItemsEquipados = new GameObject[12];

	GameObject ContenedorDeGameObjects;

	//Instanciador de donde nuestros objetos apareceran
	private GameObject Instanciador;

	//Variables para notificar al ususario de los objetos que agarro
	Text TextoNombre;
	Text TextoCategoria;
	Text TextoDescripcion;

	//Variables para la barra de Vida
	public Image Contenido;
	public float VelocidadLerp;
	float RellenoDeVida;
	public Gradient TabletaDeColores;
	public Text TextoDeVida;






	static GameObject CanvasHUD;


	void Awake()
	{
		//Fusiono las layermasks en una sola
		layermaskFinal = layerMask1 | layerMask2 | layerMask3 | layerMask4;

		//Lleno mi Array de los items activos
		Objetos = Resources.LoadAll<GameObject>("Objetos");

		//Encuentro el Slot para referenciarlo y acceder a sus variables mas adelante
		SlotActivaDeFuego = GameObject.Find("ActivaFuego");
		SlotActivaDeViento = GameObject.Find("ActivaViento");
		SlotActivaDeTierra = GameObject.Find("ActivaTierra");
		SlotActivaDeAgua = GameObject.Find("ActivaAgua");

		SlotPasivaDeFuego = GameObject.Find("PasivaFuego");
		SlotPasivaDeViento = GameObject.Find("PasivaViento");
		SlotPasivaDeTierra = GameObject.Find("PasivaTierra");
		SlotPasivaDeAgua = GameObject.Find("PasivaAgua");

		SlotEstadisticaDeFuego = GameObject.Find("EstadisticaFuego");
		SlotEstadisticaDeViento = GameObject.Find("EstadisticaViento");
		SlotEstadisticaDeTierra = GameObject.Find("EstadisticaTierra");
		SlotEstadisticaDeAgua = GameObject.Find("EstadisticaAgua");

		//Encuentro los campos notifcadores para acceder a sus variables mas adelante
		NombreNotificador = GameObject.Find("NombreDelItem");
		CategoriaNotificador = GameObject.Find("CategoriaDelItem");
		DescripcionNotificador = GameObject.Find("DescripcionDelItem");

		Instanciador = GameObject.Find("Instanciador");

		ContenedorDeGameObjects = null;

		TextoNombre = NombreNotificador.GetComponent<Text>();
		TextoCategoria = CategoriaNotificador.GetComponent<Text>();
		TextoDescripcion = DescripcionNotificador.GetComponent<Text>();

	}

	// Use this for initialization
	void Start ()
	{
		MensajeNotificador.alpha = 0f;
		ManejadorDeVida();

		//Funcion para mantener al HUD entre escenas
		if(CanvasHUD != null)
		{
			Destroy(this.gameObject);
			return;
		}

		CanvasHUD = this.gameObject;
		GameObject.DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if(Input.GetKeyDown(KeyCode.C))
		{
			EstadisticasDePersonaje.VidaActualPersonaje += 1;
			ManejadorDeVida();
		}
		if(Input.GetKeyDown(KeyCode.V))
		{
			EstadisticasDePersonaje.VidaActualPersonaje -= 1;
			ManejadorDeVida();
		}
		*/
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
		if(Input.GetKeyDown(KeyCode.E) && OcultadorDeMensaje == true)
		{
			OcultadorDeMensaje = false;
			MensajeNotificador.alpha = 0f;
		}
		
		if(Input.GetKeyDown(KeyCode.Tab))
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
			EquipadorObjetos();
		}
	}

	private void EquipadorObjetos()
	{
		GameObject[] GuardadorDeItems = new GameObject[2];

		string TagDelObjeto = DondeToco.collider.tag;
		int LayerDelObjeto = DondeToco.collider.gameObject.layer;
		string Categoria = "";

		BuscadorDeLayer();

		for(int i = 0; i < Objetos.Length; i++)
		{
			if(Objetos[i].tag == TagDelObjeto)
			{
				ContenedorDeGameObjects = Objetos[i];
				Categoria = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Categoria;
				//print(Objetos[i]);
				break;
			}
		}


		for(int i = 0; i < Objetos.Length; i++)
		{
			if(Objetos[i].layer == LayerDelObjeto && TagDelObjeto != Objetos[i].tag && Objetos[i].GetComponent<InformacionDeItems>().Categoria == Categoria)
			{
				GuardadorDeItems[1] = Objetos[i];
			}

			if(GuardadorDeItems[1] != null)
			{
				break;
			}
		}

		for(int i = 0; i < ItemsEquipados.Length; i++)
		{
			if(ItemsEquipados[i] == null)
			{
				ItemsEquipados[i] = ContenedorDeGameObjects;
				EquipadorSlotsDeItems();
				Destroy(DondeToco.collider.gameObject);
				break;
			}
			
			string BuscadorCategoria = ItemsEquipados[i].GetComponent<InformacionDeItems>().Categoria;

			if(ItemsEquipados[i].layer == LayerDelObjeto && BuscadorCategoria == Categoria)
			{
				ItemsEquipados[i] = ContenedorDeGameObjects;
				EquipadorSlotsDeItems();
				Destroy(DondeToco.collider.gameObject);
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
				break;
			}
		}
		
		for(int i = 0; i < ItemsEquipados.Length; i++)
		{
			print(ItemsEquipados[i]);
		}
		
		TextoNombre.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Nombre;

		TextoCategoria.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Categoria;

		TextoDescripcion.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Descripcion;

		OcultadorDeMensaje = true;

		MensajeNotificador.alpha = 1f;
	}

	private void BuscadorDeLayer()
	{
		int BuscadorDeLayer = DondeToco.collider.gameObject.layer;

		switch (BuscadorDeLayer)
		{
			//Agua
			case 14:
			OrdenDeLayers = 3;
			break;
			
			//Fuego
			case 15:
			OrdenDeLayers = 0;
			break;

			//Tierra
			case 16:
			OrdenDeLayers = 2;
			break;

			//Viento
			case 17:
			OrdenDeLayers = 1;
			break;

			default:
			print("ERROR LAYER NO ENCONTRADA");
			break;
		}
	}

	private void EquipadorSlotsDeItems()
	{
		string Categoria = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Categoria;
		
		switch(Categoria)
		{
			case "Activa":
			BuscadorDeSlotsActiva();
			break;

			case "Pasiva":
			BuscadorDeSlotsPasivas();
			break;

			case "Estadistica":
			BuscadorDeSlotsEstadistica();
			break;
		}
	}

	private void BuscadorDeSlotsEstadistica()
	{
		switch (OrdenDeLayers)
		{
			case 0:
			SlotEstadisticaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 1:
			SlotEstadisticaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 2:
			SlotEstadisticaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 3:
			SlotEstadisticaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;
		}
	}
	private void BuscadorDeSlotsPasivas()
	{
		switch (OrdenDeLayers)
		{
			case 0:
			SlotPasivaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 1:
			SlotPasivaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 2:
			SlotPasivaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 3:
			SlotPasivaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;
		}
	}
	private void BuscadorDeSlotsActiva()
	{
		switch (OrdenDeLayers)
		{
			case 0:
			SlotActivaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 1:
			SlotActivaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 2:
			SlotActivaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case 3:
			SlotActivaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;
		}
	}

	public void ManejadorDeVida()
	{
		if(EstadisticasDePersonaje.VidaActualPersonaje >= 0)
		{
			float ValorActualVida = EstadisticasDePersonaje.VidaActualPersonaje;
			float ValorMaximoDeVida = EstadisticasDePersonaje.VidaMaximaPersonaje;

			RellenoDeVida = ValorActualVida / ValorMaximoDeVida;

			Contenido.fillAmount = RellenoDeVida;
			Contenido.color = TabletaDeColores.Evaluate(RellenoDeVida);
			TextoDeVida.text = "Tu vida es: " + ValorActualVida.ToString();
		}
	}
}