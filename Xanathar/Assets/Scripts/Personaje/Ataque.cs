using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{

    // C * ((100 - CDR) / 100)

    public GameObject AtaqueParticula;
    public LifeManager JefeFuego;
    public Animator anim;
    public float CoolDownInicial;
    public float CoolDownActiva;
    public float CDTotal;
    public float AreaAtaque;
    public Transform Arma;
    [Header("Variables de items")]
    [Header("Sol de la patria")]
    public bool ActivaPatria;
    public float DamagePatria;
    public GameObject SunBeam;
    [Header("ExplosiveMusic")]
    public bool ActivaMusica;

    public GameObject ParticulaExplosion;
    public float DamageMusica;
    public float MusicaDuracionInicial;
    public float MusicaDuracionActual;

    // Use this for initialization
    void Start()
    {
        CoolDownInicial = GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;
        CDTotal = 0;
        anim.speed = 1 / GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;
        MusicaDuracionActual = MusicaDuracionInicial;

        CoolDownActiva = GetComponent<EstadisticasDePersonaje>().TiempoCooldownActivas[0];

    }

    // Update is called once per frame
    void Update()
    {
        MovimientoPersonaje c = FindObjectOfType<MovimientoPersonaje>();
        if (ActivaMusica && MusicaDuracionActual > Mathf.Epsilon)
        {
            MusicaDuracionActual -= Time.deltaTime;
        }
        else if (ActivaMusica)
        {
            MusicaDuracionActual = MusicaDuracionInicial;
            ActivaMusica = false;
            GestorItems d = FindObjectOfType<GestorItems>();
            d.ItemsEquipados[0].Activado = false;
            AnimacionIconos f = FindObjectOfType<AnimacionIconos>();
            f.ActivaDeFuegoCooldown = true;
            f.SeleccionadorDeImagenes(CoolDownActiva);

            d.ExplosiveMusic.SetActive(false);


        }

        if (c.Corriendo)
            anim.SetBool("Caminando", true);
        else
            anim.SetBool("Caminando", false);

        Atacar();
    }
    public void FullReset()
    {
        GestorItems c = FindObjectOfType<GestorItems>();

        for (int i = 0; i < c.ItemsEquipados.Length; i++)
        {
            c.ItemsEquipados[i].Activado = false;
            c.ItemsEquipados[i].item = null;
        }
        Reset(0);
    }
    public void Reset(int i)
    {
        GestorItems c = FindObjectOfType<GestorItems>();

        c.ExplosiveMusic.SetActive(false);
        c.SolPatriaParticula.SetActive(false);

        ActivaPatria = false;
        ActivaMusica = false;

        c.ItemsEquipados[i].Activado = false;



    }
    public void HacerDamage()
    {
        Collider[] ataque = Physics.OverlapSphere(Arma.position, AreaAtaque);
        bool Golpeo = false;
        foreach (Collider a in ataque)
        {

            if (a.tag == "Enemigo" && !Golpeo)
            {
                Golpeo = true;
                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                if (a.GetComponent<LifeManager>() != null)
                {

                    GestorItems c = FindObjectOfType<GestorItems>();

                    LifeManager Enemigo = a.GetComponent<LifeManager>();
                    if (ActivaPatria)
                    {
                        RayoSolar(a.gameObject);

                        c.ItemsEquipados[0].Activado = false;
                        c.SolPatriaParticula.SetActive(false);
                        AnimacionIconos d = FindObjectOfType<AnimacionIconos>();
                        d.ActivaDeFuegoCooldown = true;
                        d.SeleccionadorDeImagenes(CoolDownActiva);
                        ActivaPatria = false;
                    }
                    else if (ActivaMusica)
                    {
                        Enemigo.RecibirDamage(DamageMusica);
                        Instantiate(ParticulaExplosion, a.ClosestPoint(transform.position), Quaternion.identity);
                    }
                    Enemigo.RecibirDamage();

                }


            }
            else if (a.tag == "JefeFuego")
            {
                Golpeo = true;

                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                JefeFuego = FindObjectOfType<FuegoJefe>().GetComponent<LifeManager>();
                JefeFuego.RecibirDamage();
            }
            else if (a.tag == "JefeRoca")
            {
                Golpeo = true;

                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                LifeManager c = FindObjectOfType<JefeRoca>().GetComponent<LifeManager>();
                c.RecibirDamage();
            }
            else if (a.tag == "JefeViento")
            {
                Golpeo = true;

                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                LifeManager c = FindObjectOfType<JefeViento>().GetComponent<LifeManager>();
                c.RecibirDamage();
            }
            else if (a.tag == "JefeAgua")
            {
                Golpeo = true;

                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                LifeManager c = a.GetComponent<LifeManager>();
                c.RecibirDamage();
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
        if (CDTotal > Mathf.Epsilon && !anim.GetBool("atacando"))
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

