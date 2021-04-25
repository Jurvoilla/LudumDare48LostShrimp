using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class MyArray{
        public GameObject[] objects;
    }


    public Transform[] spawnPoints;
    public MyArray[] spawnObject;
    public int[] zones;
    public Transform profondeur;

    public int zone = 0;
    public float timeSpawn;
    private float timePass;



    private void FixedUpdate() 
    {
        if(timePass > timeSpawn)
        {
            Spawn();
            timePass = 0f;
        }
        else
        {
            timePass += Time.fixedDeltaTime;
        }
    }

    private void Spawn()
    {
        GameObject fishI = spawnObject[zone].objects[Random.Range(0,  spawnObject[zone].objects.Length)];
        Transform spawnI = spawnPoints[Random.Range(0,  spawnPoints.Length)];
        GameObject fish = Instantiate(fishI, spawnI.position, spawnI.rotation);

        if(zone < zones.Length)
        {
            if(-profondeur.position.y > zones[zone])
            {
                zone++;
                FindObjectOfType<Player>().Upgrade();
                timeSpawn -= 0.15f;
                FindObjectOfType<CameraMove>().speed += 0.9f;
            }
        }
    }
}
