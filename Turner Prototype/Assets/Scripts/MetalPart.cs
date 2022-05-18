using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPart : MonoBehaviour
{

    private bool isCutted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chisel") && !isCutted)
        {
            if (collision.gameObject.GetComponent<Chisel>().IsHeld)
            {
                StartCoroutine(Cutting());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();
    }

    IEnumerator Cutting()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            transform.localScale = new Vector3(1, transform.localScale.y - 0.1f, 1);
        }
    }
}
