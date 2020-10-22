using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinalScrpt : MonoBehaviour
{

    private VibracionCamara vibracion;
    private LayerMask Pmask;
    private RaycastHit hit;
    public estados estado;
    public GameObject Ojo;
    private GameObject Personaje;

    [Header("Viento")]
    public GameObject VientoParticula;
    public bool InicieCorrutina;
    [Header("Fuego")]
    public GameObject FuegoParticula;
    private bool InicieCorrutinaFuego;
    public enum estados
    {
        Fuego,
        Tierra,
        Viento,
        Agua,
        Orbiting


    }
    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Personaje");
        Pmask = LayerMask.NameToLayer("Personaje");
        vibracion = FindObjectOfType<VibracionCamara>();
    }

    void Update()
    {

        switch (estado)
        {
            case estados.Orbiting:
                OrbitAroundPlayer();
                return;
            case estados.Viento:
                ShootViento();
                return;
            case estados.Fuego:
                EstadoFuego();
                return;
        }


    }

    void EstadoFuego()
    {
        if(!InicieCorrutinaFuego)
        {
            StartCoroutine(AtaqueFuego());
            InicieCorrutinaFuego = true;
        }
    }
    void ShootViento()
    {
        if (!InicieCorrutina)
        {
            StartCoroutine(AtaqueViento());
            InicieCorrutina = true;
        }
    }
    IEnumerator AtaqueFuego()
    {
        for(int i = 0; i < 9;i++)
        {
            
            Physics.Raycast(transform.position,transform.up * -1,out hit, Mathf.Infinity);
            Vector3 pos = new Vector3(Personaje.transform.position.x, hit.point.y,Personaje.transform.position.z);
            vibracion.StartCoroutine(vibracion.Shake(.15f, .4f));
            yield return new WaitForSeconds(1.6f);
            GameObject c = Instantiate(FuegoParticula,pos,FuegoParticula.transform.rotation);
            Destroy(c,4f);
            yield return new WaitForSeconds(4);
        }
    }
    IEnumerator AtaqueViento()
    {
        for (int indice = 0; indice < 6; indice++)
        {
            Ray c = new Ray(Personaje.transform.position, Personaje.transform.up);
            Vector3 pos = c.GetPoint(15);
            Debug.DrawRay(c.origin, c.direction, Color.red);
            while (Vector3.Distance(pos, transform.position) > 3)
            {
                c = new Ray(Personaje.transform.position, Personaje.transform.up);
                pos = c.GetPoint(15);
                transform.position = Vector3.MoveTowards(transform.position, pos, 20 * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 15; i++)
            {
                Vector3 direction = (Personaje.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 25);
                yield return new WaitForEndOfFrame();

            }
            yield return new WaitForSeconds(0.5f);
            List<GameObject> Blasters = new List<GameObject>();

            for (int i = 0; i < 2; i++)
            {
                float ran1 = Random.Range(10, -10);

                Vector3 pos1 = new Vector3(ran1 + transform.position.x, ran1 + transform.position.y - 1.4f, ran1 + transform.position.z);
                GameObject b = Instantiate(VientoParticula, pos1, Quaternion.identity);
                b.transform.LookAt(Personaje.transform.position);
                Blasters.Add(b);


            }



            yield return new WaitForSeconds(.4f);


            for (int i = 0; i < 10; i++)
            {

                foreach (GameObject bla in Blasters)
                {
                    Physics.Raycast(bla.transform.position, bla.transform.forward, out hit, Mathf.Infinity);

                    Collider[] objs = Physics.OverlapCapsule(bla.transform.position, hit.point, 1.9f);


                    //Debug.DrawRay(bla.transform.position, bla.transform.forward * 999, Color.black);
                    //Debug.DrawLine(bla.transform.position, hit.point, Color.cyan);
                    //Debug.Break();

                    foreach (Collider f in objs)
                    {
                        print(f.tag);
                        if (f.CompareTag("Personaje"))
                        {
                            EstadisticasDePersonaje EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
                            EstadisticasDePersonaje.RecibirDaño(5f);
                        }
                    }

                    yield return new WaitForSeconds(0.1f);

                }
            }
            foreach (GameObject bla in Blasters)
            {
                Destroy(bla.gameObject);
            }

        }
        InicieCorrutina = false;
        estado = estados.Fuego;

    }


    void OrbitAroundPlayer()
    {
        transform.RotateAround(Personaje.transform.position, Vector3.up, 59 * Time.deltaTime);
        transform.LookAt(Personaje.transform.position);
    }
}
