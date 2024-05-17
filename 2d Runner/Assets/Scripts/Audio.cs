using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip myClip;
    // Use this for initialization
    void Start()
    {
        AudioSource.PlayClipAtPoint(myClip, transform.position); //создает пустой объект, который включает музыку через переменную myClip

    }

    // Update is called once per frame
    void Update()
    {

    }
}
