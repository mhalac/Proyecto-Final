using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaBala : MonoBehaviour
{
    // Start is called before the first frame update
    //Rigidbody Cuerpo;
    //bool Caida = false;

    Agua2 Padre;

    void Start()
    {
        //Cuerpo = GetComponent<Rigidbody>();

        Padre = FindObjectOfType<Agua2>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Cuerpo.velocity.y < 0 && Caida == false)
        {
            Debug.Log("Esto funca");
            Caida = true;
            Cuerpo.AddForce(-2.5f , -5 , -2.5f , ForceMode.Impulse);
        }
        */
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Personaje")
        {
            //Debug.Log("Te di puto");
            EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
            Estadisticas.RecibirDaño(Padre.Damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag != "Bala")
        {
            Destroy(gameObject);
        }
    }
    
}
