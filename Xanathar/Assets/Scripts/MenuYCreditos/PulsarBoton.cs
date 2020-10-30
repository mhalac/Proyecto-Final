using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsarBoton : MonoBehaviour
{
    public static bool SePresionoBoton = false;
    public Animator AnimacionDesaparecer;

    public void IniciarJuego()
    {
        if(SePresionoBoton == false)
        {
            Debug.Log("Iniciar Juego");
            SePresionoBoton = true;
            AnimacionDesaparecer.SetBool("BotonPresionado" , true);
            CodigoCambio.NombreDeEscena = "Lobby";
        }
    }

    public void Creditos()
    {
        if(SePresionoBoton == false)
        {
            Debug.Log("Creditos");
            SePresionoBoton = true;
            AnimacionDesaparecer.SetBool("BotonPresionado" , true);
            CodigoCambio.NombreDeEscena = "Creditos";
        }
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego");
        SePresionoBoton = true;
        Application.Quit();
    }

    public void VolverAlMenu()
    {
        Debug.Log("Volver al menu");
        SePresionoBoton = true;
        AnimacionDesaparecer.SetBool("AtrasPulsado" , true);
        CodigoCambio.NombreDeEscena = "Menu";
    }
}
