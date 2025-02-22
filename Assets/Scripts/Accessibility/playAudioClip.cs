using UnityEngine;

public class PlayAudioClip : IOptionObserver
{
    [SerializeField] private AudioClip audioClip;
    private bool doPlayAudio;

    void Start() {
        doPlayAudio = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.NARRATION_MUTED);
    }

    public void PlayAudio() {
        Debug.Log("Narration played!");
        if (doPlayAudio) SoundEffectHolder.instance.PlayClip(
                                        SoundEffectHolder.instance.Narration, 
                                        audioClip);
    }

    public override void OnOptionChanged()
    {
        doPlayAudio = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.NARRATION_MUTED);
    }
}
