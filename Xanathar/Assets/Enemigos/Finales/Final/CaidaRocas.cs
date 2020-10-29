using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaRocas : MonoBehaviour
{
    private Vector3 PosicionHitbox;
    public Vector3 size;
    public Vector3 hit;
    public bool YaGolpie;

    public AudioSource Strike;

    private Ray rasho;
    // Start is called before the first frame update
    void Start()
    {
        //Physics.Raycast(transform.position, transform.up * -1, out hit, Mathf.Infinity);
        rasho = new Ray(transform.position,transform.up * - 1);
        hit = rasho.GetPoint(9000);
    }

    // Update is called once per frame
    void Update()
    {
        PosicionHitbox = transform.position;
        if (Vector3.Distance(hit, transform.position) > 1f)
        {
            Collider[] cols = Physics.OverlapBox(PosicionHitbox, size / 3);
            foreach (Collider c in cols)
            {
                if (c.CompareTag("Personaje") && !YaGolpie)
                {
                    EstadisticasDePersonaje f = FindObjectOfType<EstadisticasDePersonaje>();
                    f.RecibirDaño(7);
                    YaGolpie = true;
                    StartCoroutine(Knockback());
                }
            }
            transform.Translate(0, -1.9f, 0);

        }
        else
            Strike.Play();
    }
    IEnumerator Knockback()
    {
        GameObject Personaje = GameObject.FindGameObjectWithTag("Personaje");
        for (int i = 0; i < 30; i++)
        {
            Personaje.GetComponent<CharacterController>().Move(Personaje.transform.forward * -1 * Time.deltaTime * 35);
            yield return new WaitForEndOfFrame();
        }


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position - transform.forward * 3.5f, size);
    }
}
