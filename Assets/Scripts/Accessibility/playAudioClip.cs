using UnityEngine;

public class PlayAudioClip : IOptionObserver
{
    [SerializeField] private AudioClip audioClip;
    private bool doPlayAudio;

    void Start() {
        doPlayAudio = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.NARRATION_MUTED);
    }

    public void PlayAudio() {
        if (doPlayAudio) {
            Debug.Log("Narration played!");
            SoundEffectHolder.instance.PlayClip(
                                        SoundEffectHolder.instance.Narration, 
                                        audioClip);
        }
    }

    public override void OnOptionChanged()
    {
        doPlayAudio = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.NARRATION_MUTED);
    }
}
