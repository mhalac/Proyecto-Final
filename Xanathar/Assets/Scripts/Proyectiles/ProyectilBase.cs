﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilBase : MonoBehaviour
{
    public Transform PuntoDisparo;

    private Ray DisparoPosicion;

    private int PMask;
    private Vector3 IrPosicion;
    private float velocidad;
    private bool Selecionado = false;
	private bool Empezo;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Esperar());
		PMask = LayerMask.NameToLayer("Personaje");
        Quaternion lookRotation = Quaternion.LookRotation(IrPosicion);
        transform.rotation = Quaternion.Slerp(lookRotation, transform.rotation, Time.deltaTime);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Lanzar(IrPosicion, velocidad);

    }


    // casteas un rayo al jugador y despues lo amplificas para sacar el punto mas lejano en su direccion 
    private void Seleccionar()
    {
        DisparoPosicion = new Ray(PuntoDisparo.position, IrPosicion);
        IrPosicion = DisparoPosicion.GetPoint(1000);
        Selecionado = true;
    }
    public void Enemigo(Vector3 direccion)
    {
        Debug.DrawRay(PuntoDisparo.position, IrPosicion, Color.green);
    }
   // Lo del IEnumerator es para que no detecte que colisiono contra si mismo cuando se dispara
    IEnumerator Esperar()
	{
		yield return new WaitForSeconds(0.1f);
		Empezo = true;
	}
	

    //funcion llamada recien se instancia para que la bala tenga los paramentros de la posicion del jugador y la velocidad heredada del enemigo
    public void Lanzar(Vector3 Objetivo, float speed)
    {
        //El if esta para que lo seleccione solo una vez, en la posicion que le dieron en un principio
		//print(Colisiono());
        Debug.DrawRay(transform.position, Objetivo, Color.magenta);
        Enemigo(Objetivo);
        if (!Selecionado)
        {
            velocidad = speed;
            IrPosicion = Objetivo;
            Seleccionar();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Objetivo, speed * Time.deltaTime);
        }

    }
    
	void OnCollisionEnter(Collision collision) {
        print("Collisione");
		if(collision.gameObject.tag == "Jugador")
		{
			//hacer damage
			Destroy(gameObject);
			
		}
        else if(collision.gameObject.tag != "Enemigo")
		{
			Destroy(gameObject);
		}
        
    }
	void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(IrPosicion, 3f);

    }
}
