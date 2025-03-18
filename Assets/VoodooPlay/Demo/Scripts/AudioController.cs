using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    private AudioSource audioSource;
    [Range(0.01f, 10f)]
    public float pitchRandomMultiplier = 1f;

    public AudioClip shoot;
    public AudioClip dropBomb;
    public AudioClip explode;
    public AudioClip duck;
    public AudioClip pickup;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayAudioFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Missing Audio clip");
            return;
        }
        audioSource.pitch = 1f;
        if (pitchRandomMultiplier != 1)
        {
            if (Random.value < .5)
                audioSource.pitch *= Random.Range(1 / pitchRandomMultiplier, 1);
            else
                audioSource.pitch *= Random.Range(1, pitchRandomMultiplier);
        }
        audioSource.PlayOneShot(clip);
    }

    public void ShootAFX() {
        PlayAudioFX(shoot);
    }

    public void DuckAFX()
    {
        PlayAudioFX(duck);
    }
    public void ExplodeAFX()
    {
        PlayAudioFX(explode);
    }

    public void DropAFX()
    {
        PlayAudioFX(dropBomb);
    }

    public void PickupAFX()
    {
        PlayAudioFX(pickup);
    }
}
