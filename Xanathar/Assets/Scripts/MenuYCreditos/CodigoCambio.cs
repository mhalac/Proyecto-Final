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
}
