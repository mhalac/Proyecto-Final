using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{

    public float DistanciaLejana;
    public float DistanciaCercana;
    
    public float SlowLejano;
    public float SlowCercano;
    public bool EstaEnArea;
    public bool EstaEnAreaCercana;
    public bool EntroCorriendo;

    private GameObject Personaje;

    private MovimientoPersonaje movimientoPersonaje;
    // Use this for initialization
    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Personaje");
        movimientoPersonaje = FindObjectOfType<MovimientoPersonaje>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position, Personaje.transform.position);

    

        if (dis < DistanciaLejana && !EstaEnAreaCercana && !movimientoPersonaje.AfectadoSlowLejano)
        {
			movimientoPersonaje.PuedoCorrer = false;
            movimientoPersonaje.AfectadoSlowLejano = true;
            EstaEnAreaCercana = true;
            movimientoPersonaje.Stats.VelocidadDeMovimiento -= SlowLejano;

        }
        else if (dis > DistanciaLejana && EstaEnAreaCercana)
        {
			movimientoPersonaje.PuedoCorrer = true;
            movimientoPersonaje.AfectadoSlowLejano = false;
            EstaEnAreaCercana = false;
            movimientoPersonaje.Stats.VelocidadDeMovimiento += SlowLejano;
        }
        if (dis < DistanciaLejana && !EstaEnArea && !movimientoPersonaje.AfectadoSlowCercano)
        {
            movimientoPersonaje.AfectadoSlowCercano = true;

            movimientoPersonaje.Stats.VelocidadDeMovimiento -= SlowCercano;
            EstaEnArea = true;
        }
        else if (dis > DistanciaLejana && EstaEnArea)
        {
            movimientoPersonaje.AfectadoSlowCercano = false;

            EstaEnArea = false;
            movimientoPersonaje.Stats.VelocidadDeMovimiento += SlowCercano;
        }

    }
}
