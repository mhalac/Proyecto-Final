﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;


public class MovimientoPersonaje : MonoBehaviour
{
    private float fuerzaDesenfoque = .42f;
    private Vignette dashFade;
   public PostProcessVolume volume;
    //Script para el movimiento del personaje
    public bool PuedoCorrer = true;
    public bool AfectadoSlowCercano;
    public bool AfectadoSlowLejano;
    //El controlador de personaje
    public CharacterController Controlador;

    //Referencia para el script que va a tener todas las estadisticas del personaje
    public EstadisticasDePersonaje Stats;

    // Los ejes X e Z
    float X;
    float Z;

    //Bool para ver si el personaje esta corriendo o no
    [Header("Bools Para chequear estados del personaje")]
    public bool Corriendo;
    public bool EstaQuieto;
    public bool EstaSaltando = false;

    [Header("Variables para el salto")]
    //Vector para la fuerza de gravedad
    public Vector3 Velocidad;

    //CheckDePiso es el objeto abajo de nuestro personaje que choca con el piso
    public Transform CheckDePiso;

    //Distancia de piso es el radio de la esfera
    public float DistanciaDePiso = 0.5f;
    //Mascara de piso es para que chequee todos los objetos que tengan el tag "piso"
    public LayerMask MascaraDePiso;
    public bool EstaEnPiso;

    //Variables del Dash
    private float potencia;
    [Header("Variables de Dash")]
    public float VelocidadDash;
    public float DistanciaDeDash = 10;
    public bool YaDasheo = false;
    public float tiempoDash;
    public Vector3 VectorDash;

    //Variable Inmortal
    [Header("Acceso a Variable Inmortalidad")]
    public EstadisticasDePersonaje VaribaleInmortal;





    // Use this for initialization
    void Start()
    {
      
        volume.profile.TryGetSettings(out dashFade);
        dashFade.intensity.value = 0;
        PuedoCorrer = true;
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

        if(VaribaleInmortal.Inmortalidad && dashFade.intensity.value +  fuerzaDesenfoque <= 1)
        {
            potencia = potencia + fuerzaDesenfoque;
            dashFade.intensity.value = potencia;
            print(dashFade.intensity.value);
        }
        else if(dashFade.intensity.value  - fuerzaDesenfoque >= 0f)
        {
            potencia = potencia -fuerzaDesenfoque ;
            dashFade.intensity.value = potencia;
            print(dashFade.intensity.value);
        }

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
            EstaSaltando = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso)
        {
            Velocidad.y = Mathf.Sqrt(Stats.FuerzaDeSalto * -2 * Stats.Gravedad);
            EstaSaltando = true;
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
        

        if (Input.GetKey(KeyCode.W))
        {
            EstaQuieto = false;
        }
        else
        {
            EstaQuieto = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && EstaQuieto == false && Corriendo == false && PuedoCorrer)
        {
            Ataque c = FindObjectOfType<Ataque>();
            if (!c.anim.GetBool("atacando"))
            {
                Corriendo = true;
                //                Debug.Log("Esta CORRIENDO");
                Stats.VelocidadDeMovimiento += 6;
            }

        }
        else if (!Input.GetKey(KeyCode.LeftShift) && Corriendo == true || !Input.GetKey(KeyCode.W) && Corriendo == true || !PuedoCorrer && Corriendo)
        {
            Corriendo = false;
            //Debug.Log("PARO DE CORRER");
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

            Controlador.Move(VectorDash * Time.fixedDeltaTime * VelocidadDash);

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
