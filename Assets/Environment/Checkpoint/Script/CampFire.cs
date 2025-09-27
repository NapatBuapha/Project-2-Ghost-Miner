using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CampFire : MonoBehaviour
{
    enum CampState
    {
        On,
        Off,
    }
    CampState campState;
    [SerializeField] private float voidStopDistance = 20f;
    [SerializeField] private float ignitionDistance = 20f;
    [SerializeField] private GameObject light_;

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        campState = CampState.On;
    }

    void Update()
    {
        float playerDis = Vector2.Distance(player.transform.position, transform.position);
        {
            if (playerDis < ignitionDistance)
            {
                campState = CampState.On;
            }
            else
            {
                campState = CampState.Off;
            }
        }
        switch (campState)
        {
            case CampState.On:
                CampfireOn();
                break;
            default:
                light_.SetActive(false);
                break;
        }
    }

    void CampfireOn()
    {
        light_.SetActive(true);
        GameObject movingV = GameObject.FindWithTag("MovingVoid");

        if (Vector3.Distance(transform.position, movingV.transform.position) < voidStopDistance)
        {
            MovingVoid moVoid = movingV.GetComponent<MovingVoid>();
            moVoid.counter = 3;
        }
    }

}
