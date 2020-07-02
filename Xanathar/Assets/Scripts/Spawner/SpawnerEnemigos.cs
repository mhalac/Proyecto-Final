using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{


    public GameObject EnemigoASpawnear;
    public float Veces;
    public float Delay;

    public Vector3 Destino;
    private float DelayInicial;
    public float Size;
    private int PMask;
    public List<GameObject> EnemigosSpawneados = new List<GameObject>();
    private Transform TInicial;

    // Use this for initialization
    void Start()
    {
        DelayInicial = Delay;
        TInicial = transform;
    }

    public void ModificarDelay(float Delay)
    {
        DelayInicial = Delay;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; EnemigosSpawneados.Count > i; i++)
        {
            if (EnemigosSpawneados[i] == null)
            {
                EnemigosSpawneados.Remove(EnemigosSpawneados[i]);
            }
        }
        // se tienen que spawnear enemigos
        if (EnemigosSpawneados.Count < Veces)
        {
            Delay -= Time.deltaTime;

            Vector3 Posicion = RandomPos();

            if (Posicion != Vector3.zero && Delay < Mathf.Epsilon)
            {

                GameObject obj = Instantiate(EnemigoASpawnear, Posicion, Quaternion.identity);
                EnemigosSpawneados.Add(obj);
                Delay = DelayInicial;
            }

        }
    }
    private Vector3 RandomPos()
    {

        float RandomX = Random.Range(TInicial.position.x - Size, Size + TInicial.position.x);
        float RandomZ = Random.Range(TInicial.position.z - Size, Size + TInicial.position.z);

        Vector3 posRandom = new Vector3(RandomX, transform.position.y, RandomZ);

        Collider[] Objs = Physics.OverlapSphere(posRandom, 1f);
        for (int i = 0; i < Objs.Length; i++)
        {
            if (Objs[i].gameObject.tag == "Entorno")
            {
                return Vector3.zero;
            }

        }
        return posRandom;
    }
    void OnDrawGizmosSelected()
    {
        Vector3 cubo = new Vector3(Size * 2, 2, Size * 2);
        Gizmos.DrawWireCube(transform.position, cubo);
    }
}
