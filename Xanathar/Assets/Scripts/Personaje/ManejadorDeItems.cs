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
	string OrdenDeElemento;
	GameObject[] Objetos;

	public static GameObject[] ItemsEquipados = new GameObject[12];

	GameObject ContenedorDeGameObjects;

	//Instanciador de donde nuestros objetos apareceran
	private GameObject Instanciador;

	//Variables para notificar al ususario de los objetos que agarro
	Text TextoNombre;
	Text TextoCategoria;
	Text TextoDescripcion;

	//Variables para la barra de Vida
	public Image Contenido;
	float RellenoDeVida;
	public Gradient TabletaDeColores;
	public Text TextoDeVida;
	static GameObject CanvasHUD;

	CambiadorDeNivel CambioCuandoMueras;

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

		CambioCuandoMueras = FindObjectOfType<CambiadorDeNivel>();
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
		ActivadorDeHUD();
		RaycastItems();

		
		if(Input.GetKeyDown(KeyCode.J))
		{
			//DropeadorDeItems();
			//VerObjetosEquipados();

			EstadisticasDePersonaje.VidaActualPersonaje -=1;
			ManejadorDeVida();
		}
		
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

		string ElementoDelObjeto = DondeToco.collider.gameObject.GetComponent<InformacionDeItems>().Elemento;
		string CategoriaDelObjeto = DondeToco.collider.gameObject.GetComponent<InformacionDeItems>().Categoria;
		string NombreDelObjeto = DondeToco.collider.gameObject.GetComponent<InformacionDeItems>().Nombre;
		//string DescripcionDelObjeto = DondeToco.collider.gameObject.GetComponent<InformacionDeItems>().Descripcion;

		BuscadorDeLayer();

		for(int i = 0; i < Objetos.Length; i++)
		{
			if(Objetos[i].GetComponent<InformacionDeItems>().Elemento == ElementoDelObjeto && Objetos[i].GetComponent<InformacionDeItems>().Categoria == CategoriaDelObjeto)
			{
				if(Objetos[i].GetComponent<InformacionDeItems>().Nombre == NombreDelObjeto)
				{
					GuardadorDeItems[0] = Objetos[i];
					ContenedorDeGameObjects = Objetos[i];
					//print("Este es el item que agarre" + GuardadorDeItems[0]);
				}

				if(Objetos[i].GetComponent<InformacionDeItems>().Nombre != NombreDelObjeto)
				{
					GuardadorDeItems[1] = Objetos[i];
					//print("Esto es el item que no agarre" + GuardadorDeItems[1]);
				}

				if(GuardadorDeItems[0] != null && GuardadorDeItems[1] != null)
				{
					break;
				}
			}
		}

		for(int i = 0; i < ItemsEquipados.Length; i++)
		{
			if(ItemsEquipados[i] == null)
			{
				ItemsEquipados[i] = ContenedorDeGameObjects;
				EquipadorSlotsDeItems();
				Destroy(DondeToco.collider.gameObject);
				//print("Tengo equipado este item: " + ItemsEquipados[i]);
				break;
			}

			string ElementoItemEquipado = ItemsEquipados[i].GetComponent<InformacionDeItems>().Elemento;
			string CategoriaItemEquipado = ItemsEquipados[i].GetComponent<InformacionDeItems>().Categoria;

			if(ElementoItemEquipado == ElementoDelObjeto && CategoriaItemEquipado == CategoriaDelObjeto)
			{
				ItemsEquipados[i] = ContenedorDeGameObjects;
				EquipadorSlotsDeItems();
				Destroy(DondeToco.collider.gameObject);
				Instantiate(GuardadorDeItems[1] , Instanciador.transform.position , Quaternion.identity);
				//print("Tire este item: " + GuardadorDeItems[1]);
				break;
			}
		}

		TextoNombre.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Nombre;
		TextoCategoria.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Categoria;
		TextoDescripcion.text = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Descripcion;

		OcultadorDeMensaje = true;
		MensajeNotificador.alpha = 1f;
	}

	private void BuscadorDeLayer()
	{
		string ElementoDelItem = DondeToco.collider.gameObject.GetComponent<InformacionDeItems>().Elemento;

		switch (ElementoDelItem)
		{
			case "Fuego":
			OrdenDeElemento = "Fuego";
			break;

			case "Viento":
			OrdenDeElemento = "Viento";
			break;

			case "Tierra":
			OrdenDeElemento = "Tierra";
			break;

			case "Agua":
			OrdenDeElemento = "Agua";
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
		switch (OrdenDeElemento)
		{
			case "Fuego":
			SlotEstadisticaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Viento":
			SlotEstadisticaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Tierra":
			SlotEstadisticaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Agua":
			SlotEstadisticaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;
		}
	}
	private void BuscadorDeSlotsPasivas()
	{
		switch(OrdenDeElemento)
		{
			case "Fuego":
			SlotPasivaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Viento":
			SlotPasivaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Tierra":
			SlotPasivaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Agua":
			SlotPasivaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;
		}
	}
	private void BuscadorDeSlotsActiva()
	{
		switch(OrdenDeElemento)
		{
			case "Fuego":
			SlotActivaDeFuego.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Viento":
			SlotActivaDeViento.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Tierra":
			SlotActivaDeTierra.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;

			case "Agua":
			SlotActivaDeAgua.GetComponent<Image>().sprite = ContenedorDeGameObjects.GetComponent<InformacionDeItems>().Icono;
			break;
		}
	}

	public void ManejadorDeVida()
	{
		float ValorActualVida = EstadisticasDePersonaje.VidaActualPersonaje;
		float ValorMaximoDeVida = EstadisticasDePersonaje.VidaMaximaPersonaje;

		RellenoDeVida = ValorActualVida / ValorMaximoDeVida;

		Contenido.fillAmount = RellenoDeVida;
		Contenido.color = TabletaDeColores.Evaluate(RellenoDeVida);

		if(ValorActualVida > 0)
		{
			TextoDeVida.text = "Tu vida es: " + ValorActualVida.ToString();
		}
		else
		{
			TextoDeVida.text = "Tu vida es: 0";
			DropeadorDeItems();
		}
	}

	public void DropeadorDeItems()
	{
		for(int i = 0; i < ItemsEquipados.Length; i++)
		{
			if(ItemsEquipados[i] != null)
			{
				Instantiate(ItemsEquipados[i] , Instanciador.transform.position , Quaternion.identity);
				ItemsEquipados[i] = null;
			}
		}

		if(ManejadorDeEscenas.ActivadorDeCambio == false)
		{	
			EstadisticasDePersonaje.VidaActualPersonaje = EstadisticasDePersonaje.VidaMaximaPersonaje;
			ManejadorDeEscenas.NombreDeEscena = "TestMenda";
			CambioCuandoMueras.IniciadorDeCambio();
		}
	}

	//Funcion para testear inventario
	private void VerObjetosEquipados()
	{
		for(int i = 0; i < ItemsEquipados.Length; i++)
		{
			print(ItemsEquipados[i]);
		}
	}
}