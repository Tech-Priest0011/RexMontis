using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonEnvironnement : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    [Range(0.1f,0.5f)]
    public float volumeChangeMultiplier = 0.2f;
    [Range(0.1f,0.5f)]
    public float pitchChangeMultiplier = 0.2f;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource> ();
        Son();
    }

    // Update is called once per frame
    void Update()
    {
     
  
      
    }
    void Son()
    {
        InvokeRepeating("JouerSon",5.0f, 10.0f);
    }

       void JouerSon()
    {
            source.clip = sounds[Random.Range(0, sounds.Length)];
            source.volume = Random.Range(0.3f - volumeChangeMultiplier, 0.3f);
            source.pitch = Random.Range(0.3f - pitchChangeMultiplier, 0.3f + pitchChangeMultiplier);
            source.PlayOneShot(source.clip);
    }

}
