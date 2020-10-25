using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    GameObject Personaje;
    Viento3 Enemigo;
    public ParticleSystem Tornado;
    public float VelocidadTornado;
    public bool TornadoDesapareciendo = false;
    // Start is called before the first frame update
    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Personaje");

        StartCoroutine(TimerTornado());
    }

    // Update is called once per frame
    void Update()
    {
        if(TornadoDesapareciendo == false)
        {
            transform.position = Vector3.MoveTowards(transform.position , Personaje.transform.position , VelocidadTornado * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider Collision)
    {
        if(Collision.gameObject.tag == "Personaje")
        {
            if(TornadoDesapareciendo == false)
            {
                TornadoDesapareciendo = true;
                StartCoroutine(DesaparecerTornado());  

                Enemigo = FindObjectOfType<Viento3>();
                EstadisticasDePersonaje EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
                
                EstadisticasDePersonaje.RecibirDaño(Enemigo.Damage);
            }
        }
    }

    IEnumerator DesaparecerTornado()
    {
        Vector3 Escala = Tornado.transform.localScale;
        float Contador = 0;

        while(TornadoDesapareciendo == true)
        {
            yield return new WaitForEndOfFrame();
            
            Contador += 0.005f;

            float EscalaX = Escala.x - Contador;
            float EscalaY = Escala.y - Contador;
            float EscalaZ = Escala.z - Contador;

            Tornado.transform.localScale = new Vector3(EscalaX , EscalaY , EscalaZ);

            if(Tornado.transform.localScale.x < 0.1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator TimerTornado()
    {
        yield return new WaitForSeconds(6f);

        TornadoDesapareciendo = true;
        StartCoroutine(DesaparecerTornado());
    }
}
