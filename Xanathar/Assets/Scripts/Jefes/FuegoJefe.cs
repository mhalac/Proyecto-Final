using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuegoJefe : MonoBehaviour
{
    public float Radio;
    public GameObject PosicionAtaque;
    private GameObject Personaje;

    public GameObject Arma;
    public string Estado;
    private bool Golpie = false;
    private Animator anim;
    private NavMeshAgent Agente;

    // Use this for initialization
    public bool AcaboDeAtacar;
    private bool FinalizoAnim = true;


    enum States
    {
        Idle,
        Atacando,
        Persiguiendo
    }

    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Personaje");
        Agente = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(EsperarInicio());
    }

    // Update is called once per frame
    void Update()
    {
        //!!Acordate que a veces hace 2 de damage.

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("right swing") && !Golpie)
        {
            Collider[] Obj = Physics.OverlapSphere(Arma.transform.position, Radio);
            //Personaje.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            foreach (Collider c in Obj)
            {
                if (c.gameObject.tag == "Personaje")
                {
                    Vector3 dir = c.transform.position - transform.position;
                    dir.y = 0;
                    Golpie = true;
                    print("le di");
                    StartCoroutine(Knockback(dir));

                }

            }
        }
        Debug.DrawLine(PosicionAtaque.transform.position, Personaje.transform.position, Color.magenta);
        Debug.DrawLine(transform.position, PosicionAtaque.transform.position);
        if (FinalizoAnim)
        {
            if (Estado == States.Idle.ToString() && Vector3.Distance(Personaje.transform.position, transform.position) >= Vector3.Distance(transform.position, PosicionAtaque.transform.position))
            {
                Agente.enabled = true;
                AcaboDeAtacar = false;
                anim.SetBool("Correr", true);
                Agente.destination = Personaje.transform.position;
            }
            else if (Estado == States.Idle.ToString() && !AcaboDeAtacar && Vector3.Distance(Personaje.transform.position, transform.position) <= Vector3.Distance(transform.position, PosicionAtaque.transform.position))
            {
                Atacar();
            }
            else if (AcaboDeAtacar)
            {
                Golpie = false;
                FinalizoAnim = false;
                anim.Play("right swing", -1, 0f);
                Estado = States.Atacando.ToString();
            }

        }//print(Vector3.Distance(Personaje.transform.position, transform.position));
    }
    public void TermineAnimacion()
    {
        //Personaje.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(EsperarDespuesDeAtacar());

    }
    IEnumerator Knockback(Vector3 dir)
    {
        for (int i = 0; i < 30; i++)
        {
            Personaje.transform.Translate(dir.normalized * Time.deltaTime * 50);
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
        Vector3 PosicionRelativa = Personaje.transform.position - transform.position;
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
    IEnumerator EsperarInicio()
    {
        yield return new WaitForSeconds(2);
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Arma.transform.position, Radio);
    }
}
