using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Viento1 : MonoBehaviour {

	private RaycastHit Hit;
	private Transform UltimaPosicion;

	[Header("Transforms Seleccionables")]
	public Transform Vision;
	public Transform Heredar;

	
	[Header("Variables")]
	public float RangoDeVision;
	public float AreaIdle;
	public float Damage;
	private int Emask;
	private int PMask;
	private string [] Estados = {"Idle" , "Chasing" , "Searching" , "CoolDown"};
	public string EstadoActual;
	public bool PermitirAtaque = false;


	[Header("Parametros")]
	private Vector3 PosicionInicial;
	private Vector3 Destino;
	private GameObject Personaje;
	public NavMeshAgent Agente;

	// Use this for initialization
	void Start ()
	{
		PosicionInicial = Heredar.position;
		Agente = GetComponent<NavMeshAgent>();

		EstadoActual = Estados[0];

		PMask = LayerMask.NameToLayer("Personaje");
		Emask = LayerMask.NameToLayer("Enemigo");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Agente.velocity.magnitude < 1f && EstadoActual != Estados[1])
		{
			FindObjectOfType<PositionManager>().Llegue(Destino);
			Idle();
		}

		if(Agente.remainingDistance < 0.7f && EstadoActual == Estados[1] && PermitirAtaque == false)
		{
			//Debug.Log("LLegue a tu pos ahora te cago a piñas");
			PermitirAtaque = true;
			Agente.isStopped = true;

			EstadisticasDePersonaje EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
			EstadisticasDePersonaje.RecibirDaño(Damage);

			StartCoroutine(CoolDownAtaque());
		}

		if(BuscarPersonaje() && PuedoVer())
		{
			Acercar();
		}
		else if(EstadoActual == Estados[1])
		{
			Buscar();
		}
	}

	public void Idle()
	{
		Agente.isStopped = false;
		EstadoActual = Estados[0];

		IrAPosRandom();
	}

	public void IrAPosRandom()
	{
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

			Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(PosicionInicial , AreaIdle , Heredar.position);
			Agente.destination = Destino;
		}
	}

	public bool BuscarPersonaje()
	{
		Collider [] Obj = Physics.OverlapSphere(Vision.position , RangoDeVision);

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

	public bool PuedoVer()
	{
		var Direccion = Personaje.transform.position - Heredar.position;

		if(Physics.Raycast(Vision.position , Direccion , out Hit , RangoDeVision , Emask) && Hit.collider.gameObject.tag != "Personaje")
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	public void Acercar()
	{
		Agente.destination = Personaje.transform.position;
		EstadoActual = Estados[1];
	}

	public bool TengoQueAcercarme()
	{
		Collider [] Obj = Physics.OverlapSphere(Vision.position , RangoDeVision);

		for(int i = 0; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.layer == PMask)
			{
				return true;
			}

			if(Obj[i].gameObject.layer == Emask)
			{
				float Distancia = Vector3.Distance(Obj[i].gameObject.transform.position , Heredar.transform.position);

				if(Distancia < 2f)
				{
					return false;
				}
			}
		}

		return false;
	}

	public void Buscar()
	{
		Agente.isStopped = false;
		EstadoActual = Estados[2];
	}

	IEnumerator CoolDownAtaque()
	{
		yield return new WaitForSeconds(1f);

		Agente.isStopped = false;
		PermitirAtaque = false;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(Vision.position , RangoDeVision);

		Gizmos.color = Color.blue;
		Vector3 AreaCubo = new Vector3(AreaIdle * 2 , 2 , AreaIdle * 2);
		Gizmos.DrawWireCube(PosicionInicial , AreaCubo);

		Gizmos.color = Color.gray;
		Gizmos.DrawSphere(Destino , 0.5f);
	}
}
