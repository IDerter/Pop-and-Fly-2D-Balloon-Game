using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShieldCooldown : MonoBehaviour {
    bool IsCoolDown;
    public float cooldown;
    public Image shieldUI;
	// Use this for initialization
	void Start () {
        IsCoolDown = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsCoolDown)
        {
            shieldUI.fillAmount -= 1 / cooldown * Time.deltaTime;
            if(shieldUI.fillAmount <=0)
            {
                shieldUI.fillAmount = 1;
                IsCoolDown = false;
                gameObject.SetActive(false);
            }
        }
        IsCoolDown = true;
    }
}
