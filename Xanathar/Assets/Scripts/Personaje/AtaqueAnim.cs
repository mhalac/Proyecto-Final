using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAnim : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    public void Atacar()
    {
        Ataque c = FindObjectOfType<Ataque>();
        c.anim.speed = 1 / c.CoolDownInicial;
        GestorItems b = FindObjectOfType<GestorItems>();
        

        c.HacerDamage();
    }
    public void Termine()
    {
        Ataque c = FindObjectOfType<Ataque>();

        c.anim.speed = 1;
        c.anim.SetBool("atacando", false);
    }
    public void TermineDeCorrer()
    {
        Ataque c = FindObjectOfType<Ataque>();

        c.anim.speed = 1;

    }

    public void EmpiezaACorrer()
    {
        Ataque c = FindObjectOfType<Ataque>();

        c.anim.speed = 1;

    }


    // Update is called once per frame
    void Update()
    {

    }
}
