using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GestorItems : MonoBehaviour
{

    public ChromaticAberration ca;
    public PostProcessVolume volume;

    public Vector3 ClonadorPosicion;

    public GameObject ClonEffect;
    private GameObject Clon;
    public float VidaGolden;
    public bool ExtraHeartsActivo;
    public float CooldownReduction;
    public GameObject ExplosiveMusic;
    public GameObject SolPatriaParticula;
    public GameObject StomperParticula;
    public Item[] ItemsEquipados;

    public GameObject PrefabPlanta;

    //public List<Slots> EstadoSlots;
    void Start()
    {
        //fuego, viento, tierra, agua
    }
    bool TerminoCD(int indice)
    {
        if (ItemsEquipados[indice].cooldownActual >= Mathf.Epsilon)
            return false;
        else
            return true;
    }
    // Update is called once per frame

    public void AplicarCDR()
    {
        for (int i = 0; i < ItemsEquipados.Length; i++)
        {
            ItemsEquipados[i].cooldownInicial = ItemsEquipados[i].CooldownOriginal * ((100 - CooldownReduction) / 100);
        }

    }
    public void AplicarCDR(float CDR)
    {
        CooldownReduction = CDR;
        for (int i = 0; i < ItemsEquipados.Length; i++)
        {
            ItemsEquipados[i].cooldownInicial = ItemsEquipados[i].CooldownOriginal * ((100 - CooldownReduction) / 100);
        }
        CooldownReduction = CDR;
    }


    void Update()
    {


        AplicarCDR();

        for (int i = 0; i < ItemsEquipados.Length; i++)
        {
            if (ItemsEquipados[i].item != null && !TerminoCD(i) && !ItemsEquipados[i].Activado)
            {
                ItemsEquipados[i].cooldownActual -= Time.deltaTime;

            }

        }
        if (ItemsEquipados[0].item != null && Input.GetKeyDown(KeyCode.Q) && !ItemsEquipados[0].Activado)
        {
            if (TerminoCD(0))
            {
                Ataque c = FindObjectOfType<Ataque>();

                string NombreDelObjeto = ItemsEquipados[0].item.GetComponent<InformacionDeItems>().Nombre;
                if (NombreDelObjeto == "Sol De La Patria")
                {
                    ItemsEquipados[0].cooldownActual = ItemsEquipados[0].cooldownInicial;
                    ItemsEquipados[0].cooldownActual -= Time.deltaTime;
                    c.ActivaPatria = true;
                    SolPatriaParticula.SetActive(true);
                    ItemsEquipados[0].Activado = true;
                }
                else
                {
                    ItemsEquipados[0].cooldownActual = ItemsEquipados[0].cooldownInicial;
                    ItemsEquipados[0].cooldownActual -= Time.deltaTime;
                    ExplosiveMusic.GetComponent<Animator>().enabled = false;
                    c.ActivaMusica = true;
                    ItemsEquipados[0].Activado = true;

                    ExplosiveMusic.SetActive(true);

                }


            }

        }


        if (ItemsEquipados[1].item != null && Input.GetKeyDown(KeyCode.R) && !ItemsEquipados[1].Activado)
        {
            if (TerminoCD(1))
            {
                EstadisticasDePersonaje d = FindObjectOfType<EstadisticasDePersonaje>();
                ManejadorDeItems f = FindObjectOfType<ManejadorDeItems>();
                AnimacionIconos g = FindObjectOfType<AnimacionIconos>();
                Ataque c = FindObjectOfType<Ataque>();



                string NombreDelObjeto = ItemsEquipados[1].item.GetComponent<InformacionDeItems>().Nombre;
                if (NombreDelObjeto == "Golem Heart")
                {
                    Invoke("GolemHeartOff", ItemsEquipados[1].cooldownInicial);



                    ItemsEquipados[1].Activado = true;
                    d.VidaMaximaPersonaje += 5;
                    VidaGolden = d.VidaActualPersonaje;
                    d.VidaActualPersonaje += 5;
                    f.ManejadorDeVida();
                    Color dorado = new Color();
                    ColorUtility.TryParseHtmlString("#EBC400", out dorado);
                    f.Contenido.color = dorado;
                    ExtraHeartsActivo = true;
                    ItemsEquipados[1].cooldownActual = ItemsEquipados[1].cooldownInicial;
                    ItemsEquipados[1].cooldownActual -= Time.deltaTime;
                }
                else
                {
                    ItemsEquipados[1].cooldownActual = ItemsEquipados[1].cooldownInicial;
                    ItemsEquipados[1].cooldownActual -= Time.deltaTime;
                    ItemsEquipados[1].Activado = true;
                    c.ActivoStomper = true;
                    StomperParticula.SetActive(true);

                }
            }
        }

        if (ItemsEquipados[2].item != null && Input.GetKeyDown(KeyCode.F) && !ItemsEquipados[2].Activado)
        {
            if (TerminoCD(2))
            {
                EstadisticasDePersonaje d = FindObjectOfType<EstadisticasDePersonaje>();
                ManejadorDeItems f = FindObjectOfType<ManejadorDeItems>();
                AnimacionIconos g = FindObjectOfType<AnimacionIconos>();
                Ataque c = FindObjectOfType<Ataque>();


                string NombreDelObjeto = ItemsEquipados[2].item.GetComponent<InformacionDeItems>().Nombre;
                if (NombreDelObjeto == "Clonadorinador")
                {
                    d.Inmortalidad = true;
                    Clon = Instantiate(ClonEffect, transform.position, Quaternion.identity);
                    Clon.tag = "Personaje";
                    Clon.layer = LayerMask.NameToLayer("Personaje");
                    int capa = LayerMask.NameToLayer("Default");
                    ItemsEquipados[2].Activado = true;
                    gameObject.layer = capa;
                    volume.profile.TryGetSettings(out ca);
                    ca.enabled.value = true;
                    ClonadorPosicion = transform.position;
                    foreach (Transform obj in transform)
                    {
                        obj.gameObject.layer = capa;
                    }
                    transform.tag = "Untagged";
                    ItemsEquipados[2].cooldownActual = ItemsEquipados[2].cooldownInicial;
                    ItemsEquipados[2].cooldownActual -= Time.deltaTime;
                    Invoke("DesaparecerClon", 5f);
                    EstadisticasDePersonaje stats = FindObjectOfType<EstadisticasDePersonaje>();
                }
                else
                {
                    volume.profile.TryGetSettings(out ca);
                    ca.enabled.value = true;
                    Invoke("RemoverInmortalidad", 5f);
                    EstadisticasDePersonaje est = FindObjectOfType<EstadisticasDePersonaje>();
                    est.Inmortalidad = true;
                    ItemsEquipados[2].cooldownActual = ItemsEquipados[2].cooldownInicial;
                    ItemsEquipados[2].cooldownActual -= Time.deltaTime;
                    ItemsEquipados[2].Activado = true;


                }
            }

        }
        if (ItemsEquipados[3] != null && Input.GetKeyDown(KeyCode.C) && !ItemsEquipados[3].Activado)
        {
            //Debug.Log("itemEJECUTADO");
            if(TerminoCD(3))
            {
                //Debug.Log("Esto se ejecuto");

                ManejadorDeItems ManejadorDeItems = FindObjectOfType<ManejadorDeItems>();
                AnimacionIconos Animaciones = FindObjectOfType<AnimacionIconos>();
                EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
                ManejadorDeItems ManejadorHUD = FindObjectOfType<ManejadorDeItems>();

                GameObject Personaje = GameObject.FindGameObjectWithTag("Personaje");

                string NombreDelObjeto = ItemsEquipados[3].item.GetComponent<InformacionDeItems>().Nombre;

                if(NombreDelObjeto == "Toxic Plant")
                {
                    //Debug.Log("Esto se ejecuto");
                    Animaciones.ActivaDeAguaCooldown = true;
                    GameObject PlantaAnterior = GameObject.FindGameObjectWithTag("PlantaActiva");

                    if(PlantaAnterior != null)
                    {
                        Destroy(PlantaAnterior);
                    }

                    Vector3 Posicion = new Vector3(Personaje.transform.position.x , Personaje.transform.position.y - 0.6f , Personaje.transform.position.z);

                    Instantiate(PrefabPlanta , Posicion , Quaternion.identity);

                    Animaciones.SeleccionadorDeImagenes(ItemsEquipados[3].cooldownInicial);
                    ItemsEquipados[3].cooldownActual = ItemsEquipados[3].cooldownInicial;
                    ItemsEquipados[3].cooldownActual -= Time.deltaTime;

                    Invoke("ReactivarPlanta" , ItemsEquipados[3].cooldownInicial);
                }
                else
                {
                    Animaciones.ActivaDeAguaCooldown = true;

                    Estadisticas.VidaActualPersonaje = Estadisticas.VidaMaximaPersonaje;
                    ManejadorHUD.ManejadorDeVida();

                    Animaciones.SeleccionadorDeImagenes(ItemsEquipados[3].cooldownInicial);
                    ItemsEquipados[3].cooldownActual = ItemsEquipados[3].cooldownInicial;
                    ItemsEquipados[3].cooldownActual -= Time.deltaTime;

                    Invoke("ReactivarPosion" , ItemsEquipados[3].cooldownInicial);
                }
            }
        }

    }
    public void RemoverInmortalidad()
    {
        EstadisticasDePersonaje est = FindObjectOfType<EstadisticasDePersonaje>();
        est.Inmortalidad = false;
        AnimacionIconos g = FindObjectOfType<AnimacionIconos>();

        g.ActivaDeVientoCooldown = true;
        g.SeleccionadorDeImagenes(ItemsEquipados[2].cooldownInicial);
        ItemsEquipados[2].Activado = false;
        ca.enabled.value = false;

    }
    
    public void DesaparecerClon()
    {

        EstadisticasDePersonaje d = FindObjectOfType<EstadisticasDePersonaje>();
        ManejadorDeItems f = FindObjectOfType<ManejadorDeItems>();
        AnimacionIconos g = FindObjectOfType<AnimacionIconos>();
        
        d.Inmortalidad = false;
        g.ActivaDeVientoCooldown = true;

        g.SeleccionadorDeImagenes(ItemsEquipados[2].cooldownInicial);
        Destroy(Clon);
        ItemsEquipados[2].Activado = false;
        if(ClonadorPosicion != Vector3.zero)
            transform.position = ClonadorPosicion;

        gameObject.tag = "Personaje";
        int capa = LayerMask.NameToLayer("Personaje");

        volume.profile.TryGetSettings(out ca);
        ca.enabled.value = false;

        EstadisticasDePersonaje stats = FindObjectOfType<EstadisticasDePersonaje>();

        gameObject.layer = capa;
        foreach (Transform obj in transform)
        {
            obj.gameObject.layer = capa;
        }
        ClonadorPosicion = Vector3.zero;

        //CancelInvoke("DesaparecerClon");

        print("xddddddd");
    }

    public void ReactivarPlanta()
    {
        AnimacionIconos Animaciones = FindObjectOfType<AnimacionIconos>();

        Animaciones.ActivaDeAguaCooldown = true;
    }

    public void ReactivarPosion()
    {
        AnimacionIconos Animaciones = FindObjectOfType<AnimacionIconos>();

        Animaciones.ActivaDeAguaCooldown = true;
    }

    public void GolemHeartOff()
    {
        EstadisticasDePersonaje d = FindObjectOfType<EstadisticasDePersonaje>();
        ManejadorDeItems f = FindObjectOfType<ManejadorDeItems>();
        AnimacionIconos g = FindObjectOfType<AnimacionIconos>();

        d.VidaMaximaPersonaje -= 5;
        if (VidaGolden < d.VidaActualPersonaje)
        {
            d.VidaActualPersonaje = VidaGolden;
        }
        f.ManejadorDeVida();
        ExtraHeartsActivo = false;
        ItemsEquipados[1].Activado = false;

        g.ActivaDeTierraCooldown = true;

        g.SeleccionadorDeImagenes(ItemsEquipados[1].cooldownInicial);


    }

    [System.Serializable]
    public class Item
    {
        public float CooldownOriginal;
        public bool Activado;
        public GameObject item;
        public float cooldownInicial;
        public float cooldownActual;
    }

}
