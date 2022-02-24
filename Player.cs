using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody paddle_rigidbody;

    
    void Start()
    {
        
        paddle_rigidbody = GetComponent<Rigidbody>();

    }

    
    void FixedUpdate()
    {
        paddle_rigidbody.MovePosition(new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 50)).x, -17, 0));
    }
}
