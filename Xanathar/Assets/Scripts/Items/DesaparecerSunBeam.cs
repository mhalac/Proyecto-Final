using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaparecerSunBeam : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
		Destroy(gameObject,0.7f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
