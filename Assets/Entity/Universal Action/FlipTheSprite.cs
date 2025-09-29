using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlipTheSprite : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject sprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0)
        {
            sprite.transform.localScale = new Vector3( -Mathf.Abs(sprite.transform.localScale.x),
            sprite.transform.localScale.y,
            sprite.transform.localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            sprite.transform.localScale = new Vector3(Mathf.Abs(sprite.transform.localScale.x),
            sprite.transform.localScale.y,
            sprite.transform.localScale.z);
        }
    }
}
