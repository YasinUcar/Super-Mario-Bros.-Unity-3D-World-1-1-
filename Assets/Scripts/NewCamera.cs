using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{

    public GameObject target;
    public Vector3 offset;
   
    
    private Vector3 CameraPos; // Variable that contains the Cameras x,y,z position

    void Start()
    {
        CameraPos = transform.position; // stores the Camera's position in the variable
    }
    // Use this for initialization


    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position.x < target.transform.position.x)
        {
            CameraPos.x = target.transform.position.x;
            transform.position = CameraPos;

        }
    }
}
