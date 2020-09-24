using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarradorDeItems : MonoBehaviour
{
    private int layerMask = ~(1 << 9);
    RaycastHit ColisionDeObjeto;
    float Rango = 5;
    string OrdenDeElemento;
    public GameObject[] ObjetosEquipados = new GameObject[12];
    public GameObject[] CopiaDeObjetos = new GameObject[12];
    public GameObject Instanciador;
    private ManejadorDeItems ManejadorDeHUD;

    public float RangoDeInstancia;
    public float RadioDeObjetos;

    // Use this for initialization
    void Start()
    {
        ManejadorDeHUD = FindObjectOfType<ManejadorDeItems>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastDeItems();

        if (Input.GetKeyDown(KeyCode.J))
        {
            DropeadorDeItems();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * Rango);
    }

    private void RaycastDeItems()
    {
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ColisionDeObjeto, Rango, layerMask) && ColisionDeObjeto.collider.gameObject.tag == "Items")
        {
            EquipadorDeItems();
        }
    }

    private void EquipadorDeItems()
    {


        string ElementoDelObjeto = ColisionDeObjeto.collider.gameObject.GetComponent<InformacionDeItems>().Elemento;
        string CategoriaDelObjeto = ColisionDeObjeto.collider.gameObject.GetComponent<InformacionDeItems>().Categoria;
        string NombreDelObjeto = ColisionDeObjeto.collider.gameObject.GetComponent<InformacionDeItems>().Nombre;


        GameObject[] Objetos = Resources.LoadAll<GameObject>("Objetos");
        GameObject[] AlmacenadorDeObjetos = new GameObject[2];

        for (int i = 0; i < Objetos.Length; i++)
        {
            if (Objetos[i].GetComponent<InformacionDeItems>().Elemento == ElementoDelObjeto && Objetos[i].GetComponent<InformacionDeItems>().Categoria == CategoriaDelObjeto)
            {
                if (Objetos[i].GetComponent<InformacionDeItems>().Nombre == NombreDelObjeto)
                {
                    AlmacenadorDeObjetos[0] = Objetos[i];
                }

                if (Objetos[i].GetComponent<InformacionDeItems>().Nombre != NombreDelObjeto)
                {
                    AlmacenadorDeObjetos[1] = Objetos[i];
                }

                if (AlmacenadorDeObjetos[0] != null && AlmacenadorDeObjetos[1] != null)
                {
                    break;
                }
            }
        }

        AgarrarYReemplazar(AlmacenadorDeObjetos[0], AlmacenadorDeObjetos[1], ElementoDelObjeto, CategoriaDelObjeto, NombreDelObjeto);
    }

    private void AgarrarYReemplazar(GameObject ObjetoAgarrado, GameObject ObjetoNoAgarrado, string ElementoDelObjetoAgarrado, string CategoriaDelObjetoAgarrado, string NombreDelObjetoAgarrado)
    {
        string ElementoItemEquipado;
        string CategoriaItemEquipado;
        string NombreItemEquipado;

        for (int i = 0; i < ObjetosEquipados.Length; i++)
        {
            if (ObjetosEquipados[i] == null)
            {
                ObjetosEquipados[i] = ObjetoAgarrado;
                Destroy(ColisionDeObjeto.collider.gameObject);

                EquipadorActivasPasivasYEstadisticas(CategoriaDelObjetoAgarrado, ElementoDelObjetoAgarrado, NombreDelObjetoAgarrado, ObjetoAgarrado);
                break;
            }

            ElementoItemEquipado = ObjetosEquipados[i].GetComponent<InformacionDeItems>().Elemento;
            CategoriaItemEquipado = ObjetosEquipados[i].GetComponent<InformacionDeItems>().Categoria;
            NombreItemEquipado = ObjetosEquipados[i].GetComponent<InformacionDeItems>().Nombre;

            if (NombreItemEquipado == NombreDelObjetoAgarrado)
            {
                ObjetosEquipados[i] = ObjetoAgarrado;
                Destroy(ColisionDeObjeto.collider.gameObject);

                Vector3 PosAInstanciar = new Vector3(Instanciador.transform.position.x, (Instanciador.transform.position.y + 1), Instanciador.transform.position.z);
                Instantiate(ObjetoAgarrado, PosAInstanciar, Quaternion.identity);
                break;
            }

            if (ElementoItemEquipado == ElementoDelObjetoAgarrado && CategoriaItemEquipado == CategoriaDelObjetoAgarrado)
            {
                ObjetosEquipados[i] = ObjetoAgarrado;
                Destroy(ColisionDeObjeto.collider.gameObject);

                EquipadorActivasPasivasYEstadisticas(CategoriaDelObjetoAgarrado, ElementoDelObjetoAgarrado, NombreDelObjetoAgarrado, ObjetoAgarrado);

                DesequiparActivasPasivasYEstadisticas(CategoriaItemEquipado, ElementoItemEquipado, NombreItemEquipado, ObjetoNoAgarrado);

                Vector3 PosAInstanciar = new Vector3(Instanciador.transform.position.x, (Instanciador.transform.position.y + 1), Instanciador.transform.position.z);
                Instantiate(ObjetoNoAgarrado, PosAInstanciar, Quaternion.identity);
                break;
            }
        }

        string NombreDelObjeto = ObjetoAgarrado.GetComponent<InformacionDeItems>().Nombre;
        string CategoriaDelObjeto = ObjetoAgarrado.GetComponent<InformacionDeItems>().Categoria;
        string DescripcionDelObjeto = ObjetoAgarrado.GetComponent<InformacionDeItems>().Descripcion;
        string ElementoDelObjeto = ObjetoAgarrado.GetComponent<InformacionDeItems>().Elemento;

        ManejadorDeHUD.MostradorDeMensajeNotificador(NombreDelObjeto, CategoriaDelObjeto, DescripcionDelObjeto);
        ManejadorDeHUD.EquipadorSlotsDeItems(CategoriaDelObjeto, ElementoDelObjeto, ObjetoAgarrado);
    }

    public void DropeadorDeItems()
    {
        if (ControlarPuertasYJefes.JefeEliminado == true)
        {
            for (int i = 0; i < ObjetosEquipados.Length; i++)
            {
                CopiaDeObjetos[i] = ObjetosEquipados[i];
            }
        }

        GameObject PosParaInstanciar = GameObject.FindGameObjectWithTag("PosicionClave");

        for (int i = 0; i < ObjetosEquipados.Length; i++)
        {
            if (ObjetosEquipados[i] == null)
            {
                break;
            }

            Vector3 PosicionObjeto = Vector3.zero;
            bool PosicionValida = false;
            int Seguro = 0;

            while (PosicionValida == false && Seguro < 50)
            {

                Seguro += 1;
                PosicionObjeto = new Vector3(Random.Range(-RangoDeInstancia, RangoDeInstancia) + PosParaInstanciar.transform.position.x, PosParaInstanciar.transform.position.y, Random.Range(-RangoDeInstancia, RangoDeInstancia) + PosParaInstanciar.transform.position.z);
                PosicionValida = true;
                Collider[] Colisiones = Physics.OverlapSphere(PosicionObjeto, RadioDeObjetos);

                foreach (Collider col in Colisiones)
                {
                    if (col.tag == "Items" || col.tag == "Personaje" || col.tag == "Piso")
                    {
                        PosicionValida = false;
                    }
                }
            }

            if (PosicionValida == true)
            {
                string NombreDelObjeto = ObjetosEquipados[i].GetComponent<InformacionDeItems>().Nombre;
                EquiparEstadisticasYPasivas.DesEquiparEstadistica(NombreDelObjeto);
                EquiparEstadisticasYPasivas.DesEquiparPasiva(NombreDelObjeto);


                Instantiate(ObjetosEquipados[i], PosicionObjeto, Quaternion.identity);
                ObjetosEquipados[i] = null;
            }
        }

        ManejadorDeHUD.DesactivadorSlots();
    }

    public void EquipadorActivasPasivasYEstadisticas(string Categoria, string Elemento, string Nombre, GameObject ObjetoAgarrado)
    {
        switch (Categoria)
        {
            case "Activa":


                GestorItems c = FindObjectOfType<GestorItems>();
                Ataque b = FindObjectOfType<Ataque>();


                switch (Elemento)
                {
                    case "Fuego":
                        float FuegoCD = GetComponent<EstadisticasDePersonaje>().TiempoCooldownActivas[0];
                        b.Reset(0);
                        if (Nombre == "Sol De La Patria")
                        {
                            c.ItemsEquipados[0].item = ObjetoAgarrado;
                            c.ItemsEquipados[0].cooldownInicial = FuegoCD;
                            c.ItemsEquipados[0].CooldownOriginal = FuegoCD;
                            c.AplicarCDR();
                        }
                        else
                        {
                            c.ItemsEquipados[0].item = ObjetoAgarrado;
                            c.ItemsEquipados[0].cooldownInicial = FuegoCD;
                            c.ItemsEquipados[0].CooldownOriginal = FuegoCD;

                            c.AplicarCDR();
                        }
                        break;

                    case "Viento":
                        float VientoCD = GetComponent<EstadisticasDePersonaje>().TiempoCooldownActivas[2];
                        c.ItemsEquipados[2].item = ObjetoAgarrado;
                        c.ItemsEquipados[2].cooldownInicial = VientoCD;
                        c.ItemsEquipados[2].CooldownOriginal = VientoCD;
                        c.AplicarCDR();
                        b.Reset(2);
                        break;

                    case "Tierra":
                        float TierraCD = GetComponent<EstadisticasDePersonaje>().TiempoCooldownActivas[1];
                        b.Reset(1);

                        c.ItemsEquipados[1].item = ObjetoAgarrado;
                        c.ItemsEquipados[1].cooldownInicial = TierraCD;
                        c.ItemsEquipados[1].CooldownOriginal = TierraCD;

                        c.AplicarCDR();


                        break;

                    case "Agua":
                        Debug.Log("Activa de Agua");
                        break;

                    default:
                        Debug.Log("Fallo en reconocer el elemento del objeto");
                        break;
                }

                break;

            case "Pasiva":
                EquiparEstadisticasYPasivas.EquiparPasiva(Nombre);
                break;


            case "Estadistica":
                EquiparEstadisticasYPasivas.EquiparEstadistica(Nombre);
                break;


            default:
                Debug.Log("Fallo En reconocer la categoria del objeto");
                break;
        }
    }

    public void DesequiparActivasPasivasYEstadisticas(string Categoria, string Elemento, string Nombre, GameObject ObjetoADesequipar)
    {
        switch (Categoria)
        {
            case "Activa":


                switch (Elemento)
                {
                    case "Fuego":
                        break;

                    case "Viento":
                        break;

                    case "Tierra":
                        break;

                    case "Agua":
                        break;

                    default:
                        Debug.Log("Fallo en reconocer el elemento del Objeto");
                        break;
                }


                break;


            case "Pasiva":
                EquiparEstadisticasYPasivas.DesEquiparPasiva(Nombre);
                break;


            case "Estadistica":
                EquiparEstadisticasYPasivas.DesEquiparEstadistica(Nombre);
                break;


            default:
                Debug.Log("Fallo en reconocer la categoria del objeto");
                break;
        }
    }
}
