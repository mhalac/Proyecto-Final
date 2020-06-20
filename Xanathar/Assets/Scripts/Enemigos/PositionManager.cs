using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PositionManager : MonoBehaviour
{

    public List<Vector3> PosicionesOcupadas;
    public Vector3 destino;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    

    public bool EstaOcupado(Vector3 Posicion)
    {
        // Haces un raycast desde el lugar donde queres crear el lugar Idle y lo disparas a todos los idles creados
        
        foreach(Vector3 pos in PosicionesOcupadas)
        {
            float distancia = Vector3.Distance(pos,Posicion);
            if(distancia < 7f)
            {
                //print("Ocupado");
                return true;
            }
            
        }
        //print("No ocupado");
        return false;
    }
    public void Llegue(Vector3 DestinoOriginal)
    {
        if(PosicionesOcupadas.Contains(DestinoOriginal))
        {
            PosicionesOcupadas.Remove(DestinoOriginal);
        }
        
    }
    public Vector3 GenerarPosicionRandom(Vector3 posicionRandom, float AreaI, Vector3 Heredar)
    {
        //el I2 sirve como cortafuegos por si algo sale mal no explote el unity
        int i2 = 0;
        float RandomX = 0;
        float RandomZ = 0;
        bool seCreo = false;
        bool hayPared = false;
        //genera numero random entre tu posicion y el rango espesificado de idle
        while (!seCreo)
        {
            hayPared = false;
            print("Entre " + (posicionRandom.x - AreaI) + " Y "+ (posicionRandom.x + AreaI));
            RandomX = Random.Range(posicionRandom.x - AreaI, AreaI + posicionRandom.x);
            print("Genere: " +RandomX);
            RandomZ = Random.Range(posicionRandom.z - AreaI, AreaI + posicionRandom.z);
            destino = new Vector3(RandomX, Heredar.y, RandomZ);
            // revisas que el punto para ir no este en una pared
            i2++;
            if (i2 > 200)
            {
                Debug.LogError("ERROR GENERANDO IDLE ERROR SALIENDO");
                return Vector3.zero;
            }
            //CUIDADO CON MODIFICAR EL O.1F PUEDE CRASHEAR EL JUEGO
            Collider[] obj = Physics.OverlapSphere(destino, 0.1f);
            for (int i = 0; i < obj.Length; i++)
            {
                if (!EstaOcupado(destino) || obj[i].tag == "Entorno" || obj[i].tag == "Enemigo" )
                {
                    hayPared = true;
                }

            }
            if (!hayPared)
            {
                seCreo = true;
            }
        }
        PosicionesOcupadas.Add(destino);
        return destino;
    }
}
