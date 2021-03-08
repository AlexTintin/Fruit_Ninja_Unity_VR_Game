using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampo : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

        void OnTriggerEnter(Collider other)
        {
        //Debug.Log("before collided");
            cam.GetComponent<Rigidbody>().velocity = new Vector3(cam.GetComponent<Rigidbody>().velocity.x, -cam.GetComponent<Rigidbody>().velocity.y, cam.GetComponent<Rigidbody>().velocity.z);
        }
    
}
