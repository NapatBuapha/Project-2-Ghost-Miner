using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessManager : MonoBehaviour
{
    DarknessStatus playerStatus;

    void Awake()
    {
        playerStatus = GameObject.FindWithTag("Player").GetComponent<DarknessStatus>();
    }
}
