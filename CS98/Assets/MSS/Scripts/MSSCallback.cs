using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSSCallback : MonoBehaviour
{
    public int spawnerIndex;

    private SpawnerManager spawnerManager;

    public void SetSpawnerManager(SpawnerManager s_manager)
    {
        spawnerManager = s_manager;
    }

    public void SetIndex(int index){
        spawnerIndex = index;
    }

    public void SetTimer(float timeToLive){
        Destroy(this.gameObject, timeToLive);
    }

    void OnDestroy()
    {
        if(spawnerManager != null){
            spawnerManager.PrefabDestroyed(spawnerIndex);
        }
    }
}
