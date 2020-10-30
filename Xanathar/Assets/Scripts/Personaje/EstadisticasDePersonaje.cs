using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasDePersonaje : MonoBehaviour
{
    public static GameObject Jugador;

    //Estadisticas Basicas
    [Header("Estadisticas Basicas")]
    public float VidaMaximaPersonaje = 20;
    public float VidaActualPersonaje = 20;
    public float DañoDePersonajeNormal = 10f;
    public float[] TiempoCooldownActivas;
    public int Armadura = 15;
    public float Gravedad = -15f;
    public int ContadorRoboDeVida = 0;
    public float CoolDownFlash;
    public float TiempoInmortal;
    public bool Inmortalidad = false;
    public float TiempoDeRegeneracion = 10f;

    public bool EstaMuerto = false;
    public bool MurioDespuesDeMatarJefe = false;
    public bool RoboDeVida = false;
    public bool LanzarMisiles = false;
    public bool PermitirFrostWalkers = false;

    public float VelocidadDeMovimiento = 10f;
    public float CoolDownAtaque = 1f;
    public float FuerzaDeSalto = 3f;
    public float VelocidadDeDash = 8;

    [Header("Daños Elementales")]

    //Daños Elementales
    public float DañoElementalFuego = 0f;
    public float DañoElementalAire = 0f;
    public float DañoElementalAgua = 0f;
    public float DañoElementalTierra = 0f;


    [Header("Variables que guardan las estadisticas iniciales")]
    public int ArmaduraInicialCopia;
    public float DañoDePersonajeInicialCopia;
    public float VidaMaximaDePersonajeInicialCopia;
    public float VelocidadDeMovimientoInicialCopia;

    float VelocidadParticula;
    void Start()
    {
        //Igualamos las variables que guardan las estadisticas 
        ArmaduraInicialCopia = Armadura;
        DañoDePersonajeInicialCopia= DañoDePersonajeNormal;
        VidaMaximaDePersonajeInicialCopia = VidaMaximaPersonaje;
        VelocidadDeMovimientoInicialCopia = VelocidadDeMovimiento;


        //Funcion para mantener al Jugador entre escenas
        if (Jugador != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Jugador = this.gameObject;
        GameObject.DontDestroyOnLoad(this.gameObject);

        
    }

    //Funcion para rexibir daño en bas a la armadura
    public void RecibirDaño(float Daño)
    {
        if (!Inmortalidad)
        {
            ManejadorDeItems ActualizadorDeVida;
            ActualizadorDeVida = FindObjectOfType<ManejadorDeItems>();

            float RestadorDeDaño = Daño / (Daño + Armadura);
            
            RestadorDeDaño = RestadorDeDaño * Daño;

            VidaActualPersonaje -= Mathf.RoundToInt(RestadorDeDaño);
            
            ActualizadorDeVida.ManejadorDeVida();
        }
    }

    public IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1f);

        VelocidadDeMovimiento += 3;
        SistemaDeParticulas.JugadorTocoBaba = false;
    }

    public void DetectoColisionParticulas()
    {
        if(SistemaDeParticulas.JugadorTocoBaba == false)
        {
            SistemaDeParticulas.JugadorTocoBaba = true;
            VelocidadParticula = VelocidadDeMovimiento;

            if(!Input.GetKeyDown(KeyCode.LeftShift))
            {
                VelocidadParticula += 6;
            }
            
            VelocidadDeMovimiento -= 3;

            StartCoroutine(Esperar());
        }
    }
}
