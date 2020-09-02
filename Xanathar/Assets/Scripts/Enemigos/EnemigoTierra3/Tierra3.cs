using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tierra3 : MonoBehaviour {

	EstadisticasDePersonaje EstadisticasDePersonaje;
	private RaycastHit Hit;

	[Header("Transforms Seleccionables")]
	public Transform Heredar;
	public Transform PuntoDeAtaque;
	public Transform AreaDeVision;
	public Transform BrazoAtaque;
	public Transform Puño;

	[Header("Parametros")]
	public NavMeshAgent Agente;
	public Animator Animador;
	private Vector3 PosicionEnSpawn;
	private Vector3 Destino;
	private Transform UltimaPosicion;
	public GameObject Personaje;
	public string [] Estados = {"Idle" , "Searching" , "Chasing" ,  "Attack"};


	[Header("BoxColliderBrazo")]
	public SphereCollider PuñoIzquierdo;

	[Header("Variables")]
	public string EstadoActual;
	public float AreaIdle;
	public float RangoDeVision;
	public float RangoAtaque;
	public float Damage;
	private int Pmask;
	private int EMask;
	public bool PermitirAtaque = false;
	public bool PermitirRotacion = false;
	public bool InterrumpirCorrutina = false;
	public bool PermitirColision = false;
	
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

		PuñoIzquierdo.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(EstadoActual != Estados[2] && Agente.remainingDistance < 0.2f)
		{
			FindObjectOfType<PositionManager>().Llegue(Destino);
			Animador.SetBool("Idle" , true);
			Animador.SetBool("Caminando" , false);
		}


		if(EstadoActual == Estados[3])
		{
			Animador.SetBool("Atacando" , true);
			Animador.SetBool("Caminando" , false);
			Agente.isStopped = true;
			
		}
		else if(BuscarPersonaje() && PuedoVer())
		{
			//Debug.Log("Te puedo ver");
			Acercar();
			Animador.SetBool("Caminando" , true);
			Animador.SetBool("Idle" , false);

			InterrumpirCorrutina = false;
		}
		else if(EstadoActual == Estados[2])
		{
			Buscar();
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
		Collider [] Obj = Physics.OverlapSphere(AreaDeVision.position , RangoDeVision);

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
				//Debug.Log("Te vi y te voy a cagar a ñapis");

				if(PermitirRotacion == false)
				{
					PermitirRotacion = true;
					StartCoroutine(Rotar());
				}
			}
		}
	}

	public void Acercar()
	{
		Agente.destination = Personaje.transform.position;
		EstadoActual = Estados[2];

		if(PermitirAtaque == false)
		{
			DetectarAtaque();
		}
	}

	public void Buscar()
	{
		UltimaPosicion = Personaje.transform;
		Agente.isStopped = false;

		EstadoActual = Estados[1];

		InterrumpirCorrutina = true;
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

	IEnumerator Rotar()
	{
		int Contador = 0;

		while(Contador <= 1000)
		{
			yield return new WaitForEndOfFrame();

			Contador += 1;

			Vector3 Angulos = new Vector3(0 , 1 , 0);

			if(Vector3.Distance(Puño.transform.position , Personaje.transform.position) <= 1.4f)
			{
				Contador = 0;
				EstadoActual = Estados[3];
				//Debug.Log("Funciono");
				InterrumpirCorrutina = false;
				yield break;
			}
			else
			{
				transform.Rotate(Angulos);
			}

			if(InterrumpirCorrutina == true)
			{
				Contador = 0;
				InterrumpirCorrutina = false;
				yield break;
			}

			//Debug.Log("Esto anda");
		}
		
		InterrumpirCorrutina = false;
		Contador = 0;
		yield return null;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Personaje")
		{
			if(PermitirColision == false)
			{
				EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
				//Debug.Log("Te meti tremenda ñapi");
				PermitirColision = true;

				Vector3 dir = transform.forward;
				dir.y = 3f;
				float Fuerza = 10;

				StartCoroutine(KnockBack(dir , Fuerza));

				EstadisticasDePersonaje.RecibirDaño(Damage);
			}
		}
	}

	IEnumerator KnockBack(Vector3 Dir , float Fuerza)
	{
		for(int i = 0; i < 30; i++)
		{
			Personaje.GetComponent<CharacterController>().Move(Dir * Time.deltaTime * Fuerza);
			yield return null;
		}
	}
}
