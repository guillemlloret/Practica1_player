using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Vector2 movementInput;
    Animator _animator;
    Rigidbody rb;
    public float speed = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = rb.GetComponent<Animator>();
        _animator.SetBool("Walking", false);
        
        
    }

    // Update is called once per frame
    private void Update()
    {
        DoMove();
       

    }
    private void OnMove(InputValue value)
    {
        Debug.Log("hola");
        movementInput = value.Get<Vector2>();
        _animator.SetBool("Walking", true);
    }
    private void DoMove ()
    {
        rb.transform.Translate(movementInput * speed * Time.deltaTime);
        
    }
}
