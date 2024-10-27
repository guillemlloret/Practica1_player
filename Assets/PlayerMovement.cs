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
    public Transform cameraTransform; // Asigna aqu� el transform de la c�mara en Unity

    Animator _animator;
    private bool isRunning = false; // �Est� corriendo?

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<CharacterMovement>();
        _animator = GetComponent<Animator>();
        _lastVelocity = Vector3.zero;
    }

    void Update()
    {
        // Detecta si el personaje est� corriendo
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Llama a la funci�n Move para procesar el movimiento y la animaci�n
        Move();

        // Actualiza la velocidad en el animador en funci�n de la magnitud horizontal
        Vector3 horizontalSpeed = new Vector3(_lastVelocity.x, 0, _lastVelocity.z);
        _animator.SetFloat("speed", horizontalSpeed.magnitude);
    }

    private void Move()
    {
        // Obtenemos la entrada del movimiento en funci�n del teclado
        float horizontal = Input.GetAxis("Horizontal"); // Teclas A/D o flechas izquierda/derecha
        float vertical = Input.GetAxis("Vertical"); // Teclas W/S o flechas arriba/abajo

        // Calcula las direcciones hacia adelante y hacia la derecha seg�n la c�mara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignora el eje vertical para que el movimiento solo sea en el plano XZ
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calcula la direcci�n del movimiento basado en la entrada y la c�mara
        Vector3 direction = forward * vertical + right * horizontal;

        // Determina la velocidad de movimiento en funci�n de si est� corriendo
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 velocity = direction * currentSpeed;

        // Manejo de la gravedad y el salto
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

        // Aplica el movimiento del CharacterController
        _characterController.Move(velocity * Time.deltaTime);

        // Guarda la �ltima velocidad para mantener la animaci�n y el control de salto
        _lastVelocity = velocity;

        // Rotaci�n del personaje en la direcci�n de movimiento
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
