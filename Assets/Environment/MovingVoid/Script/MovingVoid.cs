using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VoidState
{
    Moving,
    Stop
}

[RequireComponent(typeof(Rigidbody2D))]
public class MovingVoid : MonoBehaviour
{

    VoidState voidSate;
    public Rigidbody2D rb { get; private set; }
    public float counter;
    [SerializeField] private float speed = 50;

    private float baseMultiplier = 1;
    [SerializeField] private float dMultiplier;

    void Start()
    {
        voidSate = VoidState.Stop;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (counter > 0)
        {
            voidSate = VoidState.Stop;
            counter -= Time.deltaTime;
        }
        else
        {
            voidSate = VoidState.Moving;
        }


        switch (voidSate)
        {
            case VoidState.Moving:
                VoidMoving();
                break;
            default:
                VoidStop();
                break;
        }
    }

    void VoidMoving()
    {
        rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
    }

    void VoidStop()
    {
        rb.velocity = Vector3.zero;
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            DarknessStatus dStatus = col.GetComponent<DarknessStatus>();
            baseMultiplier = dStatus.darkness_Multiplier;
            dStatus.darkness_Multiplier = dMultiplier;
            dStatus.isInVoid = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            AudioManager.PlaySound(SoundType.DARKNESS_scream2, 0.4f);
            DarknessStatus dStatus = col.GetComponent<DarknessStatus>();
            dStatus.darkness_Multiplier = baseMultiplier;
            dStatus.isInVoid = false;
        }
    }
}
