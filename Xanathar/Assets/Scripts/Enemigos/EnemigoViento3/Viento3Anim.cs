using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viento3Anim : MonoBehaviour
{
    public Viento3 Padre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IniciarAtaque()
    {
        
    }

    public void VolverIdle()
    {
        Padre.Animador.SetBool("Atacando" , false);
        Padre.Animador.SetBool("Flotando" , true);

        Padre.EstadoActual = Padre.Estados[2];

        Padre.StartCoroutine(Padre.EsperarParaTirarTornado());
    }
}
