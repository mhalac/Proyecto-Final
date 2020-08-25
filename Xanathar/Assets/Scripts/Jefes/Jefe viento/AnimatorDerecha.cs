using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDerecha : MonoBehaviour
{
    private JefeViento Jefe;

    // Use this for initialization
    void Start()
    {
        Jefe = FindObjectOfType<JefeViento>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TerminoTornados()
    {
        Jefe.TermineAnimTornados();
    }
    public void EmpiezoACastearTornados()
    {
        Jefe.ApareciendoTornados = true;
    }
    public void CasteoTornado()
    {
        Jefe.SpawnearTornados();
    }

    public void EmpezoAAplastar()
    {
        Jefe.EstaAplastando = true;
    }
    public void Slam()
    {
        Jefe.GolpearElPiso();
    }

    public void TerminoDeAplastar()
    {
        Jefe.AnimatorDerecha.SetBool("Aplastar",false);
        Jefe.EstaAplastando = false;

    }
	
  
}
