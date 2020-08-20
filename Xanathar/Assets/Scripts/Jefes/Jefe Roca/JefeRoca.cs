using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeRoca : MonoBehaviour
{
    public BarraDeVidaJefe barra;

    public GameObject ShieldHitBox;
    public GameObject Shield;
    private VibracionCamara camara;
    public float TiempoEntreRondas;
    public bool SpawnieTodos;
    public Transform[] PuntosSpawnEnemigos;
    public Rounds[] rondas;
    public float FuerzaKnockback;
    public Animator anim;
    private bool Empezar = false;
    private GameObject Personaje;
    public List<GameObject> EnemigosInstanciados = new List<GameObject>();
    public int IndiceRonda;
    // Use this for initialization

    void Start()
    {
        //transform.position = new Vector3(transform.position.x,transform.position.y - 0.7f,transform.position.z);
        barra = FindObjectOfType<BarraDeVidaJefe>();
        barra.ValorDeVidaMaxima = GetComponent<LifeManager>().Vida;
        Personaje = GameObject.FindGameObjectWithTag("Personaje");
        Invoke("Iniciar", 3f);
        anim = GetComponent<Animator>();
        camara = FindObjectOfType<VibracionCamara>();
        LifeManager c = GetComponent<LifeManager>();
        c.Inmortal = true;
    }

    public void Deshabilitar()
    {
        Shield.SetActive(false);
        ShieldHitBox.SetActive(false);
    }
    public void TerminoDeGolpearPiso()
    {
        anim.SetBool("Atacando", false);
        LifeManager c = GetComponent<LifeManager>();
        c.Inmortal = true;
        ShieldHitBox.SetActive(true);
        Shield.SetActive(true);
        camara.StartCoroutine(camara.Shake(.25f, .4f));

        if (IndiceRonda < rondas.Length - 1)
            IndiceRonda++;
        else
            IndiceRonda = 0;

        SpawnieTodos = false;
    }
    void PasarDeRonda()
    {
        anim.SetBool("Atacando", true);

    }
    void Iniciar()
    {
        Empezar = true;
        anim.SetBool("PuedeSalir", true);
    }

    void Update()
    {
        barra.ValorDeVidaActual = GetComponent<LifeManager>().Vida;

        if (Empezar)
        {
            if (!SpawnieTodos)
            {
                for (int i = 0; i < rondas[IndiceRonda].cantidad; i++)
                {
                    int Pos = Random.Range(0, PuntosSpawnEnemigos.Length);
                    GameObject c = Instantiate(rondas[IndiceRonda].prefab, PuntosSpawnEnemigos[Pos].position, Quaternion.identity);
                    EnemigosInstanciados.Add(c);
                    c.GetComponent<LifeManager>().NoPuedoDropear = true;
                    c.GetComponent<LifeManager>().Vida /= 2;

                }
                SpawnieTodos = true;
            }
            else if (EnemigosInstanciados.Count > 0)
            {
                for (int i = 0; i < EnemigosInstanciados.Count; i++)
                {
                    if (EnemigosInstanciados[i] == null)
                        EnemigosInstanciados.Remove(EnemigosInstanciados[i]);
                }
            }
            else if (!IsInvoking("PasarDeRonda") && !anim.GetBool("Atacando"))
            {
                Shield.SetActive(false);
                ShieldHitBox.SetActive(false);
                LifeManager c = GetComponent<LifeManager>();
                c.Inmortal = false;
                Invoke("PasarDeRonda", TiempoEntreRondas);

            }
        }
    }
    [System.Serializable]
    public class Rounds
    {
        public GameObject prefab;
        public int cantidad;

    }
}
