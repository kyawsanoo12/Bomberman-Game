
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefeb;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;

    private int bombRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefeb;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;
    public LayerMask explosionLayerMask;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public GameObject destructiblePrefeb;

    void OnEnable()
    {
        bombRemaining = bombAmount;
    }
    void Update()
    {
        if (bombRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(placeBomb());
        }

    }

    private IEnumerator placeBomb()
    {
        Vector2 position = transform.position;
        //position.x = Mathf.Round(position.x);
        //position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefeb, position, Quaternion.identity);
        bombRemaining--;

        yield return new WaitForSeconds(bombFuseTime);
        Destroy(bomb);
        position = bomb.transform.position;
        Explosion explosion = Instantiate(explosionPrefeb, position, Quaternion.identity);
        explosion.setSpriteRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        //Destroy(explosion, explosionDuration);
        bombRemaining++;

    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefeb, position, Quaternion.identity);
        explosion.setSpriteRenderer(length > 1 ? explosion.middle : explosion.end);

        explosion.setDirection(direction);
        explosion.DestroyAfter(explosionDuration);
        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefeb, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }


    }

    public void AddBomb()
    {
        bombAmount++;
        bombRemaining++;
    }
}
