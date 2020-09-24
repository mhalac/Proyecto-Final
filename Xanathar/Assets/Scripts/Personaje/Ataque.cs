using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{

    private LifeManager stomperTarget;
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
    [Header("Stomper")]
    public bool ActivoStomper;
    public GameObject PrefabStomper;
    public float DamageStomper;


    // Use this for initialization
    void Start()
    {
        CoolDownInicial = GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;
        CDTotal = 0;
        anim.speed = 1 / GetComponent<EstadisticasDePersonaje>().CoolDownAtaque;
        MusicaDuracionActual = MusicaDuracionInicial;


    }

    // Update is called once per frame
    void Update()
    {
        CoolDownActiva = GetComponent<GestorItems>().ItemsEquipados[0].cooldownInicial;

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
    void HacerDamageStomper()
    {
        stomperTarget.RecibirDamage(DamageStomper);
    }
    public void Reset(int i)
    {
        GestorItems c = FindObjectOfType<GestorItems>();
        c.ItemsEquipados[i].Activado = false;
        //c.ItemsEquipados[i].item = null;

        ApagarActivas(i);

        Debug.Log("Esto se ejecuto");
    }
    public void ApagarActivas(int elemento)
    {
        //apagar activas dependiendo el elemento
        GestorItems c = FindObjectOfType<GestorItems>();
        //0 fuego 1 roca 2 aire 3 agua

        switch (elemento)
        {
            case 0:
                c.ExplosiveMusic.SetActive(false);
                c.SolPatriaParticula.SetActive(false);
                ActivaPatria = false;
                ActivaMusica = false;
                return;
            case 1:
                c.ExtraHeartsActivo = false;
                //c.Invoke("GolemHeartOff", 1f);
                ActivoStomper = false;
                c.StomperParticula.SetActive(false);

                return;
            case 2:
                int capa = LayerMask.NameToLayer("Personaje");
                gameObject.layer = capa;
                if (c.IsInvoking("DesaparecerClon"))
                {
                    foreach (Transform obj in c.transform)
                    {
                        obj.gameObject.layer = capa;
                    }
                    c.ItemsEquipados[2].Activado = false;
                    c.CancelInvoke("DesaparecerClon");
                    ManejadorDeItems f = FindObjectOfType<ManejadorDeItems>();
                    AnimacionIconos g = FindObjectOfType<AnimacionIconos>();
                    g.ActivaDeVientoCooldown = true;
                    g.SeleccionadorDeImagenes(c.ItemsEquipados[2].cooldownInicial);

                    Debug.Log("Se ejecuto el primer if");
                }
                else if (c.IsInvoking("RemoverInmortalidad"))
                {
                    c.ItemsEquipados[2].Activado = false;

                    EstadisticasDePersonaje b = FindObjectOfType<EstadisticasDePersonaje>();
                    b.Inmortalidad = false;
                    c.CancelInvoke("RemoverInmortalidad");
                    c.ca.enabled.value = false;
                    ManejadorDeItems f = FindObjectOfType<ManejadorDeItems>();
                    AnimacionIconos g = FindObjectOfType<AnimacionIconos>();
                    g.ActivaDeVientoCooldown = true;
                    g.SeleccionadorDeImagenes(c.ItemsEquipados[2].cooldownInicial);

                    Debug.Log("Se ejecuto el segundo if");

                }

                Debug.Log("Al menos se ejecuto el case 2");
                return;

        }


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
                    else if (ActivoStomper)
                    {

                        Vector3 Arriba = new Vector3(a.transform.position.x - 2, transform.position.y + 4f, a.transform.position.z);
                        GestorItems d = FindObjectOfType<GestorItems>();
                        GameObject y = Instantiate(PrefabStomper, Arriba, PrefabStomper.transform.rotation);
                        stomperTarget = Enemigo.GetComponent<LifeManager>();
                        Invoke("HacerDamageStomper", 0.7f);
                        Destroy(y, 1.3f);
                        ActivoStomper = false;
                        AnimacionIconos g = FindObjectOfType<AnimacionIconos>();
                        g.ActivaDeTierraCooldown = true;
                        d.ItemsEquipados[1].Activado = false;
                        g.SeleccionadorDeImagenes(d.ItemsEquipados[1].cooldownInicial);
                        d.StomperParticula.SetActive(false);
                    }
                    Enemigo.RecibirDamage();

                    ContarAtaquesParaRoboDeVida();
                }


            }
            else if (a.tag == "JefeFuego")
            {
                Golpeo = true;

                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                JefeFuego = FindObjectOfType<FuegoJefe>().GetComponent<LifeManager>();
                JefeFuego.RecibirDamage();

                ContarAtaquesParaRoboDeVida();
            }
            else if (a.tag == "JefeRoca")
            {
                Golpeo = true;

                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                LifeManager c = FindObjectOfType<JefeRoca>().GetComponent<LifeManager>();
                c.RecibirDamage();

                ContarAtaquesParaRoboDeVida();
            }
            else if (a.tag == "JefeViento")
            {
                Golpeo = true;

                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                LifeManager c = FindObjectOfType<JefeViento>().GetComponent<LifeManager>();
                c.RecibirDamage();

                ContarAtaquesParaRoboDeVida();
            }
            else if (a.tag == "JefeAgua")
            {
                Golpeo = true;

                GameObject f = Instantiate(AtaqueParticula, a.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(f, 1);
                LifeManager c = a.GetComponent<LifeManager>();
                c.RecibirDamage();

                ContarAtaquesParaRoboDeVida();
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

    void ContarAtaquesParaRoboDeVida()
    {
        EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
        ManejadorDeItems ManejadorDeItems = FindObjectOfType<ManejadorDeItems>();

        if (Estadisticas.RoboDeVida == true)
        {
            if (Estadisticas.ContadorRoboDeVida >= 5)
            {
                if (Estadisticas.VidaActualPersonaje < Estadisticas.VidaMaximaPersonaje)
                {
                    Estadisticas.VidaActualPersonaje += 1;
                    ManejadorDeItems.ManejadorDeVida();
                    Debug.Log("Te robe vida");
                    Estadisticas.ContadorRoboDeVida = 0;
                }
            }
            else
            {
                Estadisticas.ContadorRoboDeVida += 1;
                Debug.Log("Ataque y ahora el valor del contador es de " + Estadisticas.ContadorRoboDeVida);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(Arma.position, AreaAtaque);
    }

}

