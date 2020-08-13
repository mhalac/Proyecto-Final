using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{



    public LifeManager JefeFuego;
    public Animator anim;
    public float CoolDownInicial;
    public float CDTotal;
    public float AreaAtaque;
    public Transform Arma;
    private float AnimSpeed;
    private Vector3 PosAtaque;
    [Header("Variables de items")]
    [Header("Sol de la patria")]
    public bool ActivaPatria;
    public float DamagePatria;
    public GameObject SunBeam;
    [Header("ExplosiveMusic")]
    public bool ActivaMusica;

    // Use this for initialization
    void Start()
    {
        CoolDownInicial = GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;
        CDTotal = 0;
        Arma.position = new Vector3(Arma.position.x, Arma.position.y, Arma.position.z + AreaAtaque);
        anim.speed = 1 / GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;

    }

    // Update is called once per frame
    void Update()
    {
        MovimientoPersonaje c = FindObjectOfType<MovimientoPersonaje>();
        //print("La anim speed es" + anim.speed);

        //anim.speed = 1;

        if (c.Corriendo)
            anim.SetBool("Caminando", true);
        else
            anim.SetBool("Caminando", false);
        Atacar();
    }
    public void Reset()
    {
        ActivaPatria = false;
        GestorItems c = FindObjectOfType<GestorItems>();
        c.SolPatriaParticula.SetActive(false);

    }
    public void HacerDamage()
    {
        Collider[] ataque = Physics.OverlapSphere(Arma.position, AreaAtaque);
        foreach (Collider a in ataque)
        {

            if (a.tag == "Enemigo")
            {
                if (a.GetComponent<LifeManager>() != null)
                {
                    GestorItems c = FindObjectOfType<GestorItems>();

                    LifeManager Enemigo = a.GetComponent<LifeManager>();
                    if (ActivaPatria)
                    {
                        RayoSolar(a.gameObject);

                        c.ItemsEquipados[0].Activado = false;
                        c.SolPatriaParticula.SetActive(false);

                        ActivaPatria = false;
                    }
                    else if (ActivaMusica)
                    {
                        c.ItemsEquipados[0].Activado = false;
                        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),a.transform.position,Quaternion.identity);
                        ActivaMusica = false;
                    }
                    Enemigo.RecibirDamage();

                }


            }
            else if (a.tag == "JefeFuego")
            {
                JefeFuego.RecibirDamage();
            }
        }

    }
    void RayoSolar(GameObject target)
    {
        Vector3 Arriba = new Vector3(target.transform.position.x - 2, transform.position.y + 4f, target.transform.position.z);
        GameObject c = Instantiate(SunBeam, Arriba, Quaternion.identity);
        c.GetComponent<Transform>().Rotate(0, 0, -90);
        target.GetComponent<LifeManager>().RecibirDamage(DamagePatria);
    }

    void Atacar()
    {

        //TODO Entre menos cooldown tenes mas rapido atacas y viceversa, xq la animacion tiene que durar menos
        //TODO para dar lugar al proximo ataque  
        if (CDTotal > -1 && !anim.GetBool("atacando"))
            CDTotal -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && CDTotal < Mathf.Epsilon && !anim.GetBool("Caminando") && !anim.GetBool("atacando"))
        {
            {
                anim.speed = 1 / GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;

                anim.SetBool("atacando", true);
                CDTotal = CoolDownInicial;

            }
        }


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(Arma.position, AreaAtaque);
    }

}

