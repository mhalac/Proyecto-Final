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
        Padre.ListoParaDisparar = true;
    }
	public void Apagar()
    {
        Padre.Apagar();
    }
  
}
