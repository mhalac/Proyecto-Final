using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{
    public PostProcessVolume PPCamara;
    public PostProcessProfile Lobby;
    public PostProcessProfile SubNivelFuego1;
    public PostProcessProfile SubNivelFuego2;
    public PostProcessProfile SubNivelFuego3;
    public PostProcessProfile SubNivelFuego4;

    public PostProcessProfile SubNivelTierra1;
    public PostProcessProfile SubNivelTierra2;
    public PostProcessProfile SubNivelTierra3;
    public PostProcessProfile SubNivelTierra4;

    public PostProcessProfile SubNivelViento1;
    public PostProcessProfile SubNivelViento2;
    public PostProcessProfile SubNivelViento3;
    public PostProcessProfile SubNivelViento4;

    public PostProcessProfile SubNivelAgua1;
    public PostProcessProfile SubNivelAgua2;
    public PostProcessProfile SubNivelAgua3;
    public PostProcessProfile SubNivelAgua4;
    public PostProcessProfile LvlFinal;

    public void CambiarElPPL(string NombreDeEscena)
    {
        switch(NombreDeEscena)
        {
            case "Lobby":
            PPCamara.profile = Lobby;
            break;

            case "SubNivelFuego1":
            PPCamara.profile = SubNivelFuego1;
            break;

            case "SubNivelFuego2":
            PPCamara.profile = SubNivelFuego2;
            break;

            case "SubNivelFuego3":
            PPCamara.profile = SubNivelFuego3;
            break;

            case "SubNivelFuego4":
            PPCamara.profile = SubNivelFuego4;
            break;

            case "SubNivelTierra1":
            PPCamara.profile = SubNivelTierra1;
            break;

            case "SubNivelTierra2":
            PPCamara.profile = SubNivelTierra2;
            break;

            case "SubNivelTierra3":
            PPCamara.profile = SubNivelTierra3;
            break;

            case "SubNivelTierra4":
            PPCamara.profile = SubNivelTierra4;
            break;

            case "SubNivelViento1":
            PPCamara.profile = SubNivelViento1;
            break;

            case "SubNivelViento2":
            PPCamara.profile = SubNivelViento2;
            break;

            case "SubNivelViento3":
            PPCamara.profile = SubNivelViento3;
            break;

            case "SubNivelViento4":
            PPCamara.profile = SubNivelViento4;
            break;

            case "SubNivelAgua1":
            PPCamara.profile = SubNivelAgua1;
            break;

            case "SubNivelAgua2":
            PPCamara.profile = SubNivelAgua2;
            break;

            case "SubNivelAgua3":
            PPCamara.profile = SubNivelAgua3;
            break;

            case "SubNivelAgua4":
            PPCamara.profile = SubNivelAgua4;
            break;

            case "LvlFinal":
            PPCamara.profile = LvlFinal;
            break;

            default:
            Debug.Log("Se jodio el Post Processing");
            break;
        }
        
    }
}
