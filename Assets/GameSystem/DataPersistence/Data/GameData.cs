using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 reSpawnPos ;
    
    public GameData()
    {
        this.reSpawnPos = new Vector3(-63,-1,0);
    }
}
