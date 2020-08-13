using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionIconos : MonoBehaviour {

	public Image Habilidad;
	public float CoolDown = 5f;
	bool EstaEnCooldown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log(EstaEnCooldown);
		if(Input.GetKeyDown(KeyCode.N) && EstaEnCooldown == false && Habilidad.enabled == true)
		{
			StartCoroutine(CoolDownDeHabilidades());
		}
	}

	IEnumerator CoolDownDeHabilidades()
	{
		EstaEnCooldown = true;
		Habilidad.fillAmount = 0;

		while (Habilidad.fillAmount <= 1)
		{
			yield return new WaitForEndOfFrame();
			Habilidad.fillAmount += 1 / CoolDown * Time.deltaTime;

			if(Habilidad.fillAmount >= 1)
			{
				Habilidad.fillAmount = 1f;
				EstaEnCooldown = false;
				Debug.Log("Corrutina Completada");
				yield break;
			}
		}
	}
}
