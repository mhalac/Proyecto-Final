using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeAgua : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Activas;

    public int TentaculosRate;

    private BarraDeVidaJefe barra;

    public int TentaculosMaximos;
    public GameObject Tentaculo;

    public AudioSource TeleportSound;
    public AudioSource ILikeYourCutG;

    public float CDSlap;
    public float Distancia;
    public bool YaAtaque;
    public bool YaGolpie;
    public bool EstaDemasiadoCerca;
    public Transform Mano;
    public float RadioMano;
    public Transform parent;
    public Transform Ojos;
    public GameObject Jugador;
    public List<GameObject> Tentaculos = new List<GameObject>();
    public Transform[] Charcos;
    public EstadosAgua estado;
    public Animator animator;
    public float IntervaloEntreTP;
    public float IntervaloLocal;
    public Transform LastPosition = null;

    public enum EstadosAgua
    {
        Tepeandose,
        Idle,
        Atacando
    }
    void Start()
    {
        barra = FindObjectOfType<BarraDeVidaJefe>();
        barra.ValorDeVidaMaxima = GetComponent<LifeManager>().Vida;
        animator = GetComponent<Animator>();
        IntervaloLocal = IntervaloEntreTP;
        Jugador = GameObject.FindGameObjectWithTag("Personaje");
        //parent = GetComponentInParent<Transform>();
    }
    private bool EstaOcupado(Transform c)
    {
        if (Vector3.Distance(Jugador.transform.position, c.position) > 12)
            return false;
        return true;

    }

    public void TermineElSlap()
    {
        animator.SetBool("Slap", false);
        estado = EstadosAgua.Idle;
    }
    // Update is called once per frame
    void ResetSlap()
    {
        YaAtaque = false;
        YaGolpie = false;

    }
    IEnumerator CheckearCercania()
    {
        yield return new WaitForSeconds(2f);
        EstaDemasiadoCerca = false;
        if (Vector3.Distance(Jugador.transform.position, transform.position) < 8f)
        {
            IntervaloLocal = 0;
        }
    }
    void SpawnearTentaculos()
    {
        int c = Random.Range(0, Charcos.Length);
        foreach (GameObject d in Tentaculos)
        {
            if (d == null)
                Tentaculos.Remove(d);
        }
        GameObject b = Instantiate(Tentaculo, Charcos[c].position, Tentaculo.transform.rotation);
        Tentaculos.Add(b);
    }
    public void Deshabilitar()
    {
        int i = -4;
        foreach (GameObject b in Activas)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z + i);
            Instantiate(b, pos, Quaternion.identity);
            i = 3;
        }

    }

    void Update()
    {


        barra.ValorDeVidaActual = GetComponent<LifeManager>().Vida;

        Distancia = Vector3.Distance(Jugador.transform.position, transform.position);
        if (Tentaculos.Count < TentaculosMaximos && !IsInvoking("SpawnearTentaculos"))
        {
            Invoke("SpawnearTentaculos", TentaculosRate);
        }
        foreach (GameObject c in Tentaculos)
        {
            if (c == null)
                Tentaculos.Remove(c);
        }

        if (!EstaDemasiadoCerca && Distancia < 8f)
        {
            StartCoroutine(CheckearCercania());
            EstaDemasiadoCerca = true;
        }
        if (!YaGolpie && estado == EstadosAgua.Atacando)
        {
            print("corri");
            Collider[] c = Physics.OverlapSphere(Mano.transform.position, RadioMano);
            foreach (Collider b in c)
            {
                if (b.tag == "Personaje")
                {
                    ILikeYourCutG.Play();
                    YaGolpie = true;
                    StartCoroutine(Empujar());
                }
            }
        }
        if (estado == EstadosAgua.Idle && IntervaloLocal > Mathf.Epsilon)
        {
            IntervaloLocal -= Time.deltaTime;
            Vector3 direction = (Jugador.transform.position - Ojos.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, lookRotation, Time.deltaTime * 2);

        }
        else if (estado == EstadosAgua.Idle)
        {
            animator.SetBool("Salir", true);
            estado = EstadosAgua.Tepeandose;
        }
        if (estado == EstadosAgua.Idle && Vector3.Distance(Jugador.transform.position, transform.position) < 12 && !YaAtaque)
        {
            estado = EstadosAgua.Atacando;
            animator.SetBool("Slap", true);
            YaAtaque = true;
            if (CDSlap < IntervaloLocal && !IsInvoking("ResetSlap"))
                Invoke("ResetSlap", CDSlap);

        }

    }
    IEnumerator Empujar()
    {
        Jugador.GetComponent<EstadisticasDePersonaje>().RecibirDaño(9f);
        Vector3 dir = Mano.transform.forward;
        for (int i = 0; i < 54; i++)
        {
            Jugador.GetComponent<CharacterController>().Move(dir * Time.deltaTime * 40);
            yield return null;
        }
        yield return null;
    }
    public void TermineEntrar()
    {
        //cuando baja
        TeleportSound.Play();

        animator.SetBool("Salir", false);
        YaGolpie = false;
        GoToRandomPos();
    }
    public void GoToRandomPos()
    {
        float DistanciaMasCercana = 9999;
        Transform Location = Charcos[0];
        foreach (Transform c in Charcos)
        {
            //print("La distancia entre: " + c.transform.name + " y el jugador es de: " + Vector3.Distance(c.transform.position, Jugador.transform.position));
            if (DistanciaMasCercana > Vector3.Distance(c.transform.position, Jugador.transform.position) && c != LastPosition && !EstaOcupado(c))
            {
                DistanciaMasCercana = Vector3.Distance(c.transform.position, Jugador.transform.position);
                Location = c;
            }

        }
        YaAtaque = false;

        LastPosition = Location;
        parent.transform.position = Location.position;
    }
    public void TermineSalir()
    {

        //cuando sube
        estado = EstadosAgua.Idle;
        IntervaloLocal = IntervaloEntreTP;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Mano.transform.position, RadioMano);
    }
}
