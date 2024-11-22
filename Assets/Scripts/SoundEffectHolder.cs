using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectHolder : MonoBehaviour
{
    public static SoundEffectHolder instance;

    public AudioSource Music;
    public AudioSource SoundEffect;
    public AudioSource Narration;

    [SerializeField] public AudioClip BOOP_SOUND;
    [SerializeField] public AudioClip LEVEL_MUSIC;

    // Update is called once per frame
    void Awake()
    {
        instance = GetComponent<SoundEffectHolder>();
        Music.clip = LEVEL_MUSIC;
        Music.Play();

    }

    public void PauseMusic() {
        Music.Pause();
    }
    public void UnpauseMusic() {
        Music.Play();
    }

    public void PlayMusic(AudioClip music) {
        Music.clip = music;
        Music.Play();
    }
    public void PlaySoundEffect(AudioClip soundEffect) {
        SoundEffect.clip = soundEffect;
        SoundEffect.Play();
    }
    public void PlayNarration(AudioClip narration) {
        Debug.Log("NARRATION PLAYED");
        Narration.clip = narration;
        Narration.Play();
    }
}
