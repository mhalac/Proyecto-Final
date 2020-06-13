using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour {

	//Script para el movimiento del personaje

	//El controlador de personaje
	public CharacterController Controlador;

	//Referencia para el script que va a tener todas las estadisticas del personaje
	public EstadisticasDePersonaje Stats;

	// Los ejes X e Z
	float X;
	float Z;

	//Este vector 3 es para la fuerza de gravedad
	Vector3 Velocidad;

	//CheckDePiso es el objeto abajo de nuestro personaje que choca con el piso
	public Transform CheckDePiso;

	//Distancia de piso es el radio de la esfera
	public float DistanciaDePiso = 0.5f;
	//Mascara de piso es para que chequee todos los objetos que tengan el tag "piso"
	public LayerMask MascaraDePiso;
	bool EstaEnPiso;
	

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Crea una esfera invisible que se encarga de chequear si colisiona con el piso
		EstaEnPiso = Physics.CheckSphere(CheckDePiso.position , DistanciaDePiso , MascaraDePiso);

		X = Input.GetAxis("Horizontal");
		Z = Input.GetAxis("Vertical");

		//Movimientos

		if(EstaEnPiso && Velocidad.y < 0)
		{
			Velocidad.y =- Stats.VelocidadDeMovimiento;
		}

		if(Input.GetKeyDown(KeyCode.Space) && EstaEnPiso)
		{
			Velocidad.y = Mathf.Sqrt(Stats.FuerzaDeSalto * -2 * Stats.Gravedad);
		}

		Vector3 Movimiento = transform.right * X + transform.forward * Z;
		Velocidad.y += Stats.Gravedad * Time.deltaTime;

		//Movimiento eje X y Z
		Controlador.Move(Movimiento * Stats.VelocidadDeMovimiento * Time.deltaTime);

		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			Stats.VelocidadDeMovimiento += 6;
		}
		else if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			Stats.VelocidadDeMovimiento -= 6;
		}

		//Movimiento Y
		Controlador.Move(Velocidad * Time.deltaTime);

	}
}
