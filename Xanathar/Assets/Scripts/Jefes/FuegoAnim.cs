using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuegoAnim : MonoBehaviour
{
    public AudioSource zombie;
    // Use this for initialization
    private FuegoJefe Padre;
    void Start()
    {
        Padre = GetComponentInParent<FuegoJefe>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private AudioSource[] allAudioSources;

    public void TermineMorir()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
            
        }
        //zombie.Play();
        Padre.DropearItems();
        Destroy(gameObject);
    }
    public void EmpezeAMorir()
    {
        Animator n = GetComponent<Animator>();
        n.SetBool("Morir", false);
        Padre.enabled = false;
        NavMeshAgent a = GetComponentInParent<NavMeshAgent>();
        a.enabled = false;
    }
    public void AplasteElPiso()
    {
        Padre.AplasteElPiso();
    }
    public void TermineAnimacion()
    {
        Padre.TermineAnimacion();
    }
    public void TermineAtacar()
    {
        Padre.TermineDeAtacar();
    }
}
