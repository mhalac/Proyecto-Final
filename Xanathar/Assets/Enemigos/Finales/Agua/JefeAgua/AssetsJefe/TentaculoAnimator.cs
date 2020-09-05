using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaculoAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    TentaculoScript parent;

    void Start()
    {
        parent = GetComponentInParent<TentaculoScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ataque()
    {
        parent.Ataque();
    }
    public void TermineAtacar()
    {
        parent.TermineAtacar();
    }

}
