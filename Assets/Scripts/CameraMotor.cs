using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;    //object that we are looking at (player for example)

    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        
        //calculating difference between character position and camera position
        float deltaX = lookAt.position.x - transform.position.x;    //transform here belongs to camera
        if (deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            } 
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        //moving the camera
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
