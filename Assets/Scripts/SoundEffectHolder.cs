using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectHolder : MonoBehaviour
{
    public static SoundEffectHolder instance;

    public AudioSource Music;
    public AudioSource SoundEffect;
    public AudioSource Environment;
    public AudioSource Narration;
    public AudioSource ProximityAlert;
    public AudioSource Footsteps;
    public AudioSource EnemyFootsteps;

    [SerializeField] public AudioClip BOOP_SOUND;
    [SerializeField] public AudioClip LEVEL_MUSIC;

    // current system for these audio sources is messy. can't tell where they're being interacted with
    // would be better to have specific managers for each or something
    // idk

    // Update is called once per frame
    void Awake()
    {
        instance = GetComponent<SoundEffectHolder>();
        Music.clip = LEVEL_MUSIC;
        Music.Play();
        ProximityAlert.loop = true; // need to add proximity alerts to the menu

        Footsteps.loop = true;
        EnemyFootsteps.loop = true;
        Environment.loop = true;

        Footsteps.spatialBlend = 0;
        EnemyFootsteps.spatialBlend = 0;
        Environment.spatialBlend = 0;
    }

    public void PlayClip(AudioSource audioSource, AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PauseAllNonNarration() {
        Music.Pause();
        SoundEffect.Pause();
        ProximityAlert.Pause();
        Footsteps.Pause();
        EnemyFootsteps.Pause();
    }
    public void UnpauseAllNonNarration() {
        Music.UnPause();
        SoundEffect.UnPause();
        ProximityAlert.UnPause();
        Footsteps.UnPause();
        EnemyFootsteps.UnPause();
    }
}
