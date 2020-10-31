using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LifeManager : MonoBehaviour
{
    public List<string> NombresJefes = new List<string>();
    public bool NoPuedoDropear;
    public bool TESTMATAR;
    public float Vida;
    public string Elemento;
    private bool YaDropie = false;
    public bool Inmortal;
    ControlarPuertasYJefes ControladorDePuertas;
    public PropiedadesItem[] items;
    void Start()
    {
        NombresJefes.Add("JefeFuego");
        NombresJefes.Add("JefeAgua");
        NombresJefes.Add("JefeRoca");
        NombresJefes.Add("JefeViento");
        for (int i = 1; i < items.Length; i++)
        {
            items[i].SpawnRate += items[i - 1].SpawnRate;
        }

        if (gameObject.name == "Jefe fuego")
        {
            ControladorDePuertas = FindObjectOfType<ControlarPuertasYJefes>();
            ControladorDePuertas.OcultarTodasLasPuertas();
            Invoke("AumentarVida" , 1f);

            if (ControlarPuertasYJefes.JefeDeFuegoMuerto == true)
            {
                Destroy(gameObject);
                ControladorDePuertas.ReaparecerPuertas();
            }
        }
        else if (gameObject.name == "Jefe Tierra")
        {
            ControladorDePuertas = FindObjectOfType<ControlarPuertasYJefes>();
            ControladorDePuertas.OcultarTodasLasPuertas();
            Invoke("AumentarVida" , 1f);

            if (ControlarPuertasYJefes.JefeDeTierraMuerto == true)
            {
                Destroy(gameObject);
                ControladorDePuertas.ReaparecerPuertas();
            }
        }
        else if (gameObject.name == "JefeViento")
        {
            ControladorDePuertas = FindObjectOfType<ControlarPuertasYJefes>();
            ControladorDePuertas.OcultarTodasLasPuertas();
            Invoke("AumentarVida" , 1f);

            if (ControlarPuertasYJefes.JefeDeVientoMuero == true)
            {
                Destroy(gameObject);
                ControladorDePuertas.ReaparecerPuertas();
            }
        }
        else if (gameObject.name == "JefeAgua")
        {
            ControladorDePuertas = FindObjectOfType<ControlarPuertasYJefes>();
            ControladorDePuertas.OcultarTodasLasPuertas();
            Invoke("AumentarVida" , 1f);

            if (ControlarPuertasYJefes.JefeDeAguaMuerto == true)
            {
                Destroy(gameObject);
                ControladorDePuertas.ReaparecerPuertas();
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
            if (!NombresJefes.Contains(gameObject.tag) && !NoPuedoDropear)
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
                EstadisticasDePersonaje estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
                estadisticas.JefesEliminados += 1;
                Animator c = GetComponentInChildren<Animator>();

                ControlarPuertasYJefes.JefeDeFuegoMuerto = true;
                ControlarPuertasYJefes.JefeEliminado = true;

                ControladorDePuertas.ReaparecerPuertas();
                FindObjectOfType<BarraDeVidaJefe>().ValorDeVidaActual = 0;
                FuegoJefe d = FindObjectOfType<FuegoJefe>();
                d.enabled = false;
                c.SetBool("Morir", true);

                enabled = false;
            }
            else if (gameObject.name == "Jefe Tierra")
            {
                EstadisticasDePersonaje estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
                estadisticas.JefesEliminados += 1;

                JefeRoca c = FindObjectOfType<JefeRoca>();


                ControlarPuertasYJefes.JefeDeTierraMuerto = true;
                ControlarPuertasYJefes.JefeEliminado = true;

                ControladorDePuertas.ReaparecerPuertas();
                FindObjectOfType<BarraDeVidaJefe>().ValorDeVidaActual = 0;


                c.anim.SetBool("Morir", true);
                c.Deshabilitar();
                c.enabled = false;
            }
            else if (gameObject.name == "JefeViento")
            {
                EstadisticasDePersonaje estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
                estadisticas.JefesEliminados += 1;

                JefeViento c = FindObjectOfType<JefeViento>();
                GameObject[] tornados = GameObject.FindGameObjectsWithTag("Enemigo");
                foreach(GameObject b in tornados)
                {
                    Destroy(b.gameObject);
                }

                ControlarPuertasYJefes.JefeDeVientoMuero = true;
                ControlarPuertasYJefes.JefeEliminado = true;
                FindObjectOfType<BarraDeVidaJefe>().ValorDeVidaActual = 0;

                c.Deshabilitar();
                c.enabled = false;

                ControladorDePuertas.ReaparecerPuertas();
            }
            else if (gameObject.tag == "JefeAgua")
            {
                EstadisticasDePersonaje estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
                estadisticas.JefesEliminados += 1;

                JefeAgua c = FindObjectOfType<JefeAgua>();

                ControlarPuertasYJefes.JefeDeAguaMuerto = true;
                ControlarPuertasYJefes.JefeEliminado = true;
                c.Deshabilitar();
                FindObjectOfType<BarraDeVidaJefe>().ValorDeVidaActual = 0;

                Destroy(c.gameObject, 9);
                c.enabled = false;
                c.animator.SetBool("Morir", true);

                ControladorDePuertas.ReaparecerPuertas();
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

    public void AumentarVida()
    {
        EstadisticasDePersonaje estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
        int AumentoDeVida = 0;
        int Cant = estadisticas.JefesEliminados;
        Debug.Log(Cant);
        switch(Cant)
        {
            case 0:
            Debug.Log("No hay aumento de vida");
            break;

            case 1:
            AumentoDeVida = 20;
            break;

            case 2:
            AumentoDeVida = 40;
            break;

            case 3:
            AumentoDeVida = 60;
            break;
        }

        GameObject JefeDeFuego = GameObject.Find("Jefe fuego");
        GameObject JefeDeViento = GameObject.Find("JefeViento");
        GameObject JefeDeAgua = GameObject.Find("JefeAgua");
        GameObject JefeDeTierra = GameObject.Find("Jefe Tierra");

        if(JefeDeFuego != null)
        {
            JefeDeFuego.GetComponent<LifeManager>().Vida += AumentoDeVida;
        }
        else if(JefeDeViento != null)
        {
            JefeDeViento.GetComponent<LifeManager>().Vida += AumentoDeVida;
        }
        else if(JefeDeAgua != null)
        {
            JefeDeAgua.GetComponent<LifeManager>().Vida += AumentoDeVida;
        }
        else if(JefeDeTierra != null)
        {
            JefeDeTierra.GetComponent<LifeManager>().Vida += AumentoDeVida;
        }
    }

}
