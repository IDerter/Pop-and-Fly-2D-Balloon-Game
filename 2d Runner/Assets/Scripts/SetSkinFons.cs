using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetSkinFons : MonoBehaviour
{
    public int index1;
    [SerializeField] private Sprite[] bodySprite;//, leftHandSprite, rightHandSprite, leftFootSprite, rightFootSprite;
    //private SpriteRenderer body;//, leftHand, rightHand, leftFoot, rightFoot;
    public Image body;
    //public 
    private void Awake()
    {
        index1 = PlayerPrefs.GetInt("index1");
    }
    private void Start()
    {
        //body = transform.GetChild(0).GetComponent<SpriteRenderer>();
      //  body = transform.GetComponent<Image>();
        //leftHand = transform.GetChild(1).GetComponent<SpriteRenderer>();
        //rightHand = transform.GetChild(2).GetComponent<SpriteRenderer>();
        //leftFoot = transform.GetChild(3).GetComponent<SpriteRenderer>();
        // rightFoot = transform.GetChild(4).GetComponent<SpriteRenderer>();

        UpdateSkin(index1);
    }

    public void UpdateSkin(int index1)
    {
        body.sprite = bodySprite[index1];
        // leftHand.sprite = leftHandSprite[index];
        //rightHand.sprite = rightHandSprite[index];
        // leftFoot.sprite = leftFootSprite[index];
        //rightFoot.sprite = rightFootSprite[index];
    }
}
