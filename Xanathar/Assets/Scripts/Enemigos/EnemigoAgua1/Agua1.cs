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

    [Header("Parametros")]
    private GameObject Personaje;
    private Vector3 Destino;
    public Animator Animador;

    // Start is called before the first frame update
    void Start()
    {
        EstadoActual = Estados[0];

        Animador.SetBool("Idle" , true);

        /*
        Destino = Pos1.position;
        if(Destino == transform.position)
        {
            Debug.Log("Misma Pos");
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.N))
        {
            IrAposRandom();
        }
        */
        
        /*
        if(PuedoVer())
        {
            Debug.Log("Te puedo ver");
        }
        */
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

        PermitirVision = false;
        PermitirDisparo = false;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PuntoDeVision.position , AreaDeAtaque);
    }
}
