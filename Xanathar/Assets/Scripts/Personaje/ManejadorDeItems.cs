using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorDeItems : MonoBehaviour {


	public AudioSource RuidoMuerte;
	//Variables para ocultar y mostrar el HUD
	public bool OcultadorDeHud = false;
	public bool OcultadorDeMensaje = false;

	public bool AntiCRASH = false;


	[Header("HUDS Personaje")]
	public CanvasGroup HUDPasivas;
	public CanvasGroup HUDEstadisticas;
	public CanvasGroup MensajeNotificador;
	public CanvasGroup InformacionEstadisticas;

	//Referencias a los slots de Activas, pasivas y estadisticas del HUD
	[Header("Slots de Las Habilidades")]
	public Image SlotActivaDeFuego;
	public Image SlotActivaDeViento;
	public Image SlotActivaDeTierra;
	public Image SlotActivaDeAgua;
	public Image SlotPasivaDeFuego;
	public Image SlotPasivaDeViento;
	public Image SlotPasivaDeTierra;
	public Image SlotPasivaDeAgua;
	public Image SlotEstadisticaDeFuego;
	public Image SlotEstadisticaDeViento;
	public Image SlotEstadisticaDeTierra;
	public Image SlotEstadisticaDeAgua;
	public Image [] SlotsDeImagesArray;

	//Notifico las variables para reemplazar su texto mas adelante
	private GameObject NombreNotificador;
	private GameObject CategoriaNotificador;
	private GameObject DescripcionNotificador;

	//Variables para notificar al ususario de los objetos que agarro
	Text TextoNombre;
	Text TextoCategoria;
	Text TextoDescripcion;

	//Variables para la barra de Vida
	[Header("Variables para la vida del personaje")]
	public Image Contenido;
	float RellenoDeVida;
	public Gradient TabletaDeColores;
	public Text TextoDeVida;
	public static GameObject CanvasHUD;
	public bool ActivadorRegeneracionDeVida = false;
	public bool CorrutinaFuncionando = false;
	RegeneracionVida regeneracionDeVida;
	ManejadorDeEscenas ManejadorDeEscenas;

	//Variables para mostrar las estadisticas del personaje
	public Text TextoInformacionVida;
	public Text TextoInformacionArmadura;
	private AgarradorDeItems agarradorDeItems;

	EstadisticasDePersonaje EstadisticasDePersonaje;
	void Awake()
	{
		//Encuentro los campos notifcadores para acceder a sus variables mas adelante
		NombreNotificador = GameObject.Find("NombreDelItem");
		CategoriaNotificador = GameObject.Find("CategoriaDelItem");
		DescripcionNotificador = GameObject.Find("DescripcionDelItem");

		TextoNombre = NombreNotificador.GetComponent<Text>();
		TextoCategoria = CategoriaNotificador.GetComponent<Text>();
		TextoDescripcion = DescripcionNotificador.GetComponent<Text>();

		agarradorDeItems = FindObjectOfType<AgarradorDeItems>();

		EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
		regeneracionDeVida = FindObjectOfType<RegeneracionVida>();
		ManejadorDeEscenas = FindObjectOfType<ManejadorDeEscenas>();
	}

	// Use this for initialization
	void Start ()
	{
		//MensajeNotificador.alpha = 0f;
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
		ManejadorDeEstadisticas();
			
		if(Input.GetKeyDown(KeyCode.H))
		{
			EstadisticasDePersonaje.VidaActualPersonaje -= 1;
			ManejadorDeVida();
		}
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
			InformacionEstadisticas.alpha = 0f;
			ManejadorDeEstadisticas();
		}
		else
		{
			HUDPasivas.alpha = 1f;
			HUDEstadisticas.alpha = 1f;
			InformacionEstadisticas.alpha = 1f;
		}
	}

	public void EquipadorSlotsDeItems(string CategoriaDelObjeto , string ElementoDelObjeto , GameObject ObjetoAgarrado)
	{
		switch(CategoriaDelObjeto)
		{
			case "Activa":
			BuscadorSlotsActiva(ElementoDelObjeto , ObjetoAgarrado);
			break;

			case "Pasiva":
			BuscadorSlotsPasiva(ElementoDelObjeto , ObjetoAgarrado);
			break;

			case "Estadistica":
			BuscadorSlotsEstadistica(ElementoDelObjeto , ObjetoAgarrado);
			break;
		}
	}

	private void BuscadorSlotsActiva(string ElementoDelObjeto , GameObject Objeto)
	{
		switch(ElementoDelObjeto)
		{
			case "Fuego":
			SlotActivaDeFuego.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotActivaDeFuego.enabled = true;
			break;

			case "Viento":
			SlotActivaDeViento.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotActivaDeViento.enabled = true;
			break;

			case "Tierra":
			SlotActivaDeTierra.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotActivaDeTierra.enabled = true;
			break;

			case "Agua":
			SlotActivaDeAgua.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotActivaDeAgua.enabled = true;
			break;
		}
	}

	private void BuscadorSlotsPasiva(string ElementoDelObjeto , GameObject Objeto)
	{
		switch(ElementoDelObjeto)
		{
			case "Fuego":
			SlotPasivaDeFuego.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotPasivaDeFuego.enabled = true;
			break;

			case "Viento":
			SlotPasivaDeViento.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotPasivaDeViento.enabled = true;
			break;

			case "Tierra":
			SlotPasivaDeTierra.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotPasivaDeTierra.enabled = true;
			break;

			case "Agua":
			SlotPasivaDeAgua.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotPasivaDeAgua.enabled = true;
			break;
		}
	}

	private void BuscadorSlotsEstadistica(string ElementoDelObjeto , GameObject Objeto)
	{
		switch(ElementoDelObjeto)
		{
			case "Fuego":
			SlotEstadisticaDeFuego.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotEstadisticaDeFuego.enabled = true;
			break;

			case "Viento":
			SlotEstadisticaDeViento.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotEstadisticaDeViento.enabled = true;
			break;

			case "Tierra":
			SlotEstadisticaDeTierra.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotEstadisticaDeTierra.enabled = true;
			break;

			case "Agua":
			SlotEstadisticaDeAgua.GetComponent<Image>().sprite = Objeto.GetComponent<InformacionDeItems>().Icono;
			SlotEstadisticaDeAgua.enabled = true;
			break;
		}
	}

	public void DesactivadorSlots()
	{
		for(int i = 0; i < SlotsDeImagesArray.Length; i++)
		{
			if(SlotsDeImagesArray[i].enabled == true)
			{
				SlotsDeImagesArray[i].enabled = false;
			}
		}
	}

	public void ManejadorDeVida()
	{
		float ValorActualVida = EstadisticasDePersonaje.VidaActualPersonaje;
		float ValorMaximoDeVida = EstadisticasDePersonaje.VidaMaximaPersonaje;

		if(ValorActualVida > ValorMaximoDeVida)
		{
			EstadisticasDePersonaje.VidaActualPersonaje = EstadisticasDePersonaje.VidaMaximaPersonaje;

			ValorActualVida = EstadisticasDePersonaje.VidaActualPersonaje;
			ValorMaximoDeVida = EstadisticasDePersonaje.VidaMaximaPersonaje;
		}

		if(ValorActualVida == ValorMaximoDeVida)
		{
			TextoDeVida.text = ValorActualVida.ToString();
			ActivadorRegeneracionDeVida = false;
		}

		RellenoDeVida = ValorActualVida / ValorMaximoDeVida;

		Contenido.fillAmount = RellenoDeVida;
		Contenido.color = TabletaDeColores.Evaluate(RellenoDeVida);

		if(ValorActualVida < ValorMaximoDeVida)
		{
			TextoDeVida.text = ValorActualVida.ToString();
			ActivadorRegeneracionDeVida = true;

			
			if(ActivadorRegeneracionDeVida == true)
			{
				StartCoroutine(regeneracionDeVida.RegenerarVida());
			}
			

			if(ValorActualVida < 1 && AntiCRASH == false)
			{
				if(ControlarPuertasYJefes.JefeEliminado == true)
				{
					EstadisticasDePersonaje.MurioDespuesDeMatarJefe = true;
				}

				AntiCRASH = true;
				TextoDeVida.text = "0";
				agarradorDeItems.DropeadorDeItems();
				ActivadorRegeneracionDeVida = false;

				
				if(EstadisticasDePersonaje.EstaMuerto == false)
				{
					RuidoMuerte.Play();
					ManejadorDeEscenas.VolverAlLobby();
					StartCoroutine(RecargarVida());
				}
				
			}
		}
	}

	public void ManejadorDeEstadisticas()
	{
		EstadisticasDePersonaje estadisticas = FindObjectOfType<EstadisticasDePersonaje>();

		float ValorMaximaDeVida = EstadisticasDePersonaje.VidaMaximaPersonaje;
		float ArmaduraPersonaje = estadisticas.Armadura;

		TextoInformacionVida.text = ValorMaximaDeVida.ToString();
		TextoInformacionArmadura.text = ArmaduraPersonaje.ToString();
	}

	public void MostradorDeMensajeNotificador(string NombreDelObjeto , string CategoriaDelObjeto , string DescripcionDelObjeto)
	{
		TextoNombre.text = NombreDelObjeto;
		TextoCategoria.text = CategoriaDelObjeto;
		TextoDescripcion.text = DescripcionDelObjeto;

		MensajeNotificador.alpha = 1f;
		OcultadorDeMensaje = true;
		Debug.Log(MensajeNotificador.alpha);
	}

	
	public IEnumerator RecargarVida()
	{
		EstadisticasDePersonaje.EstaMuerto = true;
		yield return new WaitForSeconds(1);

		EstadisticasDePersonaje.VidaActualPersonaje = EstadisticasDePersonaje.VidaMaximaPersonaje;
		ManejadorDeVida();
		EstadisticasDePersonaje.EstaMuerto = false;
		CorrutinaFuncionando = false;
		AntiCRASH = false;
	}
}