using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonJugar : MonoBehaviour
{
    public bool Pulsoboton = false;
    public AnimacionDesaparecer Iniciar;

    public void BotonApretado()
    {
        if(Pulsoboton == false)
        {
            Pulsoboton = true;
            Iniciar.IniciarJuego();
        }
    }
}
