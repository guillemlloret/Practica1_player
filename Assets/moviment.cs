using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController; // Assegura't d'afegir el CharacterController al component
    public float moveSpeed = 5f; // Velocitat del moviment
    public float moveSmooth = 0.1f; // Suavitat del moviment
    public float gravity = -9.81f; // Gravetat

    private Vector3 velocity; // Vector que emmagatzema la velocitat, incloent la gravetat

    void Update()
    {
        // Obtenir input del jugador (WASD o joystick)
        float moveX = Input.GetAxis("Horizontal"); // Tecles A/D o fletxes esquerra/dreta
        float moveZ = Input.GetAxis("Vertical");   // Tecles W/S o fletxes amunt/avall

        // Calcular direcció de moviment respecte a la direcció actual del personatge
        Vector3 localInput = transform.right * moveX + transform.forward * moveZ;

        // Suavitzar el moviment (Lerp fa la transició suau)
        float x = Mathf.Lerp(characterController.velocity.x, localInput.x * moveSpeed, moveSmooth);
        float z = Mathf.Lerp(characterController.velocity.z, localInput.z * moveSpeed, moveSmooth);

        // Gravetat
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Assegura que el personatge es quedi a terra
        }
        velocity.y += gravity * Time.deltaTime;

        // Crear vector final de moviment (x, gravetat en y, z)
        Vector3 movement = new Vector3(x, velocity.y, z);

        // Aplicar el moviment al CharacterController
        characterController.Move(movement * Time.deltaTime);
    }
}


