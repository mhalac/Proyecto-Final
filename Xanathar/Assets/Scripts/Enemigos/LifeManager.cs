using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LifeManager : MonoBehaviour
{
    public bool NoPuedoDropear;
    public bool TESTMATAR;
    public float Vida;
    public string Elemento;
    private bool YaDropie = false;
    public bool Inmortal;
    ControlarPuertasYJefes controlarPuertasYJefes;
    public PropiedadesItem[] items;
    void Start()
    {
        for (int i = 1; i < items.Length; i++)
        {
            items[i].SpawnRate += items[i - 1].SpawnRate;
        }

        if (gameObject.name == "Jefe fuego")
        {
            controlarPuertasYJefes = FindObjectOfType<ControlarPuertasYJefes>();
            controlarPuertasYJefes.OcultarTodasLasPuertas();

            if (ControlarPuertasYJefes.JefeDeFuegoMuerto == true)
            {
                Destroy(gameObject);
                controlarPuertasYJefes.ReaparecerPuertas();
            }
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
        if (!Inmortal)
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
                case "Test":
                    print(Vida);
                    break;

            }
            //        print("Yo: " + gameObject.name + " Y mi vida es de: " + Vida);
            Mori();
        }
        

    }
    private void Mori()
    {
        if (Vida < Mathf.Epsilon)
        {
            int numero = Random.Range(0, 100);
            if ((gameObject.name != "Jefe fuego" && gameObject.name != "Jefe Tierra") && !NoPuedoDropear)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].SpawnRate > numero)
                    {
                        print("Spawneo al item: " + items[i].Item + " Con un random de: + " + numero + " < " + items[i].SpawnRate);
                        if (items[i].Item != null && !YaDropie)
                        {
                            Vector3 Pos = new Vector3(transform.position.x, transform.position.y + 2.4f, transform.position.z);
                            Instantiate(items[i].Item, Pos, Quaternion.identity);
                            YaDropie = true;
                            Destroy(gameObject);
                        }

                    }

                }
            }

            //funcion para JEFES
            if (gameObject.name == "Jefe fuego")
            {
                Animator c = GetComponentInChildren<Animator>();
                c.SetBool("Morir", true);

                ControlarPuertasYJefes.JefeDeFuegoMuerto = true;
                ControlarPuertasYJefes.JefeEliminado = true;

                controlarPuertasYJefes.ReaparecerPuertas();


                this.enabled = false;
            }
            else if(gameObject.name == "Jefe Tierra")
            {
                JefeRoca c = FindObjectOfType<JefeRoca>();
                c.anim.SetBool("Morir",true);
                c.Deshabilitar();
                c.enabled = false;
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
