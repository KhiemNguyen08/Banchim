using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCOntroller : Singleton<BGCOntroller>
{
    // Start is called before the first frame update
    public Sprite[] sprites;
    public SpriteRenderer bgImage;
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public override void Start()
    {
        ChangSprite();
    }
    public void ChangSprite()
    {
        if (bgImage != null && sprites.Length > 0)
        {
            int rnadomIdx = Random.Range(0, sprites.Length);
            if(sprites[rnadomIdx] != null && sprites.Length > 0)
            {
                bgImage.sprite = sprites[rnadomIdx];
            }
        }
    }
}
