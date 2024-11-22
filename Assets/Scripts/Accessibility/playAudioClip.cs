using UnityEngine;

public class PlayAudioClip : IOptionObserver
{
    [SerializeField] private AudioClip audioClip;
    private bool doPlayAudio;

    void Start() {
        doPlayAudio = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.MUSIC_MUTED); // TODO update to narration
    }

    public void PlayAudio() {
        Debug.Log("Narration played!");
        if (doPlayAudio) SoundEffectHolder.instance.PlayNarration(audioClip);
    }

    public override void OnOptionChanged()
    {
        doPlayAudio = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.MUSIC_MUTED); // TODO update to narration

    }
}
