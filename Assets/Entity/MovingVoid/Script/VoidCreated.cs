using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class VoidCreated : MonoBehaviour
{

    [SerializeField] private GameObject voidPrefab;
    [SerializeField] private Transform spawnPos;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Instantiate(voidPrefab, spawnPos.position, quaternion.identity);
        }
    }

    /*void DeletePreviousVoid()
    {
        GameObject[] voids = GameObject.FindGameObjectsWithTag("MovingVoid");
        foreach (GameObject void_ in voids)
        {
            Destroy(void_);
        }
        
    }*/ 
    //หากต้องการให้ void เก่าหายไป
}
