using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGreen : MonoBehaviour
{
    public GameObject gearyellow;
    void Start()
    {
        Instantiate(gearyellow, transform.position, Quaternion.identity); 
    }
}
