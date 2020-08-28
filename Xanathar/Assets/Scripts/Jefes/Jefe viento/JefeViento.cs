using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeViento : MonoBehaviour
{


    public bool Termine;

    public Collider ColliderIzq;
    public Collider ColliderDer;

    public bool ApareciendoTornados;
    public Transform ReferenciaDerecha;
    private int CantidadTornadosActuales;
    public bool Mori;

    public float SlamKnockback;
    public bool EstaAplastando;
    public GameObject Jugador;
    public GameObject AmbasManos;
    public GameObject ManoDerecha;
    private RaycastHit golpe;

    public Vector3 AplastarSize;
    public GameObject PuntoDeOrbita;
    private bool EstaAbajo;

    public float VelocidadDerecha;
    public Animator AnimatorDerecha;
    public bool Muriendo;

    [Header("ManoIZQ")]
    public GameObject ManoIzquierda;
    public Animator AnimatorIzquierda;
    public bool EstaEmpujando;
    public Transform ReferenciaIzquierda;
    public bool EmpujandoConFuerza;
    public Vector3 HitboxIzq;



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
        Idle,
        Morir,
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
        if (Mori)
        {
            GetComponent<LifeManager>().Inmortal = false;
            //ColliderIzq.enabled = true;
            //ColliderDer.enabled = true;
        }
        else
        {
            GetComponent<LifeManager>().Inmortal = true;
            //ColliderIzq.enabled = false;
            //ColliderDer.enabled = false;
        }



        switch (estado)
        {
            case Estados.Morir:

                PrepararLaMorision();
                return;
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

            case Estados.Idle:
                Idle();
                return;
        }


    }
    private void PrepararLaMorision()
    {
        Vector3 PosicionRayo = new Vector3(AmbasManos.transform.position.x, Jugador.transform.position.y, AmbasManos.transform.position.z);
        DispararArriba = new Ray(PosicionRayo, transform.up);
        Vector3 pos = DispararArriba.GetPoint(7f);

        if (!IsInvoking("Morir") && !AnimatorIzquierda.GetBool("Morir"))
        {
            ResetAnimaciones();

            Invoke("Morir", 1f);

        }
        else if (Muriendo && Vector3.Distance(AmbasManos.transform.position, pos) > 1)
        {
            AmbasManos.transform.position = Vector3.MoveTowards(AmbasManos.transform.position, pos, 30 * Time.deltaTime);
        }
    }
    private void Idle()
    {
        ResetAnimaciones();
    }
    public void ResetAnimaciones()
    {
        Termine = false;
        AnimatorIzquierda.SetBool("Empujar", false);
        AnimatorIzquierda.SetBool("TerminoReset", true);
        AnimatorIzquierda.SetBool("Tornado", false);
        AnimatorIzquierda.SetBool("Morir", false);
        EstaAplastando = false;
        EstaEmpujando = false;
        ApareciendoTornados = false;
        EmpujandoConFuerza = false;
        AnimatorDerecha.SetBool("Aplastar", false);
        AnimatorDerecha.SetBool("Tornado", false);
        AnimatorDerecha.SetBool("Levantate", false);
        AnimatorDerecha.SetBool("Morir", false);


    }
    private void Morir()
    {
        AnimatorIzquierda.SetBool("Morir", true);
        AnimatorDerecha.SetBool("Morir", true);
        Mori = true;

    }
    public void GolpearElPiso()
    {
        Collider[] c = Physics.OverlapBox(ReferenciaDerecha.transform.position, AplastarSize * 2, ManoDerecha.transform.rotation);
        foreach (Collider b in c)
        {
            if (b.gameObject.tag == "Personaje")
            {
                Jugador.GetComponent<EstadisticasDePersonaje>().RecibirDaño(9);

                StartCoroutine(Empujar(Jugador.transform.forward));

                return;
            }
        }
    }
    IEnumerator Empujar(Vector3 direccion)
    {
        for (int i = 1; i < 11; i++)
        {
            Jugador.GetComponent<CharacterController>().Move(direccion * -1 * Time.deltaTime * SlamKnockback * 25 / i);
            yield return null;
        }
        yield return null;

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
                print(golpe.transform.tag);
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

        if (EmpujandoConFuerza)
        {
            Collider[] c = Physics.OverlapBox(ReferenciaIzquierda.transform.position, HitboxIzq * 2, Quaternion.identity);
            foreach (Collider b in c)
            {
                if (b.gameObject.tag == "Personaje")
                {
                    StartCoroutine(Empujar(transform.forward * -1));
                    EmpujandoConFuerza = false;
                    Jugador.GetComponent<EstadisticasDePersonaje>().RecibirDaño(9f);

                }
            }
        }
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


        if (Vector3.Distance(AmbasManos.transform.position, PuntoDeOrbita.transform.position) > 2)
        {
            AmbasManos.transform.position = Vector3.MoveTowards(AmbasManos.transform.position, PuntoDeOrbita.transform.position, VelocidadDerecha * Time.deltaTime);

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
        print("estas accediendo a una fase que no codeaste ggggovirrr");
    }

    void OnDrawGizmosSelected()

    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(ReferenciaDerecha.position, AplastarSize * 2);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ReferenciaIzquierda.position, HitboxIzq * 2);

    }
}

