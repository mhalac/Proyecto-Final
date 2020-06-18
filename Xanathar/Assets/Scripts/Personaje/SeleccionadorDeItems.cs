using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionadorDeItems : MonoBehaviour {

	private float Rango = 5;

	public GameObject pasivas;
	public GameObject Estadisticas;


	//Imagenes
	public GameObject ImagenActivaFuego1;
	public GameObject ImagenActivaFuego2;

	public GameObject ImagenActivaAgua1;
	public GameObject ImagenActivaAgua2;

	public GameObject ImagenActivaViento1;
	public GameObject ImagenActivaViento2;

	public GameObject ImagenActivaTierra1;
	public GameObject ImagenActivaTierra2;



	//Activas
	private GameObject ActivaDeFuego1;
	private GameObject ActivaDeFuego2;

	private GameObject ActivaDeAgua1;
	private GameObject ActivaDeAgua2;

	private GameObject ActivaDeViento1;
	private GameObject ActivaDeViento2;

	private GameObject ActivaDeTierra1;
	private GameObject ActivaDeTierra2;

	
	int layerMask = 1 << 9;
	private int Contador = 0;


	string ComparadorDeItem;

	//Variables para detectar si tiene un item
	static int MantenerActivaDeFuego = 0;
	static int MantenerActivaDeAgua = 0;
	static int MantenerActivaDeViento = 0;
	static int MantenerActivaDeTierra = 0;
	

	static int TotalDeItems = 0;

	//Variable que guarda informacion del item del raycast
	RaycastHit DondeToco;


	// Use this for initialization
	void Start ()
	{
		ImagenActivaFuego1.SetActive(false);
		ImagenActivaFuego2.SetActive(false);

		ImagenActivaAgua1.SetActive(false);
		ImagenActivaAgua2.SetActive(false);

		ImagenActivaViento1.SetActive(false);
		ImagenActivaViento2.SetActive(false);
		
		ImagenActivaTierra1.SetActive(false);
		ImagenActivaTierra2.SetActive(false);

		ActivaDeFuego1 = Resources.Load<GameObject>("ActivasFuego/ActivaDeFuego1");
		ActivaDeFuego2 = Resources.Load<GameObject>("ActivasFuego/ActivaDeFuego2");

		ActivaDeAgua1 = Resources.Load<GameObject>("ActivasAgua/ActivaDeAgua1");
		ActivaDeAgua2 = Resources.Load<GameObject>("ActivasAgua/ActivaDeAgua2");

		ActivaDeViento1 = Resources.Load<GameObject>("ActivasViento/ActivaDeViento1");
		ActivaDeViento2 = Resources.Load<GameObject>("ActivasViento/ActivaDeViento2");

		ActivaDeTierra1 = Resources.Load<GameObject>("ActivasTierra/ActivaDeTierra1");
		ActivaDeTierra2 = Resources.Load<GameObject>("ActivasTierra/ActivaDeTierra2");
	}
	
	// Update is called once per frame
	void Update ()
	{
		ManejadorHUD();
		RaycastDeObjetos();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(Camera.main.transform.position , Camera.main.transform.forward * Rango);
	}

	private void RaycastDeObjetos()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			//Instantiate(prueba1 , transform.position , Quaternion.identity);
			if(Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward , out DondeToco , Rango , ~layerMask))
			{
				CodigoRedentor();
			}
		}
	}


	private void ManejadorHUD()
	{
		if(Input.GetKeyDown(KeyCode.I) && Contador == 0)
		{
			pasivas.SetActive(false);
			Estadisticas.SetActive(false);
			Contador += 1;
		}
		else if(Input.GetKeyDown(KeyCode.I) && Contador == 1)
		{
			pasivas.SetActive(true);
			Estadisticas.SetActive(true);
			Contador = 0;
		}
	}

	private void CodigoRedentor()
	{
		//Cuando se agarra un item, hay que comprobar que el slot esta vacio
		// Si ya se tiene un item en el slot, se debe "tirar" ese item y reemplazarlo con otro
		//Se deben mostrar o ocultar las imagenes del HUD

		string Comparador = DondeToco.collider.tag;

		switch(Comparador)
		{
			case "ActivaDeFuego1":
			if(MantenerActivaDeFuego == 0)
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaFuego1.SetActive(true);
				MantenerActivaDeFuego += 1;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaFuego2.SetActive(false);
				ImagenActivaFuego1.SetActive(true);
				GameObject obj = Instantiate(ActivaDeFuego2 , transform.position , Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(obj.GetComponent<Transform>().forward * 0.2f);
			}
			break;

			case "ActivaDeFuego2":
			if(MantenerActivaDeFuego == 0)
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaFuego2.SetActive(true);
				MantenerActivaDeFuego += 1;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaFuego1.SetActive(false);
				ImagenActivaFuego2.SetActive(true);
				GameObject obj = Instantiate(ActivaDeFuego1 , transform.position , Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(obj.GetComponent<Transform>().forward * 0.2f);
			}
			break;

			case "ActivaDeAgua1":
			if(MantenerActivaDeAgua == 0)
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaAgua1.SetActive(true);
				MantenerActivaDeAgua += 1;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaAgua2.SetActive(false);
				ImagenActivaAgua1.SetActive(true);
				GameObject obj = Instantiate(ActivaDeAgua2 , transform.position , Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(obj.GetComponent<Transform>().forward * 0.2f);
			}
			break;

			case "ActivaDeAgua2":
			if(MantenerActivaDeAgua == 0)
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaAgua2.SetActive(true);
				MantenerActivaDeAgua += 1;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaAgua1.SetActive(false);
				ImagenActivaAgua2.SetActive(true);
				GameObject obj = Instantiate(ActivaDeAgua1 , transform.position , Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(obj.GetComponent<Transform>().forward * 0.2f);
			}
			break;

			case "ActivaDeViento1":
			if(MantenerActivaDeViento == 0)
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaViento1.SetActive(true);
				MantenerActivaDeViento += 1;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaViento2.SetActive(false);
				ImagenActivaViento1.SetActive(true);
				GameObject obj = Instantiate(ActivaDeViento2 , transform.position , Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(obj.GetComponent<Transform>().forward * 0.2f);
			}
			break;

			case "ActivaDeViento2":
			if(MantenerActivaDeViento == 0)
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaViento2.SetActive(true);
				MantenerActivaDeViento += 1;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaViento1.SetActive(false);
				ImagenActivaViento2.SetActive(true);
				GameObject obj = Instantiate(ActivaDeViento1 , transform.position , Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(obj.GetComponent<Transform>().forward * 0.2f);
			}
			break;

			case "ActivaDeTierra1":
			if(MantenerActivaDeTierra == 0)
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaTierra1.SetActive(true);
				MantenerActivaDeTierra += 1;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaTierra2.SetActive(false);
				ImagenActivaTierra1.SetActive(true);
				GameObject obj = Instantiate(ActivaDeTierra2 , transform.position , Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(obj.GetComponent<Transform>().forward * 0.2f);
			}
			break;

			case "ActivaDeTierra2":
			if(MantenerActivaDeTierra == 0)
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaTierra2.SetActive(true);
				MantenerActivaDeTierra += 1;
			}
			else
			{
				Destroy(DondeToco.collider.gameObject);
				ImagenActivaTierra1.SetActive(false);
				ImagenActivaTierra2.SetActive(true);
				GameObject obj = Instantiate(ActivaDeTierra1 , transform.position , Quaternion.identity);
				obj.GetComponent<Rigidbody>().AddForce(obj.GetComponent<Transform>().forward * 0.2f);
			}
			break;
		}
		
	}
}
