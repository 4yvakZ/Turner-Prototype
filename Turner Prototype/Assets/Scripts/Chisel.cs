using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chisel : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float speed = 10;
    [SerializeField] private int gravityScale = 2;
    private Vector3 mouseOffset;
    private Rigidbody2D chiselRB;

    private bool isDragged = false;
    private bool isHeld = false;

    public bool IsHeld { get => isHeld; private set => isHeld = value; }

    // Start is called before the first frame update
    void Start()
    {
        chiselRB = GetComponent<Rigidbody2D>();
        chiselRB.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        mouseOffset = transform.position - GetMousePosition();
        isDragged = true;
        IsHeld = false;
    }

    private void OnMouseDrag()
    {
        var vel = (GetMousePosition() + mouseOffset - transform.position) * speed;
        
        if (vel.magnitude > maxSpeed) 
        { 
            vel = vel.normalized * maxSpeed;
        }

        chiselRB.velocity = vel;
    }

    private void OnMouseUp()
    {
        isDragged = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Holder")){
            if (!isDragged)
            {
                transform.position = (Vector3)(collision.GetComponent<CircleCollider2D>().offset * collision.transform.localScale) + collision.transform.position;
                chiselRB.gravityScale = 0;
                IsHeld = true;
            } 
            else
            {
                chiselRB.gravityScale = gravityScale;
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
