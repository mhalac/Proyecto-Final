using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaFPS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log(1.0f / Time.deltaTime);
	}
}
