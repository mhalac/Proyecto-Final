using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoPersonaje : MonoBehaviour
{

    //Script para el movimiento del personaje

    //El controlador de personaje
    public CharacterController Controlador;

    //Referencia para el script que va a tener todas las estadisticas del personaje
    public EstadisticasDePersonaje Stats;

    // Los ejes X e Z
    float X;
    float Z;

    //Bool para ver si el personaje esta corriendo o no
    public bool Corriendo;
    public bool EstaQuieto;

    //Vector para la fuerza de gravedad
    Vector3 Velocidad;

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

    [Header("Acceso a Variable Inmortalidad")]
    public EstadisticasDePersonaje VaribaleInmortal;


    //Variables para Determinar si el jugador se esta moviendo o no
    /*
	[Header("Variables para chequear si el usuario Se Movio")]
	public Transform ObjetoReferenciador;
	public float LimiteDistanciaEntrePosiciones = 0.0001f;
	private const int FramesParaDetectar = 3;
	Vector3[] ListaDePosicionesAnteriores = new Vector3[FramesParaDetectar];
	public bool EstaEnIdle = false;
	*/

    // Use this for initialization
    void Start()
    {
        /*
		for(int i = 0; i < ListaDePosicionesAnteriores.Length; i++)
		{
			ListaDePosicionesAnteriores[i] = Vector3.zero;
		}
		*/
        Corriendo = false;

        VaribaleInmortal = FindObjectOfType<EstadisticasDePersonaje>();
    }

    // Update is called once per frame
    void Update()
    {
        //Crea una esfera invisible que se encarga de chequear si colisiona con el piso
        EstaEnPiso = Physics.CheckSphere(CheckDePiso.position, DistanciaDePiso, MascaraDePiso);
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");

        //Movimientos

        Saltar();

        Moverse();

        Dash();

        //Debug.Log(1.0f / Time.deltaTime);
    }

    void Saltar()
    {
        if (EstaEnPiso && Velocidad.y < 0)
        {
            Velocidad.y = -Stats.VelocidadDeMovimiento;
        }

        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso)
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
        /*
		for(int i = 0; i < ListaDePosicionesAnteriores.Length -1; i++)
		{
			ListaDePosicionesAnteriores[i] = ListaDePosicionesAnteriores[i+1];
		}

		ListaDePosicionesAnteriores[ListaDePosicionesAnteriores.Length - 1] = ObjetoReferenciador.position;

		for(int i = 0; i < ListaDePosicionesAnteriores.Length - 1; i++)
		{
			if(Vector3.Distance(ListaDePosicionesAnteriores[i] , ListaDePosicionesAnteriores[i + 1]) <= LimiteDistanciaEntrePosiciones)
			{
				EstaEnIdle = false;
				Debug.Log("Esta Quieto");
				break;
			}
			else
			{
				EstaEnIdle = true;
				Debug.Log("Se Mueve");
			}
		}
		*/

        //Movimiento De Correr
        /*
		if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
		{
			Stats.VelocidadDeMovimiento += 6;
			Corriendo = true;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.W))
		{
			Stats.VelocidadDeMovimiento -= 6;
			Corriendo = false;
		}
		*/

        if (Input.GetKey(KeyCode.W))
        {
            EstaQuieto = false;
        }
        else
        {
            EstaQuieto = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && EstaQuieto == false && Corriendo == false)
        {
            Ataque c = FindObjectOfType<Ataque>();
            if (!c.anim.GetBool("atacando"))
            {
                Corriendo = true;
                Debug.Log("Esta CORRIENDO");
                Stats.VelocidadDeMovimiento += 6;
            }

        }
        else if (!Input.GetKey(KeyCode.LeftShift) && Corriendo == true || !Input.GetKey(KeyCode.W) && Corriendo == true)
        {
            Corriendo = false;
            Debug.Log("PARO DE CORRER");
            Stats.VelocidadDeMovimiento -= 6;
        }
    }

    void Dash()
    {
        //Movimiento Dash
        //3 Estados Inicio Dash , Dasheo , Cooldown Dash

        if (Input.GetKeyDown(KeyCode.LeftControl) && YaDasheo == false)
        {
            VelocidadDash = Stats.VelocidadDeDash;
            YaDasheo = true;
            tiempoDash = Stats.CoolDownFlash;
            //Debug.Log("Esto se repite solo 1 vez");
        }

        if (YaDasheo == true)
        {

            if (VelocidadDash > 0)
            {
                VaribaleInmortal.Inmortalidad = true;
                VectorDash = transform.forward * DistanciaDeDash;
                VelocidadDash -= 1;
            }
            else
            {
                VaribaleInmortal.Inmortalidad = false;
                VectorDash = Vector3.zero;
            }

            Controlador.Move(VectorDash * Time.deltaTime * VelocidadDash);

            tiempoDash -= Time.fixedDeltaTime;

            if (tiempoDash <= 0)
            {
                YaDasheo = false;
                tiempoDash = Stats.CoolDownFlash;
                VelocidadDash = Stats.VelocidadDeDash;
            }
        }
    }
}
