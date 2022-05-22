using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPart : MonoBehaviour
{

    [SerializeField] private float cuttingTime = 0.5f;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private int currentSpriteIndex = 21;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isCutted = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        spriteRenderer.sprite = sprites[currentSpriteIndex];
        boxCollider.size = sprites[currentSpriteIndex].bounds.size;
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
            yield return new WaitForSeconds(cuttingTime);
            currentSpriteIndex--;
            if (currentSpriteIndex >= 0)
            {
                spriteRenderer.sprite = sprites[currentSpriteIndex];
                boxCollider.size = sprites[currentSpriteIndex].bounds.size;
            } 
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
