using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int bubble_hits = 1;
    public int points = 50;
    public Vector3 rotator;
    public Material hitMaterial;

    Material material_org;
    Renderer renderer_org;


    void Start()
    {
        transform.Rotate(rotator * (transform.position.x + transform.position.y) *0.1f);
        renderer_org = GetComponent<Renderer>();
        material_org = renderer_org.sharedMaterial;


    }

    
    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        bubble_hits--;
        if(bubble_hits<=0)
        {
            GameManager.Instance.Score += points;
            Destroy(gameObject);
        }
        renderer_org.sharedMaterial = hitMaterial;
        Invoke("RestoreMaterial", 0.05f);
    }
    void RestoreMaterial()
    {
        renderer_org.sharedMaterial = material_org;
    }
}
