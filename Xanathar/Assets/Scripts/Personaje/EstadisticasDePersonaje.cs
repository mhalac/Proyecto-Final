﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasDePersonaje : MonoBehaviour
{
    static GameObject Jugador;

    //Estadisticas Basicas
    [Header("Estadisticas Basicas")]
    public float VidaMaximaPersonaje = 20;
    public float VidaActualPersonaje = 20;
    public float DañoDePersonajeNormal = 10f;
    public float TiempoCooldownActivas = 5f;
    public int Armadura = 15;
    public float Gravedad = -15f;
    public float CoolDownFlash;
    public float TiempoInmortal;
    public bool Inmortalidad = false;
    public float TiempoDeRegeneracion = 15f;

    
    public float VelocidadDeMovimiento = 10f;
    public float CoolDownAtaque = 1f;
    public float FuerzaDeSalto = 3f;
    public float VelocidadDeDash;

    [Header("Daños Elementales")]

    //Daños Elementales
    public float DañoElementalFuego = 0f;
    public float DañoElementalAire = 0f;
    public float DañoElementalAgua = 0f;
    public float DañoElementalTierra = 0f;

    void Start()
    {
        /*
		PlayerPrefs.SetInt("Vida", VidaDePersonaje);
		PlayerPrefs.SetFloat("DañoNormal", DañoDePersonajeNormal);
		PlayerPrefs.SetFloat("Cooldown", TiempoCooldownActivas);
		PlayerPrefs.SetInt("Armadura", Armadura);
		PlayerPrefs.SetFloat("Gravedad", Gravedad);

		PlayerPrefs.SetFloat("VelocidadDeMovimiento", VelocidadDeMovimiento);
		PlayerPrefs.SetFloat("VelocidadDeAtaque", VelocidadDeAtaque);
		PlayerPrefs.SetFloat("FuerzaDeSalto", FuerzaDeSalto);

		PlayerPrefs.SetFloat("DañoFuego", DañoElementalFuego);
		PlayerPrefs.SetFloat("DañoViento", DañoElementalAire);
		PlayerPrefs.SetFloat("DañoAgua", DañoElementalAgua);
		PlayerPrefs.SetFloat("DañoTierra", DañoElementalTierra);
		*/

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
}
