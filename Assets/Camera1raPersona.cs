using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstPersonController : MonoBehaviour
{
    public CharacterController characterController;
    public Transform playerCamera; // Refer�ncia a la c�mera del jugador
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloqueja el cursor al centre de la pantalla
    }

    void Update()
    {
        // Recollir l'input del moviment del jugador
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcula la direcci� del moviment respecte a la c�mera
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Mou el jugador
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Gravetat
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Rotaci� de la c�mera amb el ratol� (primer eix de moviment horitzontal i segon vertical)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Controlar la rotaci� vertical de la c�mera (limitar l'angle per no girar massa)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar l'angle vertical

        // Aplica la rotaci� a la c�mera
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX); // Gira el jugador a l'eix Y amb el ratol� horitzontal
    }
}


