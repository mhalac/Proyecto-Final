using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeAgua : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Jugador;
    public Transform[] Charcos;
    public EstadosAgua estado;
    private Animator animator;
    public float IntervaloEntreTP;
    public float IntervaloLocal;
    public Transform LastPosition = null;

    public enum EstadosAgua
    {
        Tepeandose,
        Idle
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        IntervaloLocal = IntervaloEntreTP;
        Jugador = GameObject.FindGameObjectWithTag("Personaje");
    }

    // Update is called once per frame
    void Update()
    {
        if (estado == EstadosAgua.Idle && IntervaloLocal > Mathf.Epsilon)
        {
            IntervaloLocal -= Time.deltaTime;
        }
        else if (estado == EstadosAgua.Idle)
        {
            animator.SetBool("Salir", true);
            estado = EstadosAgua.Tepeandose;

        }
    }
    public void TermineEntrar()
    {
        //cuando baja
        animator.SetBool("Salir", false);
        GoToRandomPos();
    }
    public void GoToRandomPos()
    {
        float DistanciaMasCercana = 9999;
        Transform Location = Charcos[0];
        foreach (Transform c in Charcos)
        {
            //print("La distancia entre: " + c.transform.name + " y el jugador es de: " + Vector3.Distance(c.transform.position, Jugador.transform.position));
            if (DistanciaMasCercana > Vector3.Distance(c.transform.position, Jugador.transform.position) && c != LastPosition)
            {
                DistanciaMasCercana = Vector3.Distance(c.transform.position, Jugador.transform.position);
                Location = c;
            }

        }
        transform.LookAt(Jugador.transform.position);

        LastPosition = Location;
        transform.position = Location.position;
    }
    public void TermineSalir()
    {

        //cuando sube
        estado = EstadosAgua.Idle;
        IntervaloLocal = IntervaloEntreTP;
    }
}
