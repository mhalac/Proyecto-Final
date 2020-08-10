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
        if (ItemsEquipados[indice].cooldownActual > 0)
            return false;
        else
            return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (ItemsEquipados.Length >= 1 && !TerminoCD(0))
            ItemsEquipados[0].cooldownActual -= Time.deltaTime;
        if (ItemsEquipados.Length >= 2 && !TerminoCD(1))
            ItemsEquipados[1].cooldownActual -= Time.deltaTime;
        if (ItemsEquipados.Length >= 3 && !TerminoCD(2))
            ItemsEquipados[2].cooldownActual -= Time.deltaTime;
        if (ItemsEquipados.Length >= 4 && !TerminoCD(3))
            ItemsEquipados[3].cooldownActual -= Time.deltaTime;

        if (ItemsEquipados[0] != null && Input.GetKeyDown(KeyCode.Q))
        {
            if (TerminoCD(0))
            {
                ItemsEquipados[0].cooldownActual = ItemsEquipados[0].cooldownInicial; ;
                print("guigniangiangiangna");
                ItemsEquipados[0].cooldownActual -= Time.deltaTime;

                //hacer cosas de item
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
        public GameObject item;
        public float cooldownInicial;
        public float cooldownActual;
    }

}
