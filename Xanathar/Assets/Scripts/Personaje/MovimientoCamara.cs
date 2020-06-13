using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour {

	//Script para mover la camara en eje X e Y
	//Script para limitar el movimiento de la camara en eje Y para evitar que de un giro 360 grados

	public float Sensibilidad = 100f;
	float MouseX;
	float MouseY;
	float RotacionX = 0f;

	public Transform CuerpoJugador;

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () 
	{
		MouseX = Input.GetAxis("Mouse X") * Sensibilidad * Time.deltaTime;
		MouseY = Input.GetAxis("Mouse Y") * Sensibilidad * Time.deltaTime;

		RotacionX -= MouseY;
		RotacionX = Mathf.Clamp(RotacionX , -45f , 45f);

		transform.localRotation = Quaternion.Euler(RotacionX , 0f , 0f);
		CuerpoJugador.Rotate(Vector3.up * MouseX);
	}
}
