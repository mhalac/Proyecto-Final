using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeViento : MonoBehaviour
{

    public GameObject Jugador;
    public GameObject AmbasManos;
    public GameObject ManoDerecha;
    public float VelocidadDerecha;
    public Animator AnimatorDerecha;
    public Transform OjoDerecho;
    public GameObject ManoIzquierda;
    //private Animator AnimDerecha;
    private Ray DispararArriba;
    public Estados estado;
    // Use this for initialization
    public enum Estados
    {
        Fase1,
        Fase2,
        Fase3
    }

    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Personaje");
        estado = Estados.Fase1;
        // AnimatorDerecha = GameObject.Find("Manos").GetComponent<Animator>();
        AnimatorDerecha = GetComponent<Animator>();
    }


    // Update is called once per frame

    void Update()
    {
        switch (estado)
        {
            case Estados.Fase1:
                Fase1();
                return;
            case Estados.Fase2:
                Fase2();
                return;
            case Estados.Fase3:
                Fase3();
                return;

        }


    }
    private Quaternion Apuntar(Transform Desde, Vector3 Hasta, float velocidad)
    {
        Vector3 direction = (Hasta - Desde.position).normalized;
        Quaternion rotar = Quaternion.Slerp(Desde.rotation, Quaternion.LookRotation(direction), Time.deltaTime * velocidad);

        return rotar;
    }
    private void Fase1()
    {
        DispararArriba = new Ray(Jugador.transform.position, transform.up);
        Vector3 pos = DispararArriba.GetPoint(10);
        ManoDerecha.transform.position = Vector3.MoveTowards(ManoDerecha.transform.position, pos, VelocidadDerecha * Time.deltaTime);
        //print(Vector3.Distance(ManoDerecha.transform.position, pos));

        if (Vector3.Distance(ManoDerecha.transform.position, pos) < 4f)
        {
            AnimatorDerecha.SetBool("Aplastar", true);
        }

    }
    private void Fase2()
    {

    }
    private void Fase3()
    {

    }
}
