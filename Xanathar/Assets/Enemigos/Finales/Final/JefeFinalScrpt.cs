using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JefeFinalScrpt : MonoBehaviour
{
    public AudioSource FuegoSound;
    public AudioSource HieloSound;


    public AudioSource PreTerremoto;

    public float Y;
    private BarraDeVidaJefe barra;
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
    [Header("Roca")]
    public GameObject Roca1;
    public GameObject Roca2;
    public GameObject Roca3;
    public bool InicieCorrutinaRoca;
    [Header("Agua")]
    public bool InicieCorrutinaAgua;
    public GameObject AguaPrefab;
    private float timer;
    private LifeManager life;
    public enum estados
    {
        Fuego,
        Tierra,
        Viento,
        Agua,
        Orbiting,
        Muriendo


    }
    void Start()
    {
        Physics.Raycast(transform.position, transform.up * -1, out hit, Mathf.Infinity);
        Y = hit.point.y;
        life = GetComponent<LifeManager>();
        Personaje = GameObject.FindGameObjectWithTag("Personaje");
        Pmask = LayerMask.NameToLayer("Personaje");
        vibracion = FindObjectOfType<VibracionCamara>();
        barra = FindObjectOfType<BarraDeVidaJefe>();
        barra.ValorDeVidaMaxima = GetComponent<LifeManager>().Vida;
    }

    void Update()
    {
        barra.ValorDeVidaActual = GetComponent<LifeManager>().Vida;
        if (timer > 4f)
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
                case estados.Tierra:
                    EstadoTierra();
                    return;
                case estados.Agua:
                    EstadoAgua();
                    return;
                case estados.Muriendo:
                    return;
            }
        }
        else
            timer += Time.deltaTime;


    }

    estados NuevoEstado(int i)
    {
        int f = Random.Range(0, 4);
        switch (f)
        {
            case 0:
                if (i != 0)
                    return estados.Agua;
                else
                    return NuevoEstado(i);
            case 1:
                if (i != 1)
                    return estados.Tierra;
                else
                    return NuevoEstado(i);
            case 2:
                if (i != 2)
                    return estados.Fuego;
                else
                    return NuevoEstado(i);
            case 3:
                if (i != 3)
                    return estados.Viento;
                else
                    return NuevoEstado(i);
            default:
                return estados.Viento;
        }

    }
    void EstadoAgua()
    {
        if (!InicieCorrutinaAgua)
        {
            InicieCorrutinaAgua = true;
            StartCoroutine(AtaqueAgua());
        }
    }
    void EstadoTierra()
    {
        if (!InicieCorrutinaRoca)
        {
            InicieCorrutinaRoca = true;
            StartCoroutine(AtaqueRoca());
        }
    }

    void EstadoFuego()
    {
        if (!InicieCorrutinaFuego)
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
    void TerminarJuego()
    {
        print("guniagnaing");
    }
    IEnumerator Subir()
    {
        while (true)
        {
            transform.Translate(0, 0.04f, 0);
            yield return new WaitForEndOfFrame();
        }

    }
    void Mori()
    {
        if (life.Vida <= 0)
        {
            estado = estados.Muriendo;
            InicieCorrutina = true;
            InicieCorrutinaAgua = true;
            InicieCorrutinaRoca = true;
            InicieCorrutinaFuego = true;
            Animator c = GetComponentInChildren<Animator>();
            c.SetBool("die", true);
            Invoke("TerminarJuego", 7f);
            StartCoroutine(Subir());
        }
    }
    IEnumerator AtaqueAgua()
    {
        for (int contador = 0; contador < 6; contador++)
        {
            Ray c = new Ray(Personaje.transform.position, Personaje.transform.up);
            Vector3 pos = c.GetPoint(15);
            transform.LookAt(Personaje.transform.position);


            while (Vector3.Distance(pos, transform.position) > 12)
            {
                c = new Ray(Personaje.transform.position, Personaje.transform.up);
                pos = c.GetPoint(15);
                transform.position = Vector3.MoveTowards(transform.position, pos, 50 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            while (Vector3.Distance(pos, transform.position) < 10.7f)
            {
                c = new Ray(Personaje.transform.position, Personaje.transform.up);
                pos = c.GetPoint(15);
                transform.position = Vector3.MoveTowards(transform.position, pos,- 1 * 50 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            Physics.Raycast(transform.position, transform.up * -1, out hit, Mathf.Infinity);
            Vector3 obj = new Vector3(hit.point.x, Y, hit.point.z);
            GameObject inst = Instantiate(AguaPrefab, obj, AguaPrefab.transform.rotation);
            Destroy(inst, 9f);

            var lookPos = (Personaje.transform.position - inst.transform.position).normalized;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            inst.transform.rotation = Quaternion.Slerp(inst.transform.rotation, rotation, Mathf.Infinity);
            HieloSound.Play();
            yield return new WaitForSeconds(1.9f);
        }
        yield return new WaitForSeconds(3f);

        estado = NuevoEstado(0);
        life.Vida -= 15;
        InicieCorrutinaAgua = false;
        Mori();

    }
    IEnumerator AtaqueRoca()
    {
        List<GameObject> RocasPrefab = new List<GameObject>();
        RocasPrefab.Add(Roca1);
        RocasPrefab.Add(Roca2);
        RocasPrefab.Add(Roca3);
        RocasPrefab.Add(Roca1);
        RocasPrefab.Add(Roca2);
        RocasPrefab.Add(Roca3);

        for (int repeticiones = 0; repeticiones < 4; repeticiones++)
        {
            List<GameObject> Rocas = new List<GameObject>();
            foreach (GameObject prefab in RocasPrefab)
            {
                Ray ray = new Ray(Personaje.transform.position, Vector3.up);

                Vector3 pos = ray.GetPoint(30);
                pos = new Vector3(pos.x + Random.Range(-2, 2) * 1.4f, pos.y, pos.z + Random.Range(-2, 2) * 2.3f);
                Destroy(Instantiate(prefab, pos, Quaternion.identity), 6f);
                yield return new WaitForSeconds(.8f);

            }
            yield return new WaitForSeconds(1f);

        }
        yield return new WaitForSeconds(3f);

        estado = NuevoEstado(1);
        life.Vida -= 15;
        InicieCorrutinaRoca = false;
        Mori();

    }
    IEnumerator AtaqueFuego()
    {
        for (int i = 0; i < 4; i++)
        {

            //Physics.Raycast(transform.position, transform.up * -1, out hit, Mathf.Infinity, Pmask);
            Vector3 pos = new Vector3(Personaje.transform.position.x + 1.4f, Y, Personaje.transform.position.z - 2);
            vibracion.StartCoroutine(vibracion.Shake(.25f, .9f));
            PreTerremoto.Play();
            yield return new WaitForSeconds(0.40f);
            FuegoSound.Play();
            PreTerremoto.Stop();
            GameObject c = Instantiate(FuegoParticula, pos, FuegoParticula.transform.rotation);
            Vector3 size = new Vector3(12, 8, 13) / 2;
            Destroy(c, 4f);
            bool Golpie = false;
            Collider[] col = Physics.OverlapBox(pos, size);
            foreach (Collider obj in col)
            {

                if (obj.CompareTag("Personaje"))
                {
                    for (int b = 0; b < 30; b++)
                    {
                        if (!Golpie)
                        {
                            EstadisticasDePersonaje EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
                            EstadisticasDePersonaje.RecibirDaño(15f);
                            Golpie = true;
                        }
                        Personaje.GetComponent<CharacterController>().Move(Vector3.up * Time.deltaTime * 35);

                        yield return new WaitForEndOfFrame();
                    }

                }

            }
            yield return new WaitForSeconds(2f);

        }
        yield return new WaitForSeconds(3f);

        estado = NuevoEstado(2);
        InicieCorrutinaFuego = false;

        life.Vida -= 15;
        print("1: " + life.Vida);
        Mori();
        print("2: " + life.Vida);


    }
    IEnumerator AtaqueViento()
    {
        for (int indice = 0; indice < 4; indice++)
        {
            Ray c = new Ray(Personaje.transform.position, Personaje.transform.up);
            Vector3 pos = c.GetPoint(15);
            Debug.DrawRay(c.origin, c.direction, Color.red);
            while (Vector3.Distance(pos, transform.position) > 3)
            {
                c = new Ray(Personaje.transform.position, Personaje.transform.up);
                pos = c.GetPoint(15);
                transform.position = Vector3.MoveTowards(transform.position, pos, 80 * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 6; i++)
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
                            EstadisticasDePersonaje.RecibirDaño(12f);
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
        yield return new WaitForSeconds(3f);

        InicieCorrutina = false;
        estado = NuevoEstado(3);
        life.Vida -= 15;
        Mori();
    }


    void OrbitAroundPlayer()
    {
        transform.RotateAround(Personaje.transform.position, Vector3.up, 20 * Time.deltaTime);
        transform.LookAt(Personaje.transform.position);
    }
}
