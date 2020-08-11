using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{



    public LifeManager JefeFuego;
    public Animator anim;
    private float CoolDownInicial;
    public static float CDTotal;
    public float AreaAtaque;
    public Transform Arma;
    private float AnimSpeed;
    private Vector3 PosAtaque;
    [Header("Variables de items")]
    public bool ActivaPatria;
    public float DamagePatria;
    public GameObject SunBeam;

    // Use this for initialization
    void Start()
    {
        CoolDownInicial = GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;
        CDTotal = 0;
        anim = GetComponent<Animator>();
        Arma.position = new Vector3(Arma.position.x, Arma.position.y, Arma.position.z + AreaAtaque);
    }

    // Update is called once per frame
    void Update()
    {
        
        Atacar();
    }
    void HacerDamage()
    {
        anim.speed = AnimSpeed;
        Collider[] ataque = Physics.OverlapSphere(Arma.position, AreaAtaque);
        foreach (Collider a in ataque)
        {

            if (a.tag == "Enemigo")
            {
                print("Hay enemigo");
                if (a.GetComponent<LifeManager>() != null)
                {
                    LifeManager Enemigo = a.GetComponent<LifeManager>();
                    if (ActivaPatria)
                    {
                        RayoSolar(a.gameObject);
                        GestorItems c = FindObjectOfType<GestorItems>();
                        c.ItemsEquipados[0].Activado = false;
                        ActivaPatria = false;
                    }
                    Enemigo.RecibirDamage();

                }


            }
            else if (a.tag == "JefeFuego")
            {
                JefeFuego.RecibirDamage();
            }
        }

        CDTotal = CoolDownInicial;
    }
    void RayoSolar(GameObject target)
    {
        Vector3 Arriba = new Vector3(target.transform.position.x - 2, transform.position.y + 4f, target.transform.position.z);
        GameObject c = Instantiate(SunBeam, Arriba, Quaternion.identity);
        c.GetComponent<Transform>().Rotate(0,0,-90);
        c.GetComponent<LifeManager>().RecibirDamage(DamagePatria);
    }

    void Atacar()
    {

        AnimSpeed = 1 / CoolDownInicial;
        //TODO Entre menos cooldown tenes mas rapido atacas y viceversa, xq la animacion tiene que durar menos
        //TODO para dar lugar al proximo ataque  
        CDTotal -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && CDTotal < Mathf.Epsilon)
        {
            {
                anim.SetBool("atacando", true);
            }
        }

        else
        {
            anim.SetBool("atacando", false);
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(Arma.position, AreaAtaque);
    }


}

