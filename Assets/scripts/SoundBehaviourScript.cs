using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehaviourScript : MonoBehaviour
{
    public List<AudioClip> audios;
    public AudioSource audio_bala, audio_nave, audio_asteroide, audio_enemy, audio_background;

    // Start is called before the first frame update
    void Start()
    {
        if (audio_background == null || !audio_background.isPlaying)
        {
            audio_background = Camera.main.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StopBackgroundMusic()
    {
        if (audio_background != null && audio_background.isPlaying)
        {
            audio_background.Stop();
        }
    }

    public void PlayAudioBala(int index)
    {
        audio_bala.PlayOneShot(audios[index]);
        audio_bala.Play();
    }

    public void PlayAudioNave(int index)
    {
        audio_nave.PlayOneShot(audios[index]);
        audio_nave.Play();
    }

    public void PlayAudioAsteroid(int index)
    {
        audio_asteroide.PlayOneShot(audios[index]);
        audio_asteroide.Play();
    }

    public void PlayAudioEnemy(int index)
    {
        audio_enemy.PlayOneShot(audios[index]);
        audio_enemy.Play();
    }
}
