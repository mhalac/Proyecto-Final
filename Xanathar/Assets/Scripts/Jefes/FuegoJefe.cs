using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuegoJefe : MonoBehaviour
{

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

        if (FinalizoAnim)
        {
            if (Estado == States.Idle.ToString() && Vector3.Distance(Personaje.transform.position, transform.position) > 20)
            {
                AcaboDeAtacar = false;
                anim.SetBool("Correr", true);
                Agente.destination = Personaje.transform.position;
            }
            else if (Estado == States.Idle.ToString() && !AcaboDeAtacar)
            {
                print("this");
                Atacar();
            }
            else if (AcaboDeAtacar)
            {
                FinalizoAnim = false;
                print("Ataco por segunda vez");
                anim.Play("right swing", -1, 0f);
                Estado = States.Atacando.ToString();
            }

        }//print(Vector3.Distance(Personaje.transform.position, transform.position));
    }
    public void TermineAnimacion()
    {

        StartCoroutine(EsperarDespuesDeAtacar());

    }
    private void Atacar()
    {
        anim.SetBool("Correr", false);
        anim.SetBool("Atacar", true);
        FinalizoAnim = false;

        //anim.SetBool("TerminoCorrer", true);
        Agente.isStopped = true;
        print("Ataque");
        Estado = States.Atacando.ToString();
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
        Agente.isStopped = false;
        FinalizoAnim = true;
        AcaboDeAtacar = true;

    }

}
