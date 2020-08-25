using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tierra3 : MonoBehaviour {

	private RaycastHit Hit;

	[Header("Transforms Seleccionables")]
	public Transform Heredar;
	public Transform PuntoDeAtaque;
	public Transform AreaDeVision;
	public Transform BrazoAtaque;

	[Header("Parametros")]
	private NavMeshAgent Agente;
	public Animator Animador;
	private Vector3 PosicionEnSpawn;
	private Vector3 Destino;
	private Transform UltimaPosicion;
	private GameObject Personaje;
	private string [] Estados = {"Idle" , "Searching" , "Chasing" ,  "Attack"};


	[Header("BoxColliderBrazo")]
	public BoxCollider PunoIzquierdo;

	[Header("Variables")]
	public string EstadoActual;
	public float AreaIdle;
	public float RangoDeVision;
	public float RangoAtaque;
	private int Pmask;
	private int EMask;
	public bool PermitirAtaque = false;
	
	// Use this for initialization
	void Start ()
	{
		Agente = GetComponent<NavMeshAgent>();
		PosicionEnSpawn = Heredar.position;
		Pmask = LayerMask.NameToLayer("Personaje");
		EMask = LayerMask.NameToLayer("Enemigo");
		EstadoActual = Estados[0];

		AreaDeVision.position = new Vector3(Heredar.position.x , Heredar.position.y , Heredar.position.z + RangoDeVision);

		Animador.SetBool("Idle" , true);
		Animador.SetBool("Caminando" , false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(EstadoActual == Estados[0] && Agente.remainingDistance < 0.2f)
		{
			Animador.SetBool("Idle" , true);
			Animador.SetBool("Caminando" , false);
		}

		if(BuscarPersonaje() && PuedoVer())
		{
			Acercar();
		}
	}

	public void IrAPosicionRandom()
	{
		EstadoActual = Estados[0];

		if(Agente.remainingDistance > Mathf.Epsilon)
		{
			Agente.destination = Destino;
		}
		else
		{
			if(FindObjectOfType<PositionManager>().EstaOcupado(Destino))
			{
				FindObjectOfType<PositionManager>().Llegue(Destino);
			}

			Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(PosicionEnSpawn , AreaIdle , Heredar.position);
			Agente.destination = Destino;
		}
	}

	public bool PuedoVer()
	{
		var direccion = Personaje.transform.position - Heredar.position;

		if(Physics.Raycast(Heredar.position , direccion , out Hit , RangoDeVision , EMask) && Hit.collider.gameObject.tag != "Personaje")
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	public bool BuscarPersonaje()
	{
		Collider [] Obj = Physics.OverlapSphere(Heredar.position , RangoDeVision);

		for(int i = 0; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.layer == Pmask)
			{
				Personaje = Obj[i].gameObject;
				return true;
			}
		}

		return false;
	}

	public void DetectarAtaque()
	{
		Collider [] Obj = Physics.OverlapBox(PuntoDeAtaque.position , new Vector3(RangoAtaque , RangoAtaque , RangoAtaque));

		for(int i = 0; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.tag == "Personaje")
			{

			}
		}
	}

	public void Acercar()
	{
		Agente.destination = Personaje.transform.position;
		EstadoActual = Estados[2];
	}

	public void Buscar()
	{
		UltimaPosicion = Personaje.transform;
		Agente.isStopped = false;

		EstadoActual = Estados[1];
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(AreaDeVision.position , RangoDeVision);

		Gizmos.color = Color.black;
		Vector3 AreaCubo = new Vector3(AreaIdle * 2 , 2 , AreaIdle * 2);
		Gizmos.DrawWireCube(PosicionEnSpawn , AreaCubo);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(Destino , 0.5f);

		Gizmos.color = Color.green;
		Vector3 AreaAtaque = new Vector3(RangoAtaque , RangoAtaque , RangoAtaque);
		Gizmos.DrawWireCube(PuntoDeAtaque.position , AreaAtaque);
	}
}
