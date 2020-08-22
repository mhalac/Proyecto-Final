using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeViento : MonoBehaviour
{


    public bool ApareciendoTornados;

    private int CantidadTornadosActuales;

    public bool EstaAplastando;
    public GameObject Jugador;
    public GameObject AmbasManos;
    public GameObject ManoDerecha;

    public float VelocidadDerecha;
    public Animator AnimatorDerecha;

    [Header("ManoIZQ")]
    public GameObject ManoIzquierda;
    public Animator AnimatorIzquierda;
    public bool EstaEmpujando;

    [Header("Parametros Tornados")]

    public float TiempoDeVidaTornados;
    public float DistanciaDelJugador;
    public GameObject Tornado;
    public GameObject Rayo;
    public float IntervaloEntreTornados;
    public int CantidadTornados;

    //private Animator AnimDerecha;
    private Ray DispararArriba;
    public Estados estado;
    // Use this for initialization
    public enum Estados
    {
        Fase1,
        Fase2,
        Fase3,
        Fase4
    }

    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Personaje");
        //estado = Estados.Fase1;
        // AnimatorDerecha = GameObject.Find("Manos").GetComponent<Animator>();
        // AnimatorDerecha = GetComponent<Animator>();
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
            case Estados.Fase4:
                Fase4();
                return;

        }


    }


    public void ResetAnimaciones()
    {

        AnimatorDerecha.SetBool("Aplastar", false);
        AnimatorIzquierda.SetBool("Empujar", false);
        AnimatorIzquierda.SetBool("Tornado", false);
        AnimatorDerecha.SetBool("Tornado", false);

    }

    private void Fase1()
    {
        Vector3 PosicionRayo = new Vector3(Jugador.transform.position.x, Jugador.transform.position.y, Jugador.transform.position.z + 4);
        DispararArriba = new Ray(PosicionRayo, transform.up);
        Vector3 pos = DispararArriba.GetPoint(10);

        //print(Vector3.Distance(ManoDerecha.transform.position, pos));

        if ((Vector3.Distance(AmbasManos.transform.position, pos) < 2f && !EstaAplastando && !AnimatorDerecha.GetBool("Aplastar")))
        {
            AnimatorDerecha.SetBool("Aplastar", true);
        }
        else if (!EstaAplastando)
        {
            AnimatorDerecha.SetBool("Aplastar", false);
            AmbasManos.transform.position = Vector3.MoveTowards(AmbasManos.transform.position, pos, VelocidadDerecha * Time.deltaTime);
        }

    }
    private void Fase2()
    {
        Vector3 PosicionRayo = new Vector3(Jugador.transform.position.x - 2, Jugador.transform.position.y + 3.2f, Jugador.transform.position.z);
        DispararArriba = new Ray(PosicionRayo, transform.forward * -1);
        Vector3 pos = DispararArriba.GetPoint(10);
        Debug.DrawRay(DispararArriba.origin, DispararArriba.direction * 99);
        if (Vector3.Distance(AmbasManos.transform.position, pos) < 1f && !EstaEmpujando)
        {
            AnimatorIzquierda.SetBool("Empujar", true);
            AnimatorDerecha.SetBool("Levantate", true);
        }
        else if (!EstaEmpujando)
        {
            AmbasManos.transform.position = Vector3.MoveTowards(AmbasManos.transform.position, pos, VelocidadDerecha * Time.deltaTime);

        }
    }
    private void Fase3()
    {
        if (!ApareciendoTornados)
        {
            ResetAnimaciones();
            Invoke("HabilitarManos", 6f);

        }
    }

    private void HabilitarManos()
    {
        AnimatorIzquierda.SetBool("Tornado", true);
        AnimatorDerecha.SetBool("Tornado", true);
    }
    public void SpawnearTornados()
    {
        InvokeRepeating("ActuallyAparecerElTornado", 0, IntervaloEntreTornados);

    }
    private void ActuallyAparecerElTornado()
    {
        if (CantidadTornadosActuales < CantidadTornados)
        {

            Vector3 pos = new Vector3(Random.Range(Jugador.transform.position.x - DistanciaDelJugador, Jugador.transform.position.x + DistanciaDelJugador), Jugador.transform.position.y, Random.Range(Jugador.transform.position.z - DistanciaDelJugador, Jugador.transform.position.z + DistanciaDelJugador));
            GameObject c = Instantiate(Tornado, pos, Quaternion.identity);
            GameObject d = Instantiate(Rayo,c.transform.position + Vector3.up,Rayo.transform.rotation);
            Destroy(c, TiempoDeVidaTornados);
            Destroy(d,TiempoDeVidaTornados);
            CantidadTornadosActuales++;
        }

    }
    public void TermineAnimTornados()
    {
        AnimatorIzquierda.SetBool("Tornado", false);
        AnimatorDerecha.SetBool("Tornado", false);
        ApareciendoTornados = false;
        CancelInvoke("ActuallyAparecerElTornado");
        CantidadTornadosActuales = 0;
    }
    private void Fase4()
    {

    }

}
