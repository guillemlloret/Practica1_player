using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrentAire : MonoBehaviour
{
    public Vector3 Direcci�Vent = Vector3.forward; 
    public float For�aVent = 10f;
    public Rigidbody rb;

    private void OnTriggerStay(Collider other)
    {

        rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(Direcci�Vent.normalized * For�aVent);
        }

    }
}