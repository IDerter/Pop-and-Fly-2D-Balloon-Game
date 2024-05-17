using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public Transform wall;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        wall.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed);
        wall.localScale = new Vector3(1f, 1f);
    }
}
