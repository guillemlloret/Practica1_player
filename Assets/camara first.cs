using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camarafirst : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 2f;
    float camaraVerticalRotation = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        camaraVerticalRotation -= inputY;
        camaraVerticalRotation = Mathf.Clamp(camaraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * camaraVerticalRotation;

        Player.Rotate(Vector3.up * inputX);
    }

}
