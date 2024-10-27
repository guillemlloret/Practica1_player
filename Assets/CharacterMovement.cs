using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Vector2 movementInput;

    public Vector2 Move => movementInput;
    private bool _jump;
    public bool Jump => _jump;

    private void OnMove(InputValue value)
    {
        Debug.Log("hola");
        movementInput = value.Get<Vector2>();


    }
    private void OnJump()
    {
        _jump = true;
    }

    private void LateUpdate()
    {
        _jump = false;
        if (movementInput.magnitude == 0)
        {
            movementInput = Vector2.zero;  
        }
    }

}
