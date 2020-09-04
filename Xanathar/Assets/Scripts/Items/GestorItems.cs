using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorItems : MonoBehaviour
{
    public float CooldownReduction;
    public GameObject ExplosiveMusic;
    public GameObject SolPatriaParticula;
    public bool test;
    public Item[] ItemsEquipados;
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
        for (int i = 0; i < ItemsEquipados.Length; i++)
        {
            if (ItemsEquipados[i].item != null && !TerminoCD(i) && !ItemsEquipados[i].Activado)
            {
                ItemsEquipados[i].cooldownActual -= Time.fixedDeltaTime;

            }
            else if (ItemsEquipados[i].item != null && ItemsEquipados[i].item.GetComponent<InformacionDeItems>().Nombre == "Sol De La Patria")
            {
                AnimacionIconos d = FindObjectOfType<AnimacionIconos>();
                d.ActivaDeFuegoCooldown = false;

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
                    ItemsEquipados[0].cooldownActual -= Time.fixedDeltaTime;
                    c.ActivaPatria = true;
                    SolPatriaParticula.SetActive(true);
                    ItemsEquipados[0].Activado = true;
                }
                else
                {
                    ItemsEquipados[0].cooldownActual = ItemsEquipados[0].cooldownInicial;
                    ItemsEquipados[0].cooldownActual -= Time.fixedDeltaTime;
                    ExplosiveMusic.GetComponent<Animator>().enabled = false;
                    c.ActivaMusica = true;
                    ItemsEquipados[0].Activado = true;

                    ExplosiveMusic.SetActive(true);

                }


            }

        }

        if (ItemsEquipados[1].item != null && Input.GetKeyDown(KeyCode.E))
        {
            if (TerminoCD(1))
            {
                ItemsEquipados[1].cooldownActual = ItemsEquipados[1].cooldownInicial; ;
                ItemsEquipados[1].cooldownActual -= Time.fixedDeltaTime;

                //hacer cosas de item
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {

        }
        if (Input.GetKeyDown(KeyCode.T))
        {

        }

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
