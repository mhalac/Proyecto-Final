using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tierra3 : MonoBehaviour {
	
	[Header("Transforms Seleccionables")]
	public Transform Heredar;
	public Transform PuntoDeVision;
	public Transform PuntoAtaque;

	[Header("Collider Puño")]
	public SphereCollider ColisionMameta;

	[Header("Variables")]
	public float AreaDeVision;
	public float RangoIdle;
	public string [] Estados = {"Idle" , "Searching" , "Chasing" , "Attack"};
	public string EstadoActual;
	public bool PermitirRotacion = false;

	[Header("Parametros")]
	Vector3 Destino;
	NavMeshAgent Agente;
	public Animator Animador;
	GameObject Personaje;


	// Use this for initialization
	void Start ()
	{
		EstadoActual = Estados[0];

		Agente = GetComponent<NavMeshAgent>();
		ColisionMameta.enabled = false;

		Animador.SetBool("Idle" , true);
		Animador.SetBool("Caminando" , false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(EstadoActual == Estados[0] && Agente.remainingDistance < Mathf.Epsilon)
		{
			Animador.SetBool("Caminando" , false);
			Animador.SetBool("Idle" , true);
		}

		/*
		if(DetectarPersonaje() && PermitirRotacion == false)
		{
			PermitirRotacion = true;
			Agente.isStopped = true;
			StartCoroutine(Rotar());
			//transform.LookAt(Personaje.transform);
			
			Vector3 PosRelativa = Personaje.transform.position - transform.position;

			Quaternion Rotacion = Quaternion.LookRotation(PosRelativa , Vector3.up);

			Rotacion.x = 0;
			Rotacion.z = 0;
			transform.rotation = Rotacion;
			
		}
		*/
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
			
			Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(Heredar.position , RangoIdle , Heredar.position);
			Agente.destination = Destino;
		}
	}

	public bool DetectarPersonaje()
	{
		Collider [] Obj = Physics.OverlapSphere(PuntoDeVision.position ,AreaDeVision);

		for(int i = 0 ; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.tag == "Personaje")
			{
				Personaje = Obj[i].gameObject;
				return true;
			}
		}

		return false;
	}

	/*
	IEnumerator Rotar()
	{
		int Contador = 0;
		Vector3 Angulos = new Vector3(0,120,0);
		transform.LookAt(Personaje.transform);

		while (Contador <= 30)
		{
			yield return new WaitForEndOfFrame();

			if(Vector3.Distance(PuntoAtaque.transform.position , Personaje.transform.position) < 1.5f)
			{
				Debug.Log("XDDDD");
				yield break;
			}
			else
			{
				transform.Rotate(Angulos * Time.deltaTime);
			}
			Contador += 1;
		}

		Debug.Log("Esto nmo deberia");
		yield return null;
	}
	*/
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(PuntoDeVision.position , AreaDeVision);

		Gizmos.color = Color.white;
		Vector3 AreaIdle = new Vector3(RangoIdle * 2 , 2 , RangoIdle * 2);
		Gizmos.DrawWireCube(Heredar.position , AreaIdle);

		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(Destino , 0.5f);
	}
}
