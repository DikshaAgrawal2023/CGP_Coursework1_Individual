using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour
{
    float speed = 20f;
    Rigidbody ball_rigidbody;
    Vector3 velocity;
    Renderer renderer1;

    
    void Start()
    {
        ball_rigidbody = GetComponent<Rigidbody>();
        renderer1 = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
        
    }

    void Launch()
    {
        ball_rigidbody.velocity = Vector3.up * speed;
    }

    void FixedUpdate()
    {
        ball_rigidbody.velocity = ball_rigidbody.velocity.normalized * speed;
        velocity = ball_rigidbody.velocity;

        if(!renderer1.isVisible)
        {
            GameManager.Instance.Balls--;
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        ball_rigidbody.velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
    }
}
