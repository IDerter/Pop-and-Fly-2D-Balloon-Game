using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySaw : MonoBehaviour {

	public float speed = 0.04f;

	void Update () {
        //transform.Rotate(0, 0, 2.0f);//
        transform.Rotate(new Vector3(0f, 0f, speed * Time.deltaTime));
    }
}