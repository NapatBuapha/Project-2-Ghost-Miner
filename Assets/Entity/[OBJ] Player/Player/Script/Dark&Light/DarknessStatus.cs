using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessStatus : MonoBehaviour
{
    [SerializeField] private float darknessCountdown;
    [SerializeField] private float stayInDarkTime;
    [SerializeField] private LayerMask lightLayer;
    [SerializeField] private Collider2D col;


    void Start()
    {
        col = GetComponent<Collider2D>();
        ResetTimer();
    }
    void FixedUpdate()
    {
            if (stayInDarkTime < darknessCountdown)
            {
                stayInDarkTime += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("Player Death");
            }
    }

    void Update()
    {
        if (col.IsTouchingLayers(lightLayer))
        {
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        stayInDarkTime = 0;
    }

    
}
