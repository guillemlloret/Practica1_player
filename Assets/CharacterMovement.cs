using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Vector2 movementInput;

    public Vector2 Move => movementInput;

  
    private void OnMove(InputValue value)
    {
        Debug.Log("hola");
        movementInput = value.Get<Vector2>();
       
   
    }
   
}
