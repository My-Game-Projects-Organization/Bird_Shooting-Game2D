using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : Singleton<BGController> {

    public Sprite[] sprites;

    public SpriteRenderer bgImg;

    public override void Start() {
        ChangeSprite();
    }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ChangeSprite()
    {
        if(bgImg != null && sprites.Length > 0 && sprites != null)
        {
            int randIdx = Random.Range(0, sprites.Length);

            if(sprites[randIdx] != null)
            {
                bgImg.sprite = sprites[randIdx];
            }
        }
    }

}
