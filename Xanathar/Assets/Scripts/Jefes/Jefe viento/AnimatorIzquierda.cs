﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorIzquierda : MonoBehaviour
{

    // Use this for initialization
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
    public void Empujando()
    {
        Jefe.EmpujandoConFuerza = true;
    }

    public void MeEstoyMuriendo()
    {
        Jefe.Muriendo = true;
    }
    public void Morir()
    {
        Jefe.Muriendo = false;
        Invoke("Revivir",3f);
    }
    private void Revivir()
    {
        Jefe.Mori = false;
        Jefe.AnimatorDerecha.SetBool("Morir", false);
        Jefe.AnimatorIzquierda.SetBool("Morir", false);


    }
    public void EmpezoAEmpujar()
    {
        Jefe.EstaEmpujando = true;
        Jefe.AnimatorIzquierda.SetBool("TerminoReset", false);

    }
    public void TerminoDeEmpujar()
    {
        Invoke("ResetEmpujar", 1f);
        Jefe.EmpujandoConFuerza = false;

    }
    private void ResetEmpujar()
    {
        Jefe.AnimatorIzquierda.SetBool("Empujar", false);
        Jefe.AnimatorDerecha.SetBool("Aplastar", false);

        Jefe.AnimatorDerecha.SetBool("Levantate", false);

    }
    public void TerminoAnimacionReset()
    {
        Jefe.EstaEmpujando = false;
        Jefe.AnimatorIzquierda.SetBool("TerminoReset", true);

    }
}
