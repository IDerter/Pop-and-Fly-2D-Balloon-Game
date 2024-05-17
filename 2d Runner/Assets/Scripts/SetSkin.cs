using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkin : MonoBehaviour {

    public int index;
    [SerializeField] private Sprite[] bodySprite;//, leftHandSprite, rightHandSprite, leftFootSprite, rightFootSprite;
    private SpriteRenderer body;//, leftHand, rightHand, leftFoot, rightFoot;
    private void Awake()
    {
        index = PlayerPrefs.GetInt("index");
    }
    private void Start()
    {
        body = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //leftHand = transform.GetChild(1).GetComponent<SpriteRenderer>();
        //rightHand = transform.GetChild(2).GetComponent<SpriteRenderer>();
        //leftFoot = transform.GetChild(3).GetComponent<SpriteRenderer>();
        // rightFoot = transform.GetChild(4).GetComponent<SpriteRenderer>();
       
        UpdateSkin(index);
    }

    public void UpdateSkin(int index)
    {
        body.sprite = bodySprite[index];
       // leftHand.sprite = leftHandSprite[index];
        //rightHand.sprite = rightHandSprite[index];
       // leftFoot.sprite = leftFootSprite[index];
        //rightFoot.sprite = rightFootSprite[index];
    }
}
