using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua1Anim : MonoBehaviour
{
    public Agua1 Padre;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetearPos()
    {
        Padre.PonerPosRandomYSumergirse();
    }

    public void Tepearse()
    {
        Padre.Tepearse();
    }

    public void VolverIdle()
    {
        Padre.VolverAlIdle();
    }
}
