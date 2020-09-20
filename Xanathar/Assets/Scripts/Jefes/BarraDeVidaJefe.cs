using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaJefe : MonoBehaviour
{

    //Cuando se inicie el script el valor actual de vida debe ser igual al valor maximo de vida
    public float ValorDeVidaMaxima;
    //a ValorDeVidaActual metele la vida del jefe
    public float ValorDeVidaActual;
    private float RellenoDeVida;
    public Image ContenidoBarraDeVida;

    // Use this for initialization
    float BuscarBarra()
    {
        //f (GameObject.FindGameObjectWithTag("JefeFuego") != null)
            //return FindObjectOfType<LifeManager>().Vida;
        //e/]\lse if (GameObject.Find("Jefe Tierra") != null)
            return GameObject.Find("Jefe Tierra").GetComponent<LifeManager>().Vida ;
        //else
            //Debug.LogError("FALTA ASIGNAR TAG DEL JEFE");

       // return 0;

    }
    void Start()
    {
        //Igualas la vida maxzima a la vida actual
        //ValorDeVidaMaxima = BuscarBarra();
        //ValorDeVidaMaxima = 20;
    }

    // Update is called once per frame
    void Update()
    {
        ManejadorDeVidaJefe(ValorDeVidaMaxima);
    }

    public void ManejadorDeVidaJefe(float VidaMaximaDelJefe)
    {
        //ValorDeVidaActual = BuscarBarra();
        //ValorDeVidaActual = FindObjectOfType<LifeManager>().Vida;
        RellenoDeVida = ValorDeVidaActual / VidaMaximaDelJefe;
        ContenidoBarraDeVida.fillAmount = RellenoDeVida;

        if (ValorDeVidaActual <= 0 && FindObjectOfType<LifeManager>() == null)
        {
            Debug.Log("El jefe se murio");
        }
    }
}
