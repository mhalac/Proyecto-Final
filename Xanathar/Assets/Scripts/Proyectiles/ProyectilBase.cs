using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilBase : MonoBehaviour
{
    private float DamageFinal;
    public Transform PuntoDisparo;
    private Ray DisparoPosicion;
    private bool Hit = false;
    private Vector3 IrPosicion;
    private float velocidad;
    private bool Selecionado = false;

    // Use this for initialization
    void Start()
    {
        Quaternion lookRotation = Quaternion.LookRotation(IrPosicion);
        transform.rotation = Quaternion.Slerp(lookRotation, transform.rotation, Time.deltaTime);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Lanzar(IrPosicion, velocidad, DamageFinal);

    }


    // casteas un rayo al jugador y despues lo amplificas para sacar el punto mas lejano en su direccion 
    private void Seleccionar()
    {
        Quaternion lookRotation = Quaternion.LookRotation(IrPosicion);
        transform.LookAt(IrPosicion);
        DisparoPosicion = new Ray(PuntoDisparo.position, IrPosicion);
        IrPosicion = DisparoPosicion.GetPoint(1000);
        Selecionado = true;
    }




    //funcion llamada recien se instancia para que la bala tenga los paramentros de la posicion del jugador y la velocidad heredada del enemigo
    public void Lanzar(Vector3 Objetivo, float speed, float Damage)
    {
        //El if esta para que lo seleccione solo una vez, en la posicion que le dieron en un principio
        //print(Colisiono());
        DamageFinal = Damage;
        Debug.DrawLine(transform.position, IrPosicion, Color.magenta);
        if (!Selecionado)
        {
            velocidad = speed;
            IrPosicion = Objetivo;
            Seleccionar();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, IrPosicion, speed * Time.deltaTime);
            RaycastHit Golpe;
            var direccion = (IrPosicion - transform.position).normalized;
            
            if (Physics.Raycast(transform.position, direccion, out Golpe, Mathf.Infinity))
            {
                var direccionGolpe = (Golpe.point - transform.position).normalized;
                //print("Ir a: "+ IrPosicion + " - current pos:" + transform.position + " = " + direccionGolpo);
                    
                Debug.DrawRay(PuntoDisparo.position, direccionGolpe * Golpe.distance, Color.blue);
                if (Vector3.Distance(Golpe.point, PuntoDisparo.position) < speed * Time.deltaTime)
                {
                    if (Golpe.transform.gameObject.tag == "Personaje")
                    {
                        Golpe.transform.gameObject.GetComponent<EstadisticasDePersonaje>().RecibirDaño(DamageFinal);
                        print("Golipe y hice " + DamageFinal);
                        DestroyImmediate(gameObject);
                    }
                    else
                        DestroyImmediate(gameObject);
                }
                   
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
       Destroy(gameObject);

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
       // Gizmos.DrawSphere(Golpe.transform.position, 20f);

    }
}
