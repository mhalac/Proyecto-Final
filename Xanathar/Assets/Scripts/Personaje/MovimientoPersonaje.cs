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
	const float TiempoMaximoDeDash = 1f;
	float TiempoActualDeDash = TiempoMaximoDeDash;
	float DistanciaDeDash = 10f;
	float VelocidadCuandoElDashPara = 0.1f;
	float CoolDownDash = 5f;
	bool YaDasheo = false;

	//Variable que determina la velocidad y distancia del dash
	float VelocidadDash = 10;

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

		Saltar();
		
		Moverse();

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
		else if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			Stats.VelocidadDeMovimiento -= 6;
		}

		//Movimiento De Dash
		//Una vez que se apriete el boton Tiempo actual de Dash vale 0 y se ejecuta el segundo if
		if(Input.GetKeyDown(KeyCode.LeftControl) && YaDasheo == false)
		{
			TiempoActualDeDash = 0;
			YaDasheo = true;
		}

		if(YaDasheo == true)
		{
			CoolDownDash -= Time.fixedDeltaTime;

			if(CoolDownDash <= 0)
			{
				YaDasheo = false;
				CoolDownDash = 5f;
			}
		}

		if(TiempoActualDeDash < TiempoMaximoDeDash)
		{
			DireccionDeMovimiento = transform.forward * DistanciaDeDash;
			TiempoActualDeDash += VelocidadCuandoElDashPara;
		}
		else
		{
			DireccionDeMovimiento = Vector3.zero;
		}
		Controlador.Move(DireccionDeMovimiento * Time.deltaTime * VelocidadDash);
	}
}
