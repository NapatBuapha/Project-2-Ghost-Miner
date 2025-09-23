using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingVoid : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float counter;
    [SerializeField] private float speed = 50;
    [SerializeField] private float dMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (counter > 0)
        {
            counter -= Time.fixedDeltaTime;
        }
        else
        {
            VoidMoving();
        }
    }

    void VoidMoving()
    {
        rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            DarknessStatus dStatus = col.GetComponent<DarknessStatus>();
            dStatus.darkness_Multiplier = dMultiplier;
            dStatus.isInVoid = true;
        }
    }
}
