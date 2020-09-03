using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viento2Anim : MonoBehaviour
{
    public Viento2 Padre;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void IrALugarRandom()
    {
        Padre.IrAPosRandom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
