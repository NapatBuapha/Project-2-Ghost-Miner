using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessManager : MonoBehaviour
{
    //ใช้สำหรับสร้างเสียงหลอน bla bla bla ในอนาคต
    DarknessStatus playerStatus;

    void Awake()
    {
        playerStatus = GameObject.FindWithTag("Player").GetComponent<DarknessStatus>();
    }
}
