using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionadorDeItems : MonoBehaviour {

	private float Rango = 5;

	public GameObject pasivas;
	public GameObject Estadisticas;
	public GameObject ImagenActivaFuego1;
	public GameObject ImagenActivaFuego2;
	public GameObject ActivaDeFuego1;
	public GameObject ActivaDeFuego2;
	int layerMask = 1 << 9;
	private int Contador = 0;


	static int MantenerItemDeFuego = 0;

	RaycastHit DondeToco;


	// Use this for initialization
	void Start ()
	{
		ImagenActivaFuego1.SetActive(false);
		ImagenActivaFuego2.SetActive(false);
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
			if(Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward , out DondeToco , Rango , ~layerMask))
			{
				if(MantenerItemDeFuego == 0)
				{
					SlotDeItemVacio();
				}
				else if(MantenerItemDeFuego == 1)
				{
					print ("Reemplazar Item");
					SlotDeItemLleno();
					
				}
			}
		}
	}

	private void SlotDeItemVacio()
	{
		if(DondeToco.collider.tag == "ItemDeFuego1")
		{
			print("Item 1 agarrado");
			Destroy(DondeToco.collider.gameObject);
			ImagenActivaFuego1.SetActive(true);
			MantenerItemDeFuego += 1;
		}
		if(DondeToco.collider.tag == "ItemDeFuego2")
		{
			print("Item 2 Agarrado");
			Destroy(DondeToco.collider.gameObject);
			ImagenActivaFuego2.SetActive(true);
			MantenerItemDeFuego += 1;
		}
	}

	private void SlotDeItemLleno()
	{
		if(DondeToco.collider.tag == "ItemDeFuego1")
		{
			print("Item 1 agarrado");
			Destroy(DondeToco.collider.gameObject);
			ImagenActivaFuego2.SetActive(false);
			ImagenActivaFuego1.SetActive(true);
			Instantiate(ActivaDeFuego2 , transform.position , Quaternion.identity);
		}
		if(DondeToco.collider.tag == "ItemDeFuego2")
		{
			print("Item 2 agarrado");
			Destroy(DondeToco.collider.gameObject);
			ImagenActivaFuego1.SetActive(false);
			ImagenActivaFuego2.SetActive(true);
			Instantiate(ActivaDeFuego1 , transform.position , Quaternion.identity);
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
}
