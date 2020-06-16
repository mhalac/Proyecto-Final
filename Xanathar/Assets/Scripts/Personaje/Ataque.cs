using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{

	public Animator anim;
    private float CDInicial;
    private float CDTotal;
    public float AreaAtaque;
    public Transform Arma;
    private int EMask;

    // Use this for initialization
    void Start()
    {
        CDInicial = GetComponent<EstadisticasDePersonaje>().VelocidadDeAtaque;
        CDTotal = CDInicial;
        EMask = LayerMask.NameToLayer("Personaje");
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Atacar();
    }
    void Atacar()
    {
       
		CDTotal -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && CDTotal < Mathf.Epsilon)
        {
            anim.SetBool("atacando",true);
			print("ataque XD");
            Collider[] ataque = Physics.OverlapSphere(Arma.position, AreaAtaque);
            foreach (Collider a in ataque)
            {
                if (a.tag == "Enemigo")
                {
                    if (a.GetComponent<Fuego1>() != null)
                        a.GetComponent<Fuego1>().RecibirDamage(GetComponent<EstadisticasDePersonaje>().DañoDePersonajeNormal);

                }
            }

            CDTotal = CDInicial;
        }
		else
			anim.SetBool("atacando",false);	
    }
    void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Arma.position, AreaAtaque);
    }

}
