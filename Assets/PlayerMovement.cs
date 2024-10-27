using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    CharacterMovement _input;
    public float walkSpeed = 1f;
    public float runSpeed = 5f;
    public float jumpSpeed = 5f;
    private Vector3 _lastVelocity;

    Animator _animator;
    private bool isRunning = false;     // Està corrent?

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<CharacterMovement>();
        _animator = GetComponent<Animator>();
        _lastVelocity = Vector3.zero;
    }

    void Update()
    {

        isRunning = Input.GetKey(KeyCode.LeftShift);

        Move();


        Vector3 horizontalSpeed = new Vector3(_lastVelocity.x, 0, _lastVelocity.z);
        _animator.SetFloat("speed", horizontalSpeed.magnitude);
    }

    private void Move()
    {

        Vector3 direction = new Vector3(_input.Move.x, 0, _input.Move.y);


        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 velocity = direction * currentSpeed;


        if (_characterController.isGrounded)
        {
            velocity.y = -0.5f;
            if (ShouldJump())
            {
                velocity.y = jumpSpeed;
            }
        }
        else
        {

            velocity.y = _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
        }


        _characterController.Move(velocity * Time.deltaTime);


        _lastVelocity = velocity;

        // Control de rotació
        if (direction.magnitude > 0)
        {
            Vector3 target = transform.position + direction;
            transform.LookAt(target);
        }
    }

    private bool ShouldJump()
    {
        return _input.Jump && _characterController.isGrounded;
    }
}
