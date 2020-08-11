using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorItems : MonoBehaviour
{
    public Item[] ItemsEquipados;
    //public List<Slots> EstadoSlots;
    void Start()
    {

        //fuego, viento, tierra, agua
    }
    bool TerminoCD(int indice)
    {
        if (ItemsEquipados[indice].cooldownActual >= 0)
            return false;
        else
            return true;
    }
    // Update is called once per frame

    /*if (ItemsEquipados[0].item != null && !TerminoCD(0))
           ItemsEquipados[0].cooldownActual -= Time.deltaTime;
       if (ItemsEquipados[1].item != null && !TerminoCD(1))
           ItemsEquipados[1].cooldownActual -= Time.deltaTime;
       if (ItemsEquipados[2].item != null && !TerminoCD(2))
           ItemsEquipados[2].cooldownActual -= Time.deltaTime;
       if (ItemsEquipados[3].item != null && !TerminoCD(3))
           ItemsEquipados[3].cooldownActual -= Time.deltaTime;*/


    void Update()
    {
        for (int i = 0; i < ItemsEquipados.Length; i++)
        {
            if (ItemsEquipados[i].item != null && !TerminoCD(i) && !ItemsEquipados[i].Activado)
                ItemsEquipados[i].cooldownActual -= Time.deltaTime;

        }



        if (ItemsEquipados[0].item != null && Input.GetKeyDown(KeyCode.Q) && !ItemsEquipados[0].Activado)
        {
            if (TerminoCD(0))
            {
                string NombreDelObjeto = ItemsEquipados[0].item.GetComponent<InformacionDeItems>().Nombre;
                if (NombreDelObjeto == "Sol De La Patria")
                {
                    ItemsEquipados[0].cooldownActual = ItemsEquipados[0].cooldownInicial;
                    ItemsEquipados[0].cooldownActual -= Time.deltaTime;
                    Ataque c = FindObjectOfType<Ataque>();
                    c.ActivaPatria = true;
                    ItemsEquipados[0].Activado = true;
                }
                else
                {

                }

                
            }

        }

        if (ItemsEquipados[1].item != null && Input.GetKeyDown(KeyCode.E))
        {
            if (TerminoCD(1))
            {
                ItemsEquipados[1].cooldownActual = ItemsEquipados[1].cooldownInicial; ;
                print("guigniangiangiangna");
                ItemsEquipados[1].cooldownActual -= Time.deltaTime;

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
        public bool Activado;
        public GameObject item;
        public float cooldownInicial;
        public float cooldownActual;
    }

}
