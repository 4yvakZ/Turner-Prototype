using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chisel : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float speed = 10;
    private Vector3 mouseOffset;
    private Rigidbody2D chiselRB;
    // Start is called before the first frame update
    void Start()
    {
        chiselRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        mouseOffset = transform.position - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        var vel = (GetMousePosition() + mouseOffset - transform.position) * speed;
        
        if (vel.magnitude > maxSpeed) 
        { 
            vel = vel.normalized * maxSpeed;
        }

        Debug.Log(vel.magnitude);

        chiselRB.velocity = vel;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
