using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrentAire : MonoBehaviour
{
    public Vector3 DireccióVent = Vector3.forward; 
    public float ForçaVent = 10f;
    public Rigidbody rb;

    private void OnTriggerStay(Collider other)
    {

        rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(DireccióVent.normalized * ForçaVent);
        }

    }
}