using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tierra2 : MonoBehaviour {

	private RaycastHit Hit;

	[Header("Transforms Seleccionables")]
	public Transform Heredar;
	public Transform AreaAtaque;

	[Header("Box Colliders Garras")]
	public BoxCollider GarraIzquierda;
	public BoxCollider GarraDerecha;

	[Header("Parametro")]
	public float RangoIdle;
	public int RangoMaximo;
	public float rangoAtaque;
	public NavMeshAgent Agente;
	public Animator Animador;
	public Vector3 PosicionEnSpawn;

	public string [] Estados = {"Idle" , "Searching" , "Chasing" , "Attack"};
	public string EstadoActual;
	private Vector3 Destino;
	private GameObject Personaje;

	private int PMask;
	private int EMask;

	public int EstadoAtaque;
	public Tierra2Anim Tierra2Anim;
	EstadisticasDePersonaje EstadisticasDePersonaje;

	public bool PermitirAtaque = false;
	public bool ActivarColisiones = false;

	public float Damage;

	// Use this for initialization
	void Start ()
	{
		PosicionEnSpawn = Heredar.position;
		Agente = GetComponent<NavMeshAgent>();
		EstadoActual = Estados[0];

		PMask = LayerMask.NameToLayer("Personaje");
		EMask = LayerMask.NameToLayer("Enemigo");

		GarraIzquierda.enabled = false;
		GarraDerecha.enabled = false;

		Animador.SetBool("Corriendo" , false);
		Animador.SetBool("Idle" , true);
		EstadoAtaque = 0;

		Tierra2Anim = FindObjectOfType<Tierra2Anim>();

		EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(EstadoActual == Estados[0] && Agente.remainingDistance < Mathf.Epsilon)
		{
			Animador.SetBool("Idle" , true);
			Animador.SetBool("Corriendo" , false);
		}

		if(BuscarPersonaje() && PuedoVer() && PermitirAtaque == false)
		{
			EstadoActual = Estados[2];
		}

		if(EstadoActual == Estados[2] && PermitirAtaque == false)
		{
			Agente.destination = Personaje.transform.position;
			Animador.SetBool("Idle" , false);
			Animador.SetBool("Corriendo" , true);
			DetectarAtaque();
		}
	
		if(EstadoActual == Estados[3])
		{
			Agente.destination = Personaje.transform.position;

			if(PermitirAtaque == false)
			{
				if(EstadoAtaque == 0)
				{
					Animador.SetBool("Corriendo" , false);
					Animador.SetBool("AtaqueIzquierda" , true);
				}
				else
				{
					Animador.SetBool("Corriendo" , false);
					Animador.SetBool("AtaqueDerecha" , true);
				}
			}
		}
	}

	public void IrAPosRandom()
	{
		EstadoActual = Estados[0];
		if(Agente.remainingDistance > 1)
		{
			Agente.destination = Destino;
		}
		else
		{
			if(FindObjectOfType<PositionManager>().EstaOcupado(Destino))
			{
				FindObjectOfType<PositionManager>().Llegue(Destino);
			}

			Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(PosicionEnSpawn , RangoIdle , Heredar.position);
			Agente.destination = Destino;
		}
	}

	public bool PuedoVer()
	{
		var Direccion = Personaje.transform.position - Heredar.position;

		if(Physics.Raycast(Heredar.position , Direccion , out Hit , RangoMaximo , EMask) && Hit.collider.gameObject.tag != "Personaje")
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
		Collider [] Obj = Physics.OverlapSphere(Heredar.position , RangoMaximo);

		for(int i = 0; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.layer == PMask)
			{
				Personaje = Obj[i].gameObject;
				return true;
			}
		}

		return false;
	}

	public void DetectarAtaque()
	{
		Collider [] Obj = Physics.OverlapBox(AreaAtaque.position , new Vector3(rangoAtaque, rangoAtaque, rangoAtaque));

		for(int  i = 0; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.tag == "Personaje")
			{
				EstadoActual = Estados[3];
			}
			else
			{
				EstadoActual = Estados[2];
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(Heredar.position , RangoMaximo);

		Gizmos.color = Color.green;
		Vector3 AreaCuboAtaque = new Vector3(rangoAtaque , rangoAtaque , rangoAtaque);
		Gizmos.DrawWireCube(AreaAtaque.position , AreaCuboAtaque);

		Gizmos.color = Color.red;
		Vector3 AreaIdle = new Vector3(RangoIdle * 2 , 2 , RangoIdle * 2);
		Gizmos.DrawWireCube(PosicionEnSpawn , AreaIdle);

		Gizmos.color = Color.black;
		Gizmos.DrawSphere(Destino ,0.5f);
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == "Personaje" && ActivarColisiones == false)
		{
			ActivarColisiones = true;
			EstadisticasDePersonaje.RecibirDaño(Damage);
		}
	}
}
