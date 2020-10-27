using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimacionDesaparecer : MonoBehaviour
{
    public Animator AnimacionIniciarJuego;
    public void IniciarJuego()
    { 
        AnimacionIniciarJuego.SetBool("BotonJugarApretado" , true);
    }

    public void CambiarALobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
