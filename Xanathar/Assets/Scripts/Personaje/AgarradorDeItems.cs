using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarradorDeItems : MonoBehaviour {
	private int layerMask = ~(1 << 9);
	RaycastHit ColisionDeObjeto;
	float Rango = 5;
	string OrdenDeElemento;
	public GameObject [] ObjetosEquipados = new GameObject[12];
	public GameObject Instanciador;
	private ManejadorDeItems ManejadorDeHUD;


	public float RangoDeInstancia;
	public float RadioDeObjetos;

	// Use this for initialization
	void Start ()
	{
		ManejadorDeHUD = FindObjectOfType<ManejadorDeItems>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastDeItems();
		
		if(Input.GetKeyDown(KeyCode.J))
		{
			DropeadorDeItems();
		}
		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(Camera.main.transform.position , Camera.main.transform.forward * Rango);
	}

	private void RaycastDeItems()
	{
		if(Input.GetKeyDown(KeyCode.E) && Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward , out ColisionDeObjeto , Rango , layerMask) && ColisionDeObjeto.collider.gameObject.tag == "Items")
		{
			EquipadorDeItems();
		}
	}

	private void EquipadorDeItems()
	{
		string ElementoDelObjeto = ColisionDeObjeto.collider.gameObject.GetComponent<InformacionDeItems>().Elemento;
		string CategoriaDelObjeto = ColisionDeObjeto.collider.gameObject.GetComponent<InformacionDeItems>().Categoria;
		string NombreDelObjeto = ColisionDeObjeto.collider.gameObject.GetComponent<InformacionDeItems>().Nombre;

		GameObject [] Objetos = Resources.LoadAll<GameObject>("Objetos");
		GameObject [] AlmacenadorDeObjetos = new GameObject[2];

		for(int i = 0; i < Objetos.Length; i++)
		{
			if(Objetos[i].GetComponent<InformacionDeItems>().Elemento == ElementoDelObjeto && Objetos[i].GetComponent<InformacionDeItems>().Categoria == CategoriaDelObjeto)
			{
				if(Objetos[i].GetComponent<InformacionDeItems>().Nombre == NombreDelObjeto)
				{
					AlmacenadorDeObjetos[0] = Objetos[i];
				}

				if(Objetos[i].GetComponent<InformacionDeItems>().Nombre != NombreDelObjeto)
				{
					AlmacenadorDeObjetos[1] = Objetos[i];
				}

				if(AlmacenadorDeObjetos[0] != null && AlmacenadorDeObjetos[1] != null)
				{
					break;
				}
			}
		}

		//Debug.Log("El objeto que agarre es: " + AlmacenadorDeObjetos[0]);
		//Debug.Log("El objeto que no agarre es: " + AlmacenadorDeObjetos[1]);

		AgarrarYReemplazar(AlmacenadorDeObjetos[0] , AlmacenadorDeObjetos[1], ElementoDelObjeto , CategoriaDelObjeto , NombreDelObjeto);
	}

	private void AgarrarYReemplazar(GameObject ObjetoAgarrado , GameObject ObjetoNoAgarrado , string ElementoDelObjetoAgarrado , string CategoriaDelObjetoAgarrado , string NombreDelObjetoAgarrado)
	{
		string ElementoItemEquipado;
		string CategoriaItemEquipado;


		for(int i = 0; i < ObjetosEquipados.Length; i++)
		{
			if(ObjetosEquipados[i] == null)
			{
				ObjetosEquipados[i] = ObjetoAgarrado;
				Destroy(ColisionDeObjeto.collider.gameObject);

				ChequeadorDeActivaYElemento(CategoriaDelObjetoAgarrado , ElementoDelObjetoAgarrado , NombreDelObjetoAgarrado);
				break;
			}

			ElementoItemEquipado = ObjetosEquipados[i].GetComponent<InformacionDeItems>().Elemento;
			CategoriaItemEquipado = ObjetosEquipados[i].GetComponent<InformacionDeItems>().Categoria;

			if(ElementoItemEquipado == ElementoDelObjetoAgarrado && CategoriaItemEquipado == CategoriaDelObjetoAgarrado)
			{
				ObjetosEquipados[i] = ObjetoAgarrado;
				Destroy(ColisionDeObjeto.collider.gameObject);
				ChequeadorDeActivaYElemento(CategoriaDelObjetoAgarrado , ElementoDelObjetoAgarrado , NombreDelObjetoAgarrado);


				Vector3 PosAInstanciar = new Vector3(Instanciador.transform.position.x , (Instanciador.transform.position.y + 1) , Instanciador.transform.position.z);
				Instantiate(ObjetoNoAgarrado , PosAInstanciar , Quaternion.identity);
				break;
			}
		}

		string NombreDelObjeto = ObjetoAgarrado.GetComponent<InformacionDeItems>().Nombre;
		string CategoriaDelObjeto = ObjetoAgarrado.GetComponent<InformacionDeItems>().Categoria;
		string DescripcionDelObjeto = ObjetoAgarrado.GetComponent<InformacionDeItems>().Descripcion;
		string ElementoDelObjeto = ObjetoAgarrado.GetComponent<InformacionDeItems>().Elemento;

		ManejadorDeHUD.MostradorDeMensajeNotificador(NombreDelObjeto , CategoriaDelObjeto , DescripcionDelObjeto);
		ManejadorDeHUD.EquipadorSlotsDeItems(CategoriaDelObjeto , ElementoDelObjeto , ObjetoAgarrado);
	}

	public void DropeadorDeItems()
	{
		GameObject PosParaInstanciar = GameObject.FindGameObjectWithTag("PosicionClave");
		for(int i = 0; i < ObjetosEquipados.Length; i++)
		{
			if(ObjetosEquipados[i] == null)
			{
				break;
			}

			Vector3 PosicionObjeto = Vector3.zero;
			bool PosicionValida = false;
			int Seguro = 0;

			while(PosicionValida == false)
			{
				if(Seguro >= 50)
				{
					//Debug.Log("Se rompio");
					break;
				}

				Seguro += 1;
				PosicionObjeto = new Vector3(Random.Range(-RangoDeInstancia , RangoDeInstancia) + PosParaInstanciar.transform.position.x , PosParaInstanciar.transform.position.y , Random.Range(-RangoDeInstancia , RangoDeInstancia) + PosParaInstanciar.transform.position.z);
				PosicionValida = true;
				Collider[] Colisiones = Physics.OverlapSphere(PosicionObjeto, RadioDeObjetos);

				foreach(Collider col in Colisiones)
				{
					if(col.tag == "Items" || col.tag == "Personaje" || col.tag == "Piso")
					{
						PosicionValida = false;
					}
				}
			}

			if(PosicionValida == true)
			{
				Instantiate(ObjetosEquipados[i] , PosicionObjeto , Quaternion.identity);
				Debug.Log(ObjetosEquipados[i]);
				ObjetosEquipados[i] = null;
				//Debug.Log("EXITO");
			}
		}

		ManejadorDeHUD.DesactivadorSlots();
	}

	public void ChequeadorDeActivaYElemento(string Categoria , string Elemento , string Nombre)
	{
		if(Categoria == "Activa")
		{
			switch(Elemento)
			{
				case "Fuego":
				Debug.Log("Activa De Fuego");
				break;

				case "Viento":
				Debug.Log("Activa De Viento");
				break;

				case "Tierra":
				Debug.Log("Activa De Tierra");
				break;

				case "Agua":
				Debug.Log("Activa De Agua");
				break;
			}
		}
		else
		{
			Debug.Log("No es un item activo, es un item de categoria: " + Categoria);
		}
	}
}
