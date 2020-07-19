using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuegoJefe : MonoBehaviour
{
    public GameObject Arma;
    private GameObject Personaje;

    public string Estado;

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
        Debug.DrawLine(Arma.transform.position, Personaje.transform.position, Color.magenta);
        Debug.DrawLine(transform.position, Arma.transform.position);
        if (FinalizoAnim)
        {
            if (Estado == States.Idle.ToString() && Vector3.Distance(Personaje.transform.position, transform.position) > Vector3.Distance(transform.position, Arma.transform.position))
            {
                Agente.enabled = true;
                AcaboDeAtacar = false;
                anim.SetBool("Correr", true);
                Agente.destination = Personaje.transform.position;
            }
            else if (Estado == States.Idle.ToString() && !AcaboDeAtacar && Vector3.Distance(Personaje.transform.position, transform.position) < Vector3.Distance(transform.position, Arma.transform.position))
            {
                Atacar();
            }
            else if (AcaboDeAtacar)
            {
                FinalizoAnim = false;
                anim.Play("right swing", -1, 0f);
                Estado = States.Atacando.ToString();
            }

        }//print(Vector3.Distance(Personaje.transform.position, transform.position));
    }
    public void TermineAnimacion()
    {
        //xddd
        StartCoroutine(EsperarDespuesDeAtacar());

    }
    IEnumerator RotarMientrasAtaco()
    {
        bool Rotando = true;
        anim.SetBool("Correr", false);
        FinalizoAnim = false;
        Vector3 angulos = new Vector3(0, -360, 0);
        Vector3 PosicionRelativa = Personaje.transform.position - transform.position;
     

        

        while (Rotando)
        {
            transform.Rotate(angulos * Time.deltaTime);
            if (Vector3.Distance(Arma.transform.position, Personaje.transform.position) < 2f)
            {
                Rotando = false;
            }

            yield return null;

        }
        anim.SetBool("Atacar", true);
        //anim.SetBool("TerminoCorrer", true);
        print("Ataque");
    }
    private void Atacar()
    {
        Agente.enabled = false;
        Estado = States.Atacando.ToString();

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

}
