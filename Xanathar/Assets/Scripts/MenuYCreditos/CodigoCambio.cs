using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodigoCambio : MonoBehaviour
{
    public static string NombreDeEscena;
    public void CambiarEscena()
    {
        PulsarBoton.SePresionoBoton = false;
        SceneManager.LoadScene(NombreDeEscena);
    }

    public void DesactivarPersonajeYTodo()
    {
        GameObject Jugador = GameObject.FindGameObjectWithTag("Personaje");
        GameObject CanvasHUD = GameObject.Find("ContenedorDeHUD2.0");
        if(Jugador != null && CanvasHUD != null)
        {
            ControlarPuertasYJefes.JefeDeAguaMuerto = false;
            ControlarPuertasYJefes.JefeDeFuegoMuerto = false;
            ControlarPuertasYJefes.JefeDeTierraMuerto = false;
            ControlarPuertasYJefes.JefeDeVientoMuero = false;

            Cursor.lockState = CursorLockMode.Confined;

            Destroy(Jugador);
            Destroy(CanvasHUD);
        }
    }
}
