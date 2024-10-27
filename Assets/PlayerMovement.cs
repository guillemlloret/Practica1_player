using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    CharacterMovement _input;
    public float Speed = 1f;

    private Vector3 _lastVelocity;

    Animator _animator;
    public float JumpSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<CharacterMovement>();
        _animator = GetComponent<Animator>();
        _lastVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
       
        Move();

        Vector3 horizontalSpeed = _lastVelocity;

       

        Debug.Log(horizontalSpeed.magnitude);
       

        _animator.SetFloat("speed", horizontalSpeed.magnitude);
       
    }
    private void Jump(ref Vector3 velocity)
    {
        velocity.y = JumpSpeed;
    }
    private bool ShouldJump()
    {
        return _input.Jump;
    }


    private void Move()
    {

        Vector3 direction = new Vector3(_input.Move.x, 0,_input.Move.y);
        //_characterController.SimpleMove(direction * Speed);

        Vector3 velocity;
        velocity.x = direction.x * Speed;
        velocity.y = _lastVelocity.y;
        velocity.z = direction.z * Speed;

        velocity.y = GetGravity();

        if (ShouldJump())
            Jump(ref velocity);

        _characterController.Move(velocity * Time.deltaTime);

        //turn
        if (direction.magnitude > 0)
        {
            Vector3 target = transform.position + direction;
            transform.LookAt(target);
            Speed = Speed + 0.001f;
        }
        else if (direction.magnitude == 0)
        {
            Speed = 0f;
        }
        _lastVelocity.y = velocity.y;
       
     
        
    }

    private float GetGravity()
    {
        return _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
    }
}
