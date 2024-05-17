using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] gearVariants;
    public Transform allobject;
    private float timeSpawn;
    public float startTimeSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;

    private void Update()
    {
        if (timeSpawn <= 0)
        {
            int rand = Random.Range(0, gearVariants.Length); 
            // создаем объект и помешаем его в объект allobject 
            Instantiate(gearVariants[rand], transform.position, Quaternion.identity).transform.SetParent(allobject); 
            
            timeSpawn = startTimeSpawn;
            if (startTimeSpawn > minTime)
            {
                startTimeSpawn -= decreaseTime;
            }
        }
        else
        {
            timeSpawn -= Time.deltaTime; //Интервал в секундах от последнего кадра до текущего
        }
    }

}
