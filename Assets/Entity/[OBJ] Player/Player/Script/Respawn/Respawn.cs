using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Respawn : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Vector3 reSpawnPos;

    public UnityEvent checkPoint;
    public void LoadData(GameData data)
    {
        reSpawnPos = data.reSpawnPos;
        gameObject.transform.position = reSpawnPos;
    }

    public void SaveData(ref GameData data)
    {
        data.reSpawnPos = reSpawnPos;
    }

    void Start()
    {
        
    }

    public void SetPosition(Vector3 checkpoint)
    {
        reSpawnPos = checkpoint;
        GameObject.Find("[MANAGER] DataPersistenceManager").GetComponent<DataPersistenceManager>().SaveGame();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Checkpoint"))
        {
            SetPosition(col.transform.position);
        }
    }

    
}
