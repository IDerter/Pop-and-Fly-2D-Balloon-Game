using UnityEngine;

public class SpawnerYellowBallon : MonoBehaviour
{
    public GameObject[] gearVariants;
    public Transform allobject;
    
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;
    
    private float timeBtwSpawn;

    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, gearVariants.Length);
            
            // Заменили Instantiate на пул объектов
            Instantiate(gearVariants[rand], transform.position, Quaternion.identity, allobject);
            
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