using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinalScrpt : MonoBehaviour
{
    public estados estado;
    public GameObject Ojo;
    private GameObject Personaje;

    [Header("Viento")]
    public GameObject VientoParticula;
    public bool InicieCorrutina;

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
    IEnumerator AtaqueViento()
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
        for (int i = 0; i < 22; i++)
        {
            Vector3 direction = (Personaje.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);
            yield return new WaitForEndOfFrame();

        }
       

        GameObject b = Instantiate(VientoParticula, Ojo.transform.position, Quaternion.identity);
        b.transform.LookAt(Personaje.transform.position);
        

        yield return new WaitForSeconds(3f);
        Destroy(b);
        InicieCorrutina = false;

    }


    void OrbitAroundPlayer()
    {
        transform.RotateAround(Personaje.transform.position, Vector3.up, 59 * Time.deltaTime);
        transform.LookAt(Personaje.transform.position);
    }
}
