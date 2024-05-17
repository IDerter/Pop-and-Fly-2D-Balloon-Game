using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour {
    public Player script;
    Text scoreDisplay;
    void Start () {
        scoreDisplay = GetComponent<Text>();
    }
	void Update () {
        scoreDisplay.text = script.score.ToString();
        scoreDisplay.text = "" + script.score;
        if(script.score>=100)
        {
            scoreDisplay.fontSize = 80;
        }
        if (script.score >= 1000)
        {
            scoreDisplay.fontSize = 60;
        }
    }
}
