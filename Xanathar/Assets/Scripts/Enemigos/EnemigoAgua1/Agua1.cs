using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua1 : MonoBehaviour
{
    [Header("Transforms Seleccionables")]
    public Transform PuntoDeVision;
    public Transform PuntoDeAtaque;
    public Transform [] ListaDePos;

    [Header("Variables")]
    public float AreaDeAtaque;
    public string EstadoActual;
    public string [] Estados = {"Idle" , "Attacking" , "Warping"};
    public bool PermitirDisparo = false;
    public bool PermitirVision = false;
    public float VelocidadBala;
    public float Damage;

    [Header("Parametros")]
    private GameObject Personaje;
    private Vector3 Destino;
    public Animator Animador;
    public GameObject PrefabBala;

    // Start is called before the first frame update
    void Start()
    {
        EstadoActual = Estados[0];

        Animador.SetBool("Idle" , true);
    }

    // Update is called once per frame
    void Update()
    {
        if(PuedoVer() && PermitirVision == false)
        {
            Apuntar();

            if(PermitirDisparo == false && PermitirVision == false)
            {
                InciarDisparo();
            }
        }
    }

    public void PonerPosRandomYSumergirse()
    {
        Vector3 PosDelEnemigo = transform.position;

        Vector3 NuevaPos = GenerarPosRandom();

        while(PosDelEnemigo == NuevaPos)
        {
            NuevaPos = GenerarPosRandom();
        }

        Destino = NuevaPos;

        if(PermitirDisparo == true)
        {
            Animador.SetBool("Atacando" , false);
            Animador.SetBool("Sumergirse" , true);

            PermitirVision = true;
            EstadoActual = Estados[2];
        }
        else
        {
            Animador.SetBool("Idle" , false);
            Animador.SetBool("Sumergirse" , true);

            PermitirVision = true;
            EstadoActual = Estados[2];
        }
    }

    public Vector3 GenerarPosRandom()
    {
        int Index = Random.Range(0 , 7);
        Vector3 PosRandom = Vector3.zero;

        switch(Index)
        {
            case 0:
            PosRandom = ListaDePos[0].position;
            break;

            case 1:
            PosRandom = ListaDePos[1].position;
            break;

            case 2:
            PosRandom = ListaDePos[2].position;
            break;

            case 3:
            PosRandom = ListaDePos[3].position;
            break;

            case 4:
            PosRandom = ListaDePos[4].position;
            break;

            case 5:
            PosRandom = ListaDePos[5].position;
            break;

            case 6:
            PosRandom = ListaDePos[6].position;
            break;

            default:
            Debug.Log("Pos Rancia");
            break;
        }

        return PosRandom;
    }

    public void Tepearse()
    {
        transform.position = Destino;

        Animador.SetBool("Sumergirse" , false);
        Animador.SetBool("Aparecer" , true);
    }

    public void VolverAlIdle()
    {
        Animador.SetBool("Idle" , true);
        Animador.SetBool("Aparecer" , false);

        StartCoroutine(Esperar());
    }

    public bool PuedoVer()
    {
        Collider [] Obj = Physics.OverlapSphere(PuntoDeVision.position , AreaDeAtaque);

        for (int i = 0; i < Obj.Length; i++)
        {
            if(Obj[i].gameObject.tag == "Personaje")
            {
                Personaje = Obj[i].gameObject;
                return true;
            }
        }

        return false;
    }

    public void Apuntar()
    {
        Vector3 Direccion = (Personaje.transform.position - transform.position).normalized;

        Quaternion Mirar = Quaternion.LookRotation(Direccion);
        transform.rotation = Quaternion.Lerp(transform.rotation , Mirar , Time.fixedDeltaTime * 2);
    }

    public void Disparar()
    {
        GameObject Bala = Instantiate(PrefabBala , PuntoDeAtaque.position , Quaternion.identity);

        Bala.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        Destroy(Bala , 6);
    }

    public void InciarDisparo()
    {
        PermitirDisparo = true;

        Animador.SetBool("Idle" , false);
        Animador.SetBool("Atacando" , true);
    }

    public void TerminarDisparo()
    {
        Animador.SetBool("Atacando" , false);
        Animador.SetBool("Sumergirse" , true);

        PonerPosRandomYSumergirse();
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds (0.5f);

        PermitirVision = false;
        PermitirDisparo = false;

        EstadoActual = Estados[0];
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PuntoDeVision.position , AreaDeAtaque);
    }
}
