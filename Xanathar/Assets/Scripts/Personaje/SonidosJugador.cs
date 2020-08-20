using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosJugador : MonoBehaviour
{
    public bool EstaSonandoAudio;

    private MovimientoPersonaje c;
    public AudioSource[] Sonidos;
    // Use this for initialization
    void Start()
    {
        c = FindObjectOfType<MovimientoPersonaje>();

    }

    // Update is called once per frame
    void Update()
    {
        EstaSonandoAudio = false;
        foreach (AudioSource item in Sonidos)
        {
            if (item.isPlaying)
            {
                EstaSonandoAudio = true;
            }
        }



        if (!EstaSonandoAudio && !c.EstaQuieto && !c.EstaSaltando)
        {
            int i = Random.Range(0, Sonidos.Length);
            Sonidos[i].Play();
        }
    }
}
