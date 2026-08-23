using UnityEngine;

public class Spawner : MonoBehaviour 
{
    public GameObject[] gearVariants;
    public Transform allobject;
    
    public float startTimeSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;
    
    private float timeSpawn;

    private void Update()
    {
        if (timeSpawn <= 0)
        {
            int rand = Random.Range(0, gearVariants.Length); 
            
            // Обращаемся к пулу вместо Instantiate
            Instantiate(gearVariants[rand], transform.position, Quaternion.identity, allobject);
            
            timeSpawn = startTimeSpawn;
            
            if (startTimeSpawn > minTime)
            {
                startTimeSpawn -= decreaseTime;
            }
        }
        else
        {
            timeSpawn -= Time.deltaTime; 
        }
    }
}