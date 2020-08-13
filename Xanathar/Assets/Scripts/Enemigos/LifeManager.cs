using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LifeManager : MonoBehaviour
{
    public bool TESTMATAR;
    public float Vida;
    public string Elemento;


    public PropiedadesItem[] items;
    void Start()
    {
        for (int i = 1; i < items.Length; i++)
        {
            items[i].SpawnRate += items[i - 1].SpawnRate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TESTMATAR)
        {
            Vida = 0;
            Mori();
        }

    }
    private void Ventaja(float damage)
    {
        Vida = Vida - damage * 2;
    }
    private void Desventaja(float damage)
    {
        Vida = Vida - damage * 0.5f;
    }
    public void RecibirDamage(float damage)
    {
        Vida -= damage;
        Mori();
    }
    public void RecibirDamage()
    {

        EstadisticasDePersonaje Stats = GameObject.Find("Jugador").GetComponent<EstadisticasDePersonaje>();

        // Tenes que checkear las ventajas o debilidades manualmente, para eso revisas si tiene algun tipo de damage de ese
        //elemento y si es asi lo aplicas

        //Haces un switch para ir de una a tu elemento asi no laguea y despues te sacas vida en base a los damages que tenes
        // y las ventajas o desventajas se aplican distinto dependiendo tu elemento
        Vida -= Stats.DañoDePersonajeNormal;
        switch (Elemento)
        {

            case "Fuego":

                if (Stats.DañoElementalAgua > Mathf.Epsilon)
                {
                    Ventaja(Stats.DañoElementalAgua);
                }
                if (Stats.DañoElementalAire > Mathf.Epsilon)
                {
                    Desventaja(Stats.DañoElementalAire);
                }
                if (Stats.DañoElementalTierra > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalTierra;
                }

                if (Stats.DañoElementalFuego > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalFuego;
                }
                break;

            case "Viento":
                if (Stats.DañoElementalAgua > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalAgua;
                }
                if (Stats.DañoElementalAire > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalAire;
                }
                if (Stats.DañoElementalTierra > Mathf.Epsilon)
                {
                    Desventaja(Stats.DañoElementalTierra);
                }

                if (Stats.DañoElementalFuego > Mathf.Epsilon)
                {
                    Ventaja(Stats.DañoElementalFuego);
                }
                break;

            case "Agua":
                if (Stats.DañoElementalAgua > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalAgua;
                }
                if (Stats.DañoElementalAire > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalAire;
                }
                if (Stats.DañoElementalTierra > Mathf.Epsilon)
                {
                    Ventaja(Stats.DañoElementalTierra);
                }
                if (Stats.DañoElementalFuego > Mathf.Epsilon)
                {
                    Desventaja(Stats.DañoElementalFuego);
                }
                break;

            case "Tierra":
                if (Stats.DañoElementalAgua > Mathf.Epsilon)
                {
                    Desventaja(Stats.DañoElementalAgua);
                }
                if (Stats.DañoElementalAire > Mathf.Epsilon)
                {
                    Ventaja(Stats.DañoElementalAire);
                }
                if (Stats.DañoElementalTierra > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalTierra;
                }
                if (Stats.DañoElementalFuego > Mathf.Epsilon)
                {
                    Vida -= Stats.DañoElementalFuego;
                }
                break;


        }
        //        print("Yo: " + gameObject.name + " Y mi vida es de: " + Vida);
        Mori();


    }
    private void Mori()
    {
        if (Vida < Mathf.Epsilon)
        {
            int numero = Random.Range(0, 100);
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].SpawnRate > numero)
                {
                    print("Spawneo al item: " + items[i].Item + " Con un random de: + " + numero + " < " + items[i].SpawnRate);
                    TESTMATAR = false;

                }

            }
            //funcion para JEFES
            if (gameObject.name == "Jefe fuego")
            {
                Animator c = GetComponentInChildren<Animator>();
                c.SetBool("Morir", true);
                this.enabled = false;
            }
            else
            {
                Destroy(this.gameObject);
            }



        }
    }
    [System.Serializable]
    public class PropiedadesItem
    {
        public GameObject Item;
        public int SpawnRate;
    }

}
