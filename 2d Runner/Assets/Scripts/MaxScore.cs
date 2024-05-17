using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScore : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //GetComponent<Text>().text = PlayerPrefs.GetInt("Score0").ToString();
        GetComponent<Text>().text = PlayerPrefs.GetInt("Score0").ToString();
        Debug.Log("Произошло сохранение числа:");

    }
}