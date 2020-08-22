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
    private RaycastHit golpe;

    public GameObject PuntoDeOrbita;
    private bool EstaAbajo;

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

    private Quaternion defaultRotation;

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
        defaultRotation = transform.rotation;
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
        Vector3 PosicionRayo = new Vector3(Jugador.transform.position.x, Jugador.transform.position.y, Jugador.transform.position.z + 5.5f);
        DispararArriba = new Ray(PosicionRayo, transform.up);
        Vector3 pos = DispararArriba.GetPoint(10);
        transform.rotation = defaultRotation;

        //print(Vector3.Distance(ManoDerecha.transform.position, pos));
        Debug.DrawRay(Jugador.transform.position, transform.up * 9999);
        if (Vector3.Distance(ManoDerecha.transform.position, pos) < 7f)
        {
            if (Physics.Raycast(Jugador.transform.position, transform.up, out golpe, 999))
            {
                if (golpe.transform.tag == "JefeViento")
                {
                    EstaAbajo = true;
                }
                else
                    EstaAbajo = false;
            }
        }
        if ((EstaAbajo && !EstaAplastando && !AnimatorDerecha.GetBool("Aplastar")))
        {
            AnimatorDerecha.SetBool("Aplastar", true);
            EstaAbajo = false;
        }
        else if (!EstaAplastando)
        {
            AnimatorDerecha.SetBool("Aplastar", false);
            AmbasManos.transform.position = Vector3.MoveTowards(AmbasManos.transform.position, pos, VelocidadDerecha * Time.deltaTime);
        }

    }
    private void Fase2()
    {
        transform.rotation = defaultRotation;
        Vector3 PosicionRayo = new Vector3(Jugador.transform.position.x - 1.7f, Jugador.transform.position.y + 2.9f, Jugador.transform.position.z);
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
        Orbitar();
        if (!ApareciendoTornados)
        {
            ResetAnimaciones();
            Invoke("HabilitarManos", 6f);
        }
    }
    private Quaternion Apuntar(Transform Desde, Vector3 Hasta, float velocidad)
    {
        Vector3 direction = (Hasta - Desde.position).normalized;
        Quaternion rotar = Quaternion.Slerp(AmbasManos.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * velocidad);

        return rotar;
    }
    private void Orbitar()
    {
        //transform.rotation = Quaternion.Euler(0,-180,0);

      

        if (Vector3.Distance(AmbasManos.transform.position, PuntoDeOrbita.transform.position) < 20 && Vector3.Distance(AmbasManos.transform.position, PuntoDeOrbita.transform.position) > 3)
            transform.RotateAround(Jugador.transform.position, Vector3.up, 40 * Time.deltaTime);
        else
            AmbasManos.transform.position = Vector3.MoveTowards(AmbasManos.transform.position, PuntoDeOrbita.transform.position, 10 * Time.deltaTime);

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
            GameObject d = Instantiate(Rayo, c.transform.position + Vector3.up, Rayo.transform.rotation);
            Destroy(c, TiempoDeVidaTornados);
            Destroy(d, TiempoDeVidaTornados);
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
