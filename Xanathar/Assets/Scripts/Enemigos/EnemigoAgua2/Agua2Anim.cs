using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua2Anim : MonoBehaviour
{
   public Agua2 Padre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LanzarProyectil()
    {
        Padre.DispararCañon();
    }

    public void DesactivarAtaque()
    {
        Padre.Animador.SetBool("Idle" , true);
        Padre.Animador.SetBool("Visto" , false);

        //Debug.Log("Esto se ejecuta");

        StartCoroutine(Esperar());
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1f);
        Padre.PermitirDisparo = false;
        Padre.EstadoActual = Padre.Estados[1];
    }
}
