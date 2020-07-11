using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoPersonaje : MonoBehaviour {

	//Script para el movimiento del personaje

	//El controlador de personaje
	public CharacterController Controlador;

	//Referencia para el script que va a tener todas las estadisticas del personaje
	public EstadisticasDePersonaje Stats;

	// Los ejes X e Z
	float X;
	float Z;

	//Vector para la fuerza de gravedad
	Vector3 Velocidad;
	//Vector 3 para la fuerza del dash
	Vector3 DireccionDeMovimiento;

	//CheckDePiso es el objeto abajo de nuestro personaje que choca con el piso
	public Transform CheckDePiso;

	//Distancia de piso es el radio de la esfera
	float DistanciaDePiso = 0.5f;
	//Mascara de piso es para que chequee todos los objetos que tengan el tag "piso"
	public LayerMask MascaraDePiso;
	bool EstaEnPiso;

	//Variables del Dash
	public bool YaDasheo = false;
	public float tiempoDash;

	public Vector3 VectorDash;

	//Variable que determina la velocidad y distancia del dash
	float VelocidadDash;
	float DistanciaDeDash = 10;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Crea una esfera invisible que se encarga de chequear si colisiona con el piso
		EstaEnPiso = Physics.CheckSphere(CheckDePiso.position , DistanciaDePiso , MascaraDePiso);
		EstaEnPiso = Physics.CheckSphere(CheckDePiso.position , DistanciaDePiso , MascaraDePiso);
		X = Input.GetAxis("Horizontal");
		Z = Input.GetAxis("Vertical");

		//Movimientos

		Saltar();

		Moverse();

		Dash();
	}

	void Saltar()
	{
		if(EstaEnPiso && Velocidad.y < 0)
		{
			Velocidad.y =- Stats.VelocidadDeMovimiento;
		}

		if(Input.GetKeyDown(KeyCode.Space) && EstaEnPiso)
		{
			Velocidad.y = Mathf.Sqrt(Stats.FuerzaDeSalto * -2 * Stats.Gravedad);
		}

		//Movimiento Y
		Controlador.Move(Velocidad * Time.deltaTime);
	}

	void Moverse()
	{
		Vector3 Movimiento = transform.right * X + transform.forward * Z;
		Velocidad.y += Stats.Gravedad * Time.deltaTime;

		//Movimiento eje X y Z
		Controlador.Move(Movimiento * Stats.VelocidadDeMovimiento * Time.deltaTime);

		//Movimiento De Correr
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			Stats.VelocidadDeMovimiento += 6;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			Stats.VelocidadDeMovimiento -= 6;
		}
	}

	void Dash()
	{
		//Movimiento Dash
		//3 Estados Inicio Dash , Dasheo , Cooldown Dash

		if(Input.GetKeyDown(KeyCode.LeftControl) && YaDasheo == false)
		{
			VelocidadDash = Stats.VelocidadDeDash;
			YaDasheo = true;
			tiempoDash = Stats.CoolDownFlash;
		}

		if(YaDasheo == true)
		{
			
			if(VelocidadDash > 0)
			{
				EstadisticasDePersonaje.Inmortalidad = true;
				VectorDash = transform.forward * DistanciaDeDash;
				VelocidadDash -= 1;
			}
			else
			{
				EstadisticasDePersonaje.Inmortalidad = false;
				VectorDash = Vector3.zero;
			}

			Controlador.Move(VectorDash * Time.deltaTime * VelocidadDash);

			tiempoDash -= Time.fixedDeltaTime;

			if(tiempoDash <= 0)
			{
				YaDasheo = false;
				tiempoDash = Stats.CoolDownFlash;
				VelocidadDash = Stats.VelocidadDeDash;
			}
		}
	}
}
