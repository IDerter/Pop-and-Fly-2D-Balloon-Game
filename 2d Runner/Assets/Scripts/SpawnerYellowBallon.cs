using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerYellowBallon : MonoBehaviour
{

    public GameObject[] gearVariants;
    public Transform allobject;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;

    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, gearVariants.Length);
            Instantiate(gearVariants[rand],transform.position, Quaternion.identity).transform.SetParent(allobject);
            timeBtwSpawn = startTimeBtwSpawn;
            if (startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= decreaseTime;
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

}