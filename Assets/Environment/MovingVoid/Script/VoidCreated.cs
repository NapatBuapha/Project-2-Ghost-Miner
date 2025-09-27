using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class VoidCreated : MonoBehaviour
{

    [SerializeField] private Vector3 movingPos;
    [SerializeField] private GameObject mVoid;
    [SerializeField] private float SpawnDistance = 15;
    

    void Start()
    {
        movingPos = new Vector3(transform.position.x - SpawnDistance, transform.position.y, transform.position.z);
        mVoid = GameObject.FindWithTag("MovingVoid");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            float distanceVoidToPlayer = Vector3.Distance(col.transform.position, mVoid.transform.position);
            float distanceVoidToSpawn = Vector3.Distance(movingPos, mVoid.transform.position);

            Debug.Log(distanceVoidToPlayer);
            Debug.Log(distanceVoidToSpawn);

            if (distanceVoidToPlayer > distanceVoidToSpawn)
            {
                Destroy(gameObject);
                mVoid.transform.position = new Vector3(movingPos.x, mVoid.transform.position.y, mVoid.transform.position.z);
            }
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
