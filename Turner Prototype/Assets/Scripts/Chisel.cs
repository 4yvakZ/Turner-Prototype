using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chisel : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float speed = 10;
    private Vector3 mouseOffset;
    private Rigidbody2D chiselRB;

    private bool isDragged = false;
    private bool isHeld = false;

    public bool IsHeld { get => isHeld; private set => isHeld = value; }

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
        isDragged = true;
        DetachFromHolder();
        mouseOffset = transform.position - GetMousePosition();
    }

    private void DetachFromHolder()
    {
        transform.SetParent(null);
        IsHeld = false;
        chiselRB.isKinematic = false;
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
        if (collision.gameObject.CompareTag("Holder") && !IsHeld){
            if (!isDragged)
            {
                AttachToHolder(collision.gameObject);
                IsHeld = true;
            } 
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    private void AttachToHolder(GameObject holder)
    {
        transform.SetParent(holder.transform);
        chiselRB.velocity = Vector2.zero;
        transform.localPosition = holder.GetComponent<CircleCollider2D>().offset;
        chiselRB.isKinematic = true;
    }
}
