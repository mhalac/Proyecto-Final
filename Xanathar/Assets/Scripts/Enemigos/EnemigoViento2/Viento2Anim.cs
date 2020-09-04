using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viento2Anim : MonoBehaviour
{
    public Viento2 Padre;
    public void ActivarParticulas()
    {
        Padre.Particulas.SetActive(true);
        Padre.AtaqueActivado = true;
        
        StartCoroutine(CoordinarAtaque());
    }

    public void TerminarAtaque()
    {
        Padre.Animador.SetBool("Flotando" , true);
        Padre.Animador.SetBool("Atacando" , false);

        Padre.Particulas.SetActive(false);

        Padre.AtaqueActivado = false;

        Padre.PuedoDisparar = false;

        Padre.EstadoActual = Padre.Estados[1];
    }

    IEnumerator CoordinarAtaque()
    {
        yield return new WaitForSeconds(0.7f);

        StartCoroutine(PermitirAtaque());
    }

    IEnumerator PermitirAtaque()
    {
        while(Padre.AtaqueActivado == true)
        {
            yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(0.3f);

            if(Padre.AtaqueActivado == false)
            {
                yield break;
            }

            Padre.OndasDaño();
        }

        yield return null;
    }
}
