using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanger : MonoBehaviour
{
    bool hasLantern;
    GameObject movingVoid;

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Lantern"))
        {
            LanternCore lantern = col.GetComponent<LanternCore>();
            if (lantern.lanternState == LanternState.UnAttach)
            {
                lantern.SetHaggingTransform(transform);
                lantern.SwitchState(LanternState.Hanging);
            }
        }
    }
}
