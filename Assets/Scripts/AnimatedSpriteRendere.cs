using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRendere : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float animationTime = 0.25f;

    public Sprite[] animationSprites;

    public Sprite idleSprite;

    public bool loop = true;
    public bool idle = true;
    private int animationFrame;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    void Start()
    {

        InvokeRepeating(nameof(NewFrame), animationTime, animationTime);

    }

    private void NewFrame()
    {
        animationFrame++;
        if (loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else
        {

            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}
