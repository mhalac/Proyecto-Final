using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuegoJefe : MonoBehaviour
{
    public float Radio;
    public GameObject PosicionAtaque;
    private GameObject Personaje;
    private RaycastHit hit;
    public float DamageCaida;
    public float DamageTrompada;
    public float FuerzaKnockback;
    public Transform SaltoMamado;
    public BarraDeVidaJefe barra;
    public Vector3 AreaCaida;
    public GameObject Arma;
    public string Estado;
    private bool Golpie = false;
    private Animator anim;
    private NavMeshAgent Agente;

    public GameObject[] ItemsActivos;

    public bool AcaboDeAtacar;
    private bool FinalizoAnim = true;
    public float Distancia;
    public VibracionCamara Vibrar;
    public int ContadorDeGolpes;

    [Header("Sonidos")]
    public AudioSource SonidoSalto;
    public AudioSource SonidoCorrer;


    enum States
    {
        Idle,
        Atacando,
        Persiguiendo,
        Saltando,
        Cargando


    }

    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Personaje");
        Agente = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(EsperarInicio());
        Vibrar = FindObjectOfType<VibracionCamara>();
        barra = FindObjectOfType<BarraDeVidaJefe>();
        barra.ValorDeVidaMaxima = GetComponent<LifeManager>().Vida;
    }


    // Update is called once per frame
    void Update()
    {
        barra.ValorDeVidaActual = GetComponent<LifeManager>().Vida;

        Distancia = Vector3.Distance(transform.position, Personaje.transform.position);
        if (Estado == States.Atacando.ToString() && Distancia < 7.449858f)
        {
            anim.SetBool("Atacar", false);
            anim.SetBool("Saltar", true);
            Estado = States.Saltando.ToString();
        }
        if (AcaboDeAtacar && Vector3.Distance(PosicionAtaque.transform.position, Personaje.transform.position) > 2.5f && Distancia <= Vector3.Distance(transform.position, PosicionAtaque.transform.position))
        {
            AcaboDeAtacar = false;
            anim.SetBool("Atacar", false);
            anim.SetBool("Saltar", true);
            Estado = States.Saltando.ToString();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("right swing"))
        {
            Collider[] Obj = Physics.OverlapSphere(Arma.transform.position, Radio);
            foreach (Collider c in Obj)
            {
                ContadorDeGolpes++;
                if (c.gameObject.tag == "Personaje" && !Golpie)
                {
                    var dir = transform.forward * -1;
                    SonidoSalto.Play();

                    Golpie = true;
                    StartCoroutine(Knockback(dir.normalized));

                }

            }
        }

        if (FinalizoAnim)
        {
            if (Estado == States.Idle.ToString() && Distancia >= Vector3.Distance(transform.position, PosicionAtaque.transform.position))
            {
                Agente.enabled = true;
                AcaboDeAtacar = false;
                anim.SetBool("Correr", true);

                if (SonidoCorrer.isPlaying == false)
                    SonidoCorrer.Play();

                Agente.destination = Personaje.transform.position;
            }
            else if (Estado == States.Idle.ToString() && !AcaboDeAtacar && Distancia <= Vector3.Distance(transform.position, PosicionAtaque.transform.position))
            {
                Atacar();
                SonidoCorrer.Stop();
            }
            else if (AcaboDeAtacar)
            {
                SonidoCorrer.Stop();

                Golpie = false;
                FinalizoAnim = false;
                anim.Play("right swing", -1, 0f);
                Estado = States.Atacando.ToString();
            }

        }//print(Vector3.Distance(Personaje.transform.position, transform.position));
    }

    public void TermineAnimacion()
    {
        StartCoroutine(EsperarDespuesDeAtacar());
    }
    public void AplasteElPiso()
    {
        bool encontro = false;
        Collider[] Buscar = Physics.OverlapBox(transform.position, AreaCaida);
        foreach (Collider c in Buscar)
        {
            if (c.gameObject.tag == "Personaje" && !encontro)
            {
                Personaje.GetComponent<EstadisticasDePersonaje>().RecibirDaño(DamageCaida);
                encontro = true;
            }
        }

        Vibrar.StartCoroutine(Vibrar.Shake(.15f, .4f));

        if (Distancia < 7.449858f)
        {
            anim.Play("Saltando", -1, 0f);
        }
        else
        {
            anim.SetBool("Saltar", false);
            Estado = States.Idle.ToString();
            Agente.enabled = true;

        }

    }

    IEnumerator Knockback(Vector3 dir)
    {
        Personaje.GetComponent<EstadisticasDePersonaje>().RecibirDaño(DamageTrompada);
        for (int i = 0; i < 30; i++)
        {
            Personaje.GetComponent<CharacterController>().Move(dir * Time.deltaTime * FuerzaKnockback);
            yield return null;
        }
        yield return null;

    }
    IEnumerator RotarMientrasAtaco()
    {
        bool Rotando = true;
        anim.SetBool("Correr", false);
        FinalizoAnim = false;
        Vector3 angulos = new Vector3(0, -360, 0);
        int vuelta = 0;



        while (Rotando)
        {
            vuelta++;
            transform.Rotate(angulos * Time.deltaTime);
            if (Vector3.Distance(PosicionAtaque.transform.position, Personaje.transform.position) < 2f || vuelta > 67)
            {
                Rotando = false;
            }

            yield return null;

        }
        anim.SetBool("Atacar", true);
        //anim.SetBool("TerminoCorrer", true);
    }
    private void Atacar()
    {
        Agente.enabled = false;
        Estado = States.Atacando.ToString();
        Golpie = false;
        StartCoroutine(RotarMientrasAtaco());

    }
    public void TermineDeAtacar()
    {

    }
    public void DropearItems()
    {
        int i = 0;
        foreach (GameObject c in ItemsActivos)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z + i);
            Instantiate(c, pos, Quaternion.identity);
            i = 5;
        }
    }
    IEnumerator EsperarInicio()
    {
        yield return new WaitForSeconds(3);
        Estado = States.Idle.ToString();

    }
    IEnumerator EsperarDespuesDeAtacar()
    {
        yield return new WaitForSeconds(0.5f);
        anim.StopPlayback();
        Estado = States.Idle.ToString();
        anim.SetBool("Atacar", false);

        FinalizoAnim = true;
        AcaboDeAtacar = true;


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, AreaCaida * 2);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Arma.transform.position, Radio);
    }
}
