using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaOberta : MonoBehaviour
{
    public Animator _animator;
    public GameObject porta;
    

    // Start is called before the first frame update
    void Start()
    {
       
        _animator.SetBool("porta", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("porta_oberta");
            _animator.SetBool("porta", true);
        }
    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("porta_oberta");
            _animator.SetBool("porta", false);
        }
    }
}
