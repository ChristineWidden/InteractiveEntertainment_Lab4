using System;
using UnityEngine;

public class DynamicFootstepsSoundSpeed : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private Physics physics; // Or use Rigidbody if applicable
    [SerializeField] private float minSpeed = 0f; // Standing still
    [SerializeField] private float minPitch = 0.8f; // Lower than normal
    [SerializeField] private float maxPitch = 1.5f; // Higher than normal

    public float volume = 0;
    public float speedMultiplier;

    private void Start() {
        SoundEffectHolder.instance.Footsteps.volume = 0f;
        SoundEffectHolder.instance.PlayClip(SoundEffectHolder.instance.Footsteps, audioClip);

        physics = GetComponent<Physics>();
    }

    private void Update()
    {
        if (physics == null)
            return;

        // Get character speed (for Rigidbody, use character.velocity.magnitude)
        float speed = Math.Abs(physics.trueVelocity.x);

        // Normalize speed to a 0-1 range
        float normalizedSpeed = Mathf.InverseLerp(minSpeed, physics.GetMaxSpeed(), speed);

        // Map speed to pitch range
        SoundEffectHolder.instance.Footsteps.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);

        volume = (float) Math.Pow(normalizedSpeed, 0.1f);
        SoundEffectHolder.instance.Footsteps.volume = volume;
    }
}