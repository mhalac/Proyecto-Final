using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisilViento : MonoBehaviour
{
    public Transform TargetDeMisil;
    public Rigidbody CuerpoCohete;

    public float Giro;
    public float VelocidadCohete;

    void Start()
    {
        CuerpoCohete = GetComponent<Rigidbody>();
        TargetDeMisil = GameObject.FindGameObjectWithTag("Enemigo").transform;
    }
    void Update()
    {
        if(TargetDeMisil == null)
        {
            Destroy(gameObject);
        }
        else
        {
            CuerpoCohete.velocity = transform.forward * VelocidadCohete;
            var RotacionCohete = Quaternion.LookRotation(TargetDeMisil.position - transform.position);
            CuerpoCohete.MoveRotation(Quaternion.RotateTowards(transform.rotation , RotacionCohete , Giro));

            if(Vector3.Distance(transform.position , TargetDeMisil.position) < 3)
            {
                TargetDeMisil.GetComponent<LifeManager>().RecibirDamage(4);
                Destroy(gameObject);
            }
        }
    }
}
