using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    public int LevelToLoad;
    public int LevelToLoadnew;
    public bool lvl = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeToLevel()
    {
        anim.SetTrigger("fade");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(LevelToLoad);

    }
    public void OnFadeComplete1()
    {
        SceneManager.LoadScene(LevelToLoadnew);

    }
}
