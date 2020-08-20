using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropearItems : MonoBehaviour {

	// Use this for initialization
	public GameObject[] ItemsActivos;
	public void DropearItemsEnemigo()
    {
		EstadisticasDePersonaje d = FindObjectOfType<EstadisticasDePersonaje>();
		
        int i = -3;
        foreach(GameObject c in ItemsActivos)
        {
            Vector3 pos = new Vector3(d.gameObject.transform.position.x,d.gameObject.transform.position.y + 2,d.gameObject.transform.position.z + i);
            Instantiate(c,pos,Quaternion.identity);
            i = 3;
        }
		Destroy(this.gameObject,2f);
	}

	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
