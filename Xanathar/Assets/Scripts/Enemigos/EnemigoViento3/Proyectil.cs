using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    GameObject Personaje;
    public float VelocidadTornado;
    public bool TornadoDesapareciendo;
    // Start is called before the first frame update
    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Personaje");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position , Personaje.transform.position , VelocidadTornado * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider Collision)
    {
        if(Collision.gameObject.tag == "Personaje")
        {
            Debug.Log("Choco con el personaje");
            Destroy(this.gameObject);
        }
    }
}
