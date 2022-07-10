
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    public float speed = 5f;
    private Vector2 direction = Vector2.down;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSpriteRendere spriteRendererUp;
    public AnimatedSpriteRendere spriteRendererDown;
    public AnimatedSpriteRendere spriteRendererLeft;
    public AnimatedSpriteRendere spriteRendererRight;
    public AnimatedSpriteRendere spriteRendererDeath;
    private AnimatedSpriteRendere activeSpriteRenderer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 moveMent = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + moveMent);
    }

    private void Update()
    {
        if (Input.GetKey(inputUp))
        {
            setDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            setDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            setDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            setDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            setDirection(Vector2.zero, spriteRendererDown);
        }
    }

    private void setDirection(Vector2 newDirection, AnimatedSpriteRendere spriteRender)
    {
        direction = newDirection;

        spriteRendererDown.enabled = spriteRender == spriteRendererDown;
        spriteRendererUp.enabled = spriteRender == spriteRendererUp;
        spriteRendererLeft.enabled = spriteRender == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRender == spriteRendererRight;

        activeSpriteRenderer = spriteRender;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererUp.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }


    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
