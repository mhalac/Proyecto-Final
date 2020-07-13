using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoAnim : MonoBehaviour
{

    // Use this for initialization
    private FuegoJefe Padre;
    void Start()
    {
        Padre = GetComponentInParent<FuegoJefe>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TermineAnimacion()
    {
		Padre.TermineAnimacion();
    }
    public void TermineAtacar()
    {
        Padre.TermineDeAtacar();
    }
}
