using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaSonidos : MonoBehaviour {

    public static SistemaSonidos instancia;
    private AudioSource miAudio;

    public AudioClip AudioFinCopia;
    public AudioClip AudioError;

    public void Awake()
    {
        instancia = this;
        miAudio = GetComponent<AudioSource>();
    }

    public void PlayFinalizoCopia()
    {
        miAudio.clip = AudioFinCopia;
        miAudio.Play();
    }
    public void PlayError()
    {
        miAudio.clip = AudioError;
        miAudio.Play();
    }
}
