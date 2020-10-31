using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiadorDeNivel : MonoBehaviour
{
    EstadisticasDePersonaje Estadisticas;
    public bool PermitirCambios = false;
    public Animator AnimacionDeCambioDeNivel;
    public BuscadorDePos BuscadorDePos;
    AsyncOperation asyncOperation;

    void Start()
    {
        Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
    }

    public void IniciadorDeCambio()
    {
        if (PermitirCambios == false)
        {
            PermitirCambios = true;
            AnimacionDeCambioDeNivel.SetTrigger("Desaparecer");
            ManejadorDeEscenas.ActivadorDeCambio = true;
            StartCoroutine(InmortalEnLaPuerta());
        }
    }

    IEnumerator CargarEscena()
    {
        asyncOperation = SceneManager.LoadSceneAsync(ManejadorDeEscenas.NombreDeEscena, LoadSceneMode.Additive);

        asyncOperation.allowSceneActivation = true;
        yield return asyncOperation;
        StartCoroutine(BorrarEscena());
    }

    IEnumerator BorrarEscena()
    {
        string EscenaActual = SceneManager.GetActiveScene().name;
        asyncOperation = SceneManager.UnloadSceneAsync(EscenaActual);

        asyncOperation.allowSceneActivation = true;
        while (!asyncOperation.isDone)
        {
            AnimacionDeCambioDeNivel.SetTrigger("Aparecer");
            ManejadorDeEscenas.ActivadorDeCambio = false;
            yield return asyncOperation;


            BuscadorDePos.ManejadorDePos();
            PermitirCambios = false;
            MuerteEnVacios.Caiste = false;

            PostProcessManager CambiarPostPros = FindObjectOfType<PostProcessManager>();
            CambiarPostPros.CambiarElPPL(SceneManager.GetActiveScene().name);

            if (SceneManager.GetActiveScene().name == "Lobby")
            {
                if (ControlarPuertasYJefes.JefeEliminado == true)
                {
                    EstadisticasDePersonaje estadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();

                    if (estadisticasDePersonaje.MurioDespuesDeMatarJefe == true)
                    {
                        AgarradorDeItems agarradorDeItems = FindObjectOfType<AgarradorDeItems>();
                        DropearItemsEnSpawn.TirarItemsEnSpwn(agarradorDeItems.CopiaDeObjetos);
                        ControlarPuertasYJefes.JefeEliminado = false;
                        estadisticasDePersonaje.MurioDespuesDeMatarJefe = false;

                    }
                    else
                    {
                        ControlarPuertasYJefes.JefeEliminado = false;
                    }
                }


                if (ControlarPuertasYJefes.JefeDeFuegoMuerto == true)
                {
                    GameObject PuertaDeFuego = GameObject.Find("EntradaNivelFuego");
                    GameObject.Find("FuegoLuz").GetComponent<Light>().range = 33;
                    PuertaDeFuego.SetActive(false);
                }

                if (ControlarPuertasYJefes.JefeDeTierraMuerto == true)
                {
                    GameObject PuertaDeTierra = GameObject.Find("EntradaNivelRoca");
                    GameObject.Find("RocaLuz").GetComponent<Light>().range = 33;

                    PuertaDeTierra.SetActive(false);
                }

                if (ControlarPuertasYJefes.JefeDeVientoMuero == true)
                {
                    GameObject PuertaDeViento = GameObject.Find("EntradaNivelViento");
                    PuertaDeViento.SetActive(false);
                    GameObject.Find("VientoLuz").GetComponent<Light>().range = 33;
                }

                if (ControlarPuertasYJefes.JefeDeAguaMuerto == true)
                {
                    GameObject PuertaDeAgua = GameObject.Find("EntradaNivelAgua");
                    GameObject.Find("AguaLuz").GetComponent<Light>().range = 33;

                    PuertaDeAgua.SetActive(false);
                }
                if (ControlarPuertasYJefes.JefeDeAguaMuerto && ControlarPuertasYJefes.JefeDeVientoMuero && ControlarPuertasYJefes.JefeDeTierraMuerto && ControlarPuertasYJefes.JefeDeFuegoMuerto)
                {
                    GameObject.Find("FinalLuz").GetComponent<Light>().range = 40;

                }
            }
        }
    }

    public void BuscadorDeFuncionParaGuardarItems()
    {
        ListasDeItems FuncionParaGuardar = FindObjectOfType<ListasDeItems>();
        FuncionParaGuardar.LlenarElConstructor();
    }

    public void BuscadorFuncionParaCargarItems()
    {
        ListasDeItems FuncionParaCargar = FindObjectOfType<ListasDeItems>();
        FuncionParaCargar.InstanciarLosObjetosDelConstructor();
    }

    public IEnumerator InmortalEnLaPuerta()
    {
        while( ManejadorDeEscenas.ActivadorDeCambio == true)
        {
            Estadisticas.Inmortalidad = true;
            yield return new WaitForEndOfFrame();
        }

        Estadisticas.Inmortalidad = false;
        yield return null;
    }
}
