using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuego3Anim : MonoBehaviour
{

    private Fuego3 Padre;

    // Use this for initialization
    void Start()
    {
        Padre = GetComponentInParent<Fuego3>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Disparar()
    {
        Padre.anim.speed = 1;
        Padre.ListoParaDisparar = true;
    }
    public void Apagar()
    {

        Padre.Apagar();
    }
    public void TerminoTP()
    {
        Padre.TerminoAnimacion = true;
        Padre.anim.SetBool("Tepeo", false);
        

    }
    public void RecienAparezco()
    {
        Padre.transform.localScale = Padre.Escala;
        Padre.anim.SetBool("Aparecio", false);
        Padre.anim.speed = 5;

    }
    public void Apareci()
    {
        Padre.TermineDeAparecer = true;
        Padre.anim.speed = 1;

        Padre.anim.SetBool("Aparecio", true);
    }

}
