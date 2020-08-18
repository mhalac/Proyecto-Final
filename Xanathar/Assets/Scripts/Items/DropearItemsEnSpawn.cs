using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropearItemsEnSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void TirarItemsEnSpwn(GameObject [] ArrayDeObjetos)
	{
		float RangoDeInstancia = 10;
		float RadioDeObjetos = 1;
		
		GameObject PosParaInstanciar = GameObject.FindGameObjectWithTag("PosicionClave");

        for (int i = 0; i < ArrayDeObjetos.Length; i++)
        {
            if (ArrayDeObjetos[i] == null)
            {
                break;
            }

            Vector3 PosicionObjeto = Vector3.zero;
            bool PosicionValida = false;
            int Seguro = 0;

            while (PosicionValida == false)
            {
                if (Seguro >= 50)
                {
                    //Debug.Log("Se rompio");
                    break;
                }

                Seguro += 1;
                PosicionObjeto = new Vector3(Random.Range(-RangoDeInstancia, RangoDeInstancia) + PosParaInstanciar.transform.position.x, PosParaInstanciar.transform.position.y, Random.Range(-RangoDeInstancia, RangoDeInstancia) + PosParaInstanciar.transform.position.z);
                PosicionValida = true;
                Collider[] Colisiones = Physics.OverlapSphere(PosicionObjeto, RadioDeObjetos);

                foreach (Collider col in Colisiones)
                {
                    if (col.tag == "Items" || col.tag == "Personaje" || col.tag == "Piso" || col.tag == "Entorno")
                    {
                        PosicionValida = false;
                    }
                }
            }

            if (PosicionValida == true)
            {                
                Instantiate(ArrayDeObjetos[i], PosicionObjeto, Quaternion.identity);
            }
        }
	}
}
