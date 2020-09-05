using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TentaculoScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Jugador;
    private Animator animator;
    private NavMeshAgent agente;
    public Estados estado;
    public float dist;

    public enum Estados
    {
        Chasing,
        Atacking
    }
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        Jugador = GameObject.FindGameObjectWithTag("Personaje");
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, Jugador.transform.position);

        if (dist > 3 && estado == Estados.Chasing)
        {
            agente.enabled = true;

            agente.destination = Jugador.transform.position;

        }
        else if (estado == Estados.Chasing)
        {
            agente.enabled = false;
            estado = Estados.Atacking;
            animator.SetBool("Atacando", true);
            transform.LookAt(Jugador.transform.position);
        }

    }
    public void TermineAtacar()
    {
        animator.SetBool("Atacando", false);
        estado = Estados.Chasing;
    }
}
