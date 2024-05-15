using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPhysics : MonoBehaviour
{
    //https://youtu.be/nPigL-dIqgE
    //https://www.youtube.com/watch?v=SPe1xh4D7Wg

    public float acceleration;
    public float maxSpeed;

    public float jumpHeight;

    public Vector2 velocity = new(0, 0);


    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;
    [SerializeField] private AudioSource throwSoundEffect;

    private PlayerInput playerInput;
    private PlayerAnimator animator;
    private PlayerController controller;

    private Physics physics;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        animator = GetComponent<PlayerAnimator>();
        controller = GetComponent<PlayerController>();

        physics = GetComponent<Physics>();

    }

    void Update() {
        physics.HInput = playerInput.actions["Movement"].ReadValue<float>();
        physics.CrouchInput = playerInput.actions["Crouch"].ReadValue<float>();
        physics.JumpInput = playerInput.actions["Jump"].ReadValue<float>();


        animator.onGround = physics.onGround;
        animator.velocity = physics.velocity;
        
    }

}
