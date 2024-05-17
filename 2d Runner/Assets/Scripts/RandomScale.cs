using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    float k = 0;
    // Start is called before the first frame update
    void Start()
    {
        k = Random.Range(-0.1f, 0.1f);
        transform.localScale += new Vector3(k,k, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
