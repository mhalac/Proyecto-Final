using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buho : MonoBehaviour
{
    public AudioSource[] Sonidos;
	public float TiempoDeInicio;
	public float Intervalo;
    public Vector2 RandomTime;
	public bool reproduci;
    public Vector2 DelayInicial;

    // Use this for initialization
    void Start()
    {
        TiempoDeInicio = Random.Range(DelayInicial.x, DelayInicial.y);
        Invoke("ReproducirSonido", TiempoDeInicio);
    }

    void ReproducirSonido()
    {
        int i = Random.Range(0, Sonidos.Length);
        Sonidos[i].Play();
        Intervalo = Random.Range(RandomTime.x, RandomTime.y);
		reproduci = true;
        Invoke("ReproducirSonido", Intervalo);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
