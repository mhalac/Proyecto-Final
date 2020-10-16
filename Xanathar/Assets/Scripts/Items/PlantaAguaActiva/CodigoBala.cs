using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodigoBala : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemigo")
        {
            other.GetComponent<LifeManager>().RecibirDamage();
            Destroy(gameObject);
        }
    }
}
