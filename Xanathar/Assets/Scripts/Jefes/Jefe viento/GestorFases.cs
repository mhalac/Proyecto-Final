using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorFases : MonoBehaviour
{
    public JefeViento.Estados EstadoSeleccionado;

    public List<DuracionOldeadas> OleadaGetter = new List<DuracionOldeadas>();
    public int ContadorOldeadas;
	private int NumeroPrevio;
    public float CDinicial;

    public float CDActual;

    // Use this for initialization
    void Start()
    {
        CDinicial = OleadaGetter[0].Duracion;
        CDActual = CDinicial;
        Random.InitState(System.DateTime.Now.Millisecond);

    }

    // Update is called once per frame
    void Update()
    {

        if (CDActual < Mathf.Epsilon)
        {
            if (ContadorOldeadas == 2)
            {
                GetComponent<JefeViento>().estado = (JefeViento.Estados)1;
                ContadorOldeadas = 0;
				CDinicial = OleadaGetter[1].Duracion;
                CDActual = CDinicial;
            }
            else
            {
                int RandomState = Random.Range(2, 5);
				while(RandomState == NumeroPrevio)
					RandomState = Random.Range(2, 5);
                print(" Numero: " + RandomState + " Representa: " + (JefeViento.Estados)RandomState);
				NumeroPrevio = RandomState;
                EstadoSeleccionado = (JefeViento.Estados)RandomState;
                CDinicial = OleadaGetter[RandomState].Duracion;
                CDActual = CDinicial;
                GetComponent<JefeViento>().estado = EstadoSeleccionado;
                ContadorOldeadas++;
            }

        }
        else
            CDActual -= Time.deltaTime;



    }
    private void CambiarEstado()
    {
        GetComponent<JefeViento>().estado = EstadoSeleccionado;
    }
    private void CambiarEstado(int i)
    {
        EstadoSeleccionado = (JefeViento.Estados)i;
        GetComponent<JefeViento>().estado = EstadoSeleccionado;
    }
    [System.Serializable]

    public class DuracionOldeadas
    {
        public JefeViento.Estados Estado;
        public float Duracion;

    }
}
