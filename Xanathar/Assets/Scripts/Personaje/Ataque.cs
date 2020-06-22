using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{

	public Animator anim;
    private float CoolDownInicial;
    private static float CDTotal;
    public float AreaAtaque;
    public Transform Arma;
    private int EMask;
    private float AnimSpeed;
    private Vector3 PosAtaque;
    // Use this for initialization
    void Start()
    {
        CoolDownInicial = GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;
        CDTotal = 0;
        EMask = LayerMask.NameToLayer("Personaje");
		anim = GetComponent<Animator>();
        PosAtaque = new Vector3(Arma.position.x + AreaAtaque,Arma.position.y,Arma.position.z);

    }

    // Update is called once per frame
    void Update()
    {
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
            anim.SetBool("atacando",true);
            anim.speed = AnimSpeed;
            Collider[] ataque = Physics.OverlapSphere(Arma.position, AreaAtaque);
            foreach (Collider a in ataque)
            {
                if (a.tag == "Enemigo")
                {
                    if (a.GetComponent<LifeManager>() != null )
                    {
                        
                        LifeManager Enemigo = a.GetComponent<LifeManager>();
                        Enemigo.RecibirDamage();
                    
                    }
                    else if(a.GetComponentInChildren<LifeManager>() != null) 
                    {
                        
                        LifeManager Enemigo = GetComponentInChildren<LifeManager>();
                        Enemigo.RecibirDamage();
                    }
                    else if(a.GetComponentInParent<LifeManager>() != null) 
                    {
                        
                        LifeManager Enemigo = GetComponentInParent<LifeManager>();
                        Enemigo.RecibirDamage();
                    }      

                }
            }

            CDTotal = CoolDownInicial;
        }
		else
			anim.SetBool("atacando",false);	
    }
    void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
        PosAtaque = new Vector3(Arma.position.x - AreaAtaque,Arma.position.y,Arma.position.z);

        Gizmos.DrawWireSphere(PosAtaque, AreaAtaque);
    }

}
