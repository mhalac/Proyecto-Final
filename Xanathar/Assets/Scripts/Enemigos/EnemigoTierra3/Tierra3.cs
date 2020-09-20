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

		Animador.SetBool("Idle" , true);
		Animador.SetBool("Caminando" , false);

		PuñoIzquierdo.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Agente.remainingDistance < 0.1f && EstadoActual != Estados[2])
		{
			FindObjectOfType<PositionManager>().Llegue(Destino);
			Animador.SetBool("Idle" , true);
			Animador.SetBool("Caminando" , false);
		}

		if(EstadoActual == Estados[3])
		{
			if(PermitirRotacion == false)
			{
				PermitirRotacion = true;
				Agente.isStopped = true;
				//Debug.Log("Aca roto");

				StartCoroutine(Rotar());
			}
		}
		else if(BuscarPersonaje() && PuedoVer())
		{
			Acercar();
			Animador.SetBool("Caminando" , true);
			Animador.SetBool("Idle" , false);

			EstarRangoAtaque();
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

	public void Acercar()
	{
		Agente.isStopped = false;
		Destino = Personaje.transform.position;
		Agente.destination = Destino;
		EstadoActual = Estados[2];
	}

	public void Buscar()
	{
		Destino = Personaje.transform.position;
		Agente.destination = Destino;
		EstadoActual = Estados[1];
	}

	public void EstarRangoAtaque()
	{
		Collider [] Obj = Physics.OverlapSphere(PuntoDeAtaque.transform.position , RangoAtaque);

		for(int i = 0; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.tag == "Personaje")
			{
				//Debug.Log("TE VOY A CAGAR A PIÑAS LEIBO");
				EstadoActual = Estados[3];
			}
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

	IEnumerator Rotar()
	{
		int Contador = 0;
		Vector3 Angulos = new Vector3(0,1,0);

		while(Contador <= 500)
		{
			yield return new WaitForEndOfFrame();

			if(Vector3.Distance(Puño.transform.position , Personaje.transform.position) <= 1.4f)
			{
				Contador = 0;
				Animador.SetBool("Caminando" , false);
				Animador.SetBool("Atacando" , true);
				InterrumpirCorrutina = false;
				//Debug.Log("Corrutina Finalizada");
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
				PermitirRotacion = false;
				PermitirAtaque = false;
				yield break;
			}

			Contador += 1;
		}

		InterrumpirCorrutina = false;
		PermitirRotacion = false;
		PermitirAtaque = false;
		Contador = 0;
		yield return null;
	}

	IEnumerator KnockBack(Vector3 Dir , float Fuerza)
	{
		for(int i = 0; i < 20; i++)
		{
			Personaje.GetComponent<CharacterController>().Move(Dir * Time.fixedDeltaTime * Fuerza);
			yield return null;
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

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(AreaDeVision.position , RangoDeVision);

		Gizmos.color = Color.red;
		Vector3 AreaCubo = new Vector3(AreaIdle * 2 , 2 , AreaIdle * 2);
		Gizmos.DrawWireCube(PosicionEnSpawn , AreaCubo);

		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere(Destino , 0.5f);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(PuntoDeAtaque.position , RangoAtaque);
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
}
