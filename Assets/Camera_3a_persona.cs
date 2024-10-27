using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_3a_persona : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(0, 2, -4); 
    public float mouseSensitivity = 3f; 
    private float rotationX = 0f; 
    private float rotationY = 0f; 

    void Start()
    {
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

       
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

     
        rotationY += mouseX;

       
        transform.position = target.position + offset;
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}

