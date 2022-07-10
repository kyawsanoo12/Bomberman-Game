using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRendere start;
    public AnimatedSpriteRendere middle;
    public AnimatedSpriteRendere end;

    public void setSpriteRenderer(AnimatedSpriteRendere spriteRendere)
    {
        start.enabled = spriteRendere == start;
        middle.enabled = spriteRendere == middle;
        end.enabled = spriteRendere == end;
    }

    public void setDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void DestroyAfter(float Duration)
    {
        Destroy(gameObject, Duration);
    }
}
