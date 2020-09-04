using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibracionCamara : MonoBehaviour
{
    
    public IEnumerator Shake(float duration, float magnitud)
    {
        Vector3 PosicionOriginal = transform.localPosition;
        

        float tiempoPasado = 0.0f;
        while (tiempoPasado < duration)
        {
            float x = Random.Range(-1, 1) * magnitud;
            float y = Random.Range(-1, 1) * magnitud;
            transform.localPosition = new Vector3(x, y, PosicionOriginal.z);
            tiempoPasado += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = PosicionOriginal;
        
    }
}
