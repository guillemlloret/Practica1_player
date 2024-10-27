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
    public Transform cameraTransform; // Asigna aquí el transform de la cámara en Unity

    Animator _animator;
    private bool isRunning = false; // ¿Está corriendo?

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<CharacterMovement>();
        _animator = GetComponent<Animator>();
        _lastVelocity = Vector3.zero;
    }

    void Update()
    {
        // Detecta si el personaje está corriendo
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Llama a la función Move para procesar el movimiento y la animación
        Move();

        // Actualiza la velocidad en el animador en función de la magnitud horizontal
        Vector3 horizontalSpeed = new Vector3(_lastVelocity.x, 0, _lastVelocity.z);
        _animator.SetFloat("speed", horizontalSpeed.magnitude);
    }

    private void Move()
    {
        // Obtenemos la entrada del movimiento en función del teclado
        float horizontal = Input.GetAxis("Horizontal"); // Teclas A/D o flechas izquierda/derecha
        float vertical = Input.GetAxis("Vertical"); // Teclas W/S o flechas arriba/abajo

        // Calcula las direcciones hacia adelante y hacia la derecha según la cámara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignora el eje vertical para que el movimiento solo sea en el plano XZ
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calcula la dirección del movimiento basado en la entrada y la cámara
        Vector3 direction = forward * vertical + right * horizontal;

        // Determina la velocidad de movimiento en función de si está corriendo
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

        // Guarda la última velocidad para mantener la animación y el control de salto
        _lastVelocity = velocity;

        // Rotación del personaje en la dirección de movimiento
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
