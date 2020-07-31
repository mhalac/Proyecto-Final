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
        // sacar despues, solo sirve para la escena de primera entrega
        if(Input.GetKeyDown(KeyCode.C))
        {
            transform.position = new Vector3(-49.35f,15.938f,-14.85f);
        }

        Atacar();
    }
    void Atacar()
    {

        AnimSpeed = 1 / CoolDownInicial;
        //TODO Entre menos cooldown tenes mas rapido atacas y viceversa, xq la animacion tiene que durar menos
        //TODO para dar lugar al proximo ataque  
        CDTotal -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && CDTotal < Mathf.Epsilon)
        {
            anim.SetBool("atacando", true);
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
                        Enemigo.RecibirDamage();

                    }
                    else if (a.GetComponentInChildren<LifeManager>() != null)
                    {

                        LifeManager Enemigo = GetComponentInChildren<LifeManager>();
                        Enemigo.RecibirDamage();
                    }
                    else if (a.GetComponentInParent<LifeManager>() != null)
                    {
                        LifeManager Enemigo = GetComponentInParent<LifeManager>();
                        Enemigo.RecibirDamage();
                    }
                    else
                        Debug.LogWarning("NO HAY VIDA EN EL ENEMIGO");

                }
                else if(a.tag == "JefeFuego")
                {
                    JefeFuego.RecibirDamage();
                }
            }

            CDTotal = CoolDownInicial;
        }
        else
            anim.SetBool("atacando", false);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(Arma.position, AreaAtaque);
    }

}
