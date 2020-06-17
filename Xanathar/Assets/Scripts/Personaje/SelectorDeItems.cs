using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorDeItems : MonoBehaviour {

	private float rango = 5;

	public GameObject Pasivas;
	public GameObject Estadisticas;
	public GameObject ImagenActivaDeFuego1;
	public GameObject ImagenActivaDeFuego2;
	public GameObject ActivaDeFuego1;
	public GameObject ActivaDeFuego2;
	int ContadorDesactivador = 0;
	static int MantenerActivaFuego = 0;

	RaycastHit Dondepego;

	//Variable para la layer del objeto
	int layerMask = 1 << 12;

	// Use this for initialization
	void Start ()
	{
		ImagenActivaDeFuego1.SetActive(false);
		ImagenActivaDeFuego2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		SeleccionadorDeItems();
		OcultadorDeHud();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(Camera.main.transform.position , Camera.main.transform.forward * rango);
	}

	private void SeleccionadorDeItems()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward , out Dondepego , rango , layerMask))
			{
				if(MantenerActivaFuego == 0)
				{
					SlotDeItemVacio();
				}
				else if(MantenerActivaFuego == 1)
				{
					SlotDeItemLleno();
				}
			}
		}
	}

	private void OcultadorDeHud()
	{
		if(Input.GetKeyDown(KeyCode.I) && ContadorDesactivador == 0)
		{
			Pasivas.SetActive(false);
			Estadisticas.SetActive(false);
			ContadorDesactivador +=1;
		}
		else if(Input.GetKeyDown(KeyCode.I) && ContadorDesactivador == 1)
		{
			Pasivas.SetActive(true);
			Estadisticas.SetActive(true);
			ContadorDesactivador = 0;
		}
	}



	private void SlotDeItemVacio()
	{
		if(Dondepego.collider.tag == "ItemDeFuego1")
		{
			print("Item 1 agarrado");
			Destroy(Dondepego.collider.gameObject);
			ImagenActivaDeFuego1.SetActive(true);
			MantenerActivaFuego++;
		}
		if(Dondepego.collider.tag == "ItemDeFuego2")
		{
			print("Item 2 agarrado");
			Destroy(Dondepego.collider.gameObject);
			ImagenActivaDeFuego2.SetActive(true);
			MantenerActivaFuego++;
		}
	}

	private void SlotDeItemLleno()
	{
		if(Dondepego.collider.tag == "ItemDeFuego1")
		{
			print("Item 1 agarrado");
			Destroy(Dondepego.collider.gameObject);
			ImagenActivaDeFuego2.SetActive(false);
			ImagenActivaDeFuego1.SetActive(true);
			Instantiate(ActivaDeFuego2 , transform.position , Quaternion.identity);
		}
		if(Dondepego.collider.tag == "ItemDeFuego2")
		{
			print("Item 2 agarrado");
			Destroy(Dondepego.collider.gameObject);
			ImagenActivaDeFuego1.SetActive(false);
			ImagenActivaDeFuego2.SetActive(true);
			Instantiate(ActivaDeFuego1 , transform.position , Quaternion.identity);
		}
	}
}
