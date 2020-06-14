using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilBase : MonoBehaviour
{
    public Transform PuntoDisparo;

    private Ray DisparoPosicion;

    private int PMask;
    private Vector3 IrPosicion;
    private float velocidad;
    private bool Selecionado = false;

    // Use this for initialization
    void Start()
    {
        PMask = LayerMask.NameToLayer("Personaje");
        Quaternion lookRotation = Quaternion.LookRotation(IrPosicion);
        transform.rotation = Quaternion.Slerp(lookRotation, transform.rotation, Time.deltaTime);
		Destroy(gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        Lanzar(IrPosicion, velocidad);

    }
    private void Seleccionar()
    {
        DisparoPosicion = new Ray(PuntoDisparo.position, IrPosicion);
        IrPosicion = DisparoPosicion.GetPoint(1000);
        Selecionado = true;
    }
    public void Enemigo(Vector3 direccion)
    {
        Debug.DrawRay(PuntoDisparo.position, IrPosicion, Color.green);
    }
    private bool Colisiono()
    {
        Collider[] obj = Physics.OverlapSphere(transform.position, 1f);
        for (int i = 0; obj.Length > i; i++)
        {
            if (obj[i].gameObject.layer == PMask)
            {
                return true;
            }
        }

        return false;
    }

    public void Lanzar(Vector3 Objetivo, float speed)
    {

        Debug.DrawRay(transform.position, Objetivo, Color.magenta);
        Enemigo(Objetivo);
        if (!Selecionado)
        {
            velocidad = speed;
            IrPosicion = Objetivo;
            Seleccionar();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Objetivo, speed * Time.deltaTime);
        }
        if (Colisiono())
        {
            Destroy(gameObject);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(IrPosicion, 3f);

    }
}
