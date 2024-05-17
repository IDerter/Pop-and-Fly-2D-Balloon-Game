using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreMoney : MonoBehaviour
{
    public Player script;
    Text scoreDisplay;
    // Use this for initialization
    void Start()
    {
        scoreDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = script.scoremoney.ToString();
        scoreDisplay.text = "" + script.scoremoney;
        if (script.scoremoney >= 100)
        {
            scoreDisplay.fontSize = 80;
        }
        if (script.scoremoney >= 1000)
        {
            scoreDisplay.fontSize = 60;
        }
    }
}