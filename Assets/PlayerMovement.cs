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

    public float moveSpeed = 5f;
    public Transform cameraTransform;

    Animator _animator;
    private bool isRunning = false; 
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<CharacterMovement>();
        _animator = GetComponent<Animator>();
        _lastVelocity = Vector3.zero;
        _animator.SetBool("Jump", false);
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
       
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 

     
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

    
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        
        Vector3 direction = forward * vertical + right * horizontal;

     
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 velocity = direction * currentSpeed;

        
        if (_characterController.isGrounded)
        {
            velocity.y = -0.5f;
            if (ShouldJump())
            {
                velocity.y = jumpSpeed;
                _animator.SetBool("Jump", true);
            }
        }
        else
        {
            velocity.y = _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
            _animator.SetBool("Jump", false);
        }

        

        _characterController.Move(velocity * Time.deltaTime);

       
        _lastVelocity = velocity;

     
        if (direction.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private bool ShouldJump()
    {
        return _input.Jump && _characterController.isGrounded;
    }
}


