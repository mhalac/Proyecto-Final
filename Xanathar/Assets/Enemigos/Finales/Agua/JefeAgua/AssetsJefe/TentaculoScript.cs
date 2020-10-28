using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TentaculoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Movimiento;
    public AudioSource Strike;
    public GameObject PuntoGolpe;
    public GameObject Jugador;
    private Animator animator;
    private NavMeshAgent agente;
    public Estados estado;
    public float dist;
    public GameObject Heredar;
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
        estado = Estados.Chasing;
    }
    IEnumerator Empujar()
    {
        Vector3 dir = Jugador.transform.forward * -1;
        for (int i = 0; i < 30; i++)
        {
            Jugador.GetComponent<CharacterController>().Move(dir * Time.deltaTime * 40);
            yield return null;
        }
        yield return null;
    }
    public void Ataque()
    {
        Collider[] c = Physics.OverlapSphere(PuntoGolpe.transform.position, 2f);
        foreach (Collider b in c)
        {
            if (b.tag == "Personaje")
            {
                Strike.Play();

                StartCoroutine(Empujar());
                Jugador.GetComponent<EstadisticasDePersonaje>().RecibirDaño(10f);
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()

    {
        dist = Vector3.Distance(transform.position, Jugador.transform.position);

        if (dist > 3.2f && estado == Estados.Chasing)
        {
            if (!Movimiento.isPlaying) Movimiento.Play();
            //agente.enabled = true;
            agente.isStopped = false;
            agente.destination = Jugador.transform.position;

        }
        else if (estado == Estados.Chasing)
        {
            //agente.enabled = false;
            agente.isStopped = true;
            Movimiento.Stop();
            estado = Estados.Atacking;
            animator.SetBool("Atacando", true);
            Vector3 direction = (Jugador.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(Heredar.transform.rotation, lookRotation, Mathf.Infinity);

        }

    }
    public void TermineAtacar()
    {
        animator.SetBool("Atacando", false);
        estado = Estados.Chasing;
    }
}
