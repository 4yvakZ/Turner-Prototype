using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    private GameObject horizontalHandle;
    [SerializeField] private float horizontalHandleSpeed = 40f;

    private GameObject verticalHandle;
    [SerializeField] private float verticalHandleMagnitude = 0.65f;
    [SerializeField] private float verticalHandleSpeed = 200f;
    private float verticalHandlePhase = 0;

    // Start is called before the first frame update
    void Start()
    {
        horizontalHandle = transform.GetChild(0).gameObject;
        verticalHandle = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(horizontalInput * horizontalSpeed * Time.deltaTime, verticalInput * verticalSpeed * Time.deltaTime));

        horizontalHandle.transform.Rotate(Vector3.back, horizontalInput * horizontalHandleSpeed * Time.deltaTime);

        verticalHandlePhase += verticalInput * verticalHandleSpeed * Time.deltaTime;
        verticalHandle.transform.localPosition = new Vector3(verticalHandle.transform.localPosition.x, verticalHandleMagnitude * Mathf.Cos(verticalHandlePhase), verticalHandle.transform.localPosition.z);
    }
}
