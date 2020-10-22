using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hielo : MonoBehaviour
{
    // Start is called before the first frame update

    private bool golpie = false;
    void Start()
    {
        Invoke("Ejecutar", 2.1f);
    }
    void Ejecutar()
    {
        Vector3 size = new Vector3(10, 6, 8);
        Collider[] colls = Physics.OverlapBox(transform.position, size);
        foreach (Collider obj in colls)
        {
            if (obj.CompareTag("Personaje") && !golpie)
            {
                golpie = true;
                EstadisticasDePersonaje EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
                EstadisticasDePersonaje.RecibirDaño(10f);
                StartCoroutine(Empujar());
            }
        }
    }
    // Update is called once per frame
    IEnumerator Empujar()
    {
        for (int i = 0; i < 30; i++)
        {    
            GameObject.FindGameObjectWithTag("Personaje").GetComponent<CharacterController>().Move(GameObject.FindGameObjectWithTag("Personaje").GetComponent<Transform>().forward * -1
             * Time.deltaTime * 35);
            yield return new WaitForEndOfFrame();
        }
    }
    void Update()
    {

    }
}
