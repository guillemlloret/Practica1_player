using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_3a_persona : MonoBehaviour
{
    public Transform target; // Referencia al personaje
    public Vector3 offset = new Vector3(0, 2, -4); // Ajuste de desplazamiento para la cámara
    public float mouseSensitivity = 3f; // Sensibilidad del ratón ajustada
    private float rotationX = 0f; // Para la rotación vertical
    private float rotationY = 0f; // Para la rotación horizontal

    void Start()
    {
        // Bloquea y oculta el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        // Obtén la entrada del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotación vertical (en el eje X)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Rotación horizontal (en el eje Y)
        rotationY += mouseX;

        // Aplica la rotación de la cámara sin modificar al personaje
        transform.position = target.position + offset;
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
