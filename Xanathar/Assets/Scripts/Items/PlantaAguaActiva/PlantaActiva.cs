using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaActiva : MonoBehaviour
{
    public float AreaDeVision;
    public float Damage;
    public GameObject EnemigoVisto;
    public Transform PuntoDeVision;
    public Transform PuntoDeAtaque;
    public Animator Animador;
    public GameObject PrefabBala;


    // Start is called before the first frame update
    void Start()
    {
        Animador.SetBool("Idle" , true);
        Animador.SetBool("Atacando" , false);
    }

    // Update is called once per frame
    void Update()
    {
        if(BuscarEnemigos())
        {
            Vector3 Direccion = (EnemigoVisto.transform.position - PuntoDeVision.transform.position).normalized;
            Direccion.y = 0;
            Quaternion Mirar = Quaternion.LookRotation(Direccion);

            transform.rotation = Quaternion.Lerp(transform.rotation , Mirar , Time.fixedDeltaTime * 2);

            Animador.SetBool("Idle" , false);
            Animador.SetBool("Atacando" , true);
        }
        else
        {
            Animador.SetBool("Idle" , true);
            Animador.SetBool("Atacando" , false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PuntoDeVision.position, AreaDeVision);
    }

    public bool BuscarEnemigos()
    {
        Collider [] Obj = Physics.OverlapSphere(PuntoDeVision.position , AreaDeVision);

        for(int i = 0; i < Obj.Length; i++)
        {
            if(Obj[i].gameObject.tag == "Enemigo")
            {
                if(EnemigoVisto == null)
                {
                    EnemigoVisto = Obj[i].gameObject;
                }

                return true;
            }
        }

        return false;
    }

    public void LanzarProyectil()
    {
        GameObject Bala = Instantiate(PrefabBala , PuntoDeAtaque.position , Quaternion.identity);

        Bala.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        Destroy(Bala , 4);
    }
}
