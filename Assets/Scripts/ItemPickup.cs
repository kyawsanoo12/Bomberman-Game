using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType { blastRadius, extraBomb, increaseSpeed };
    public ItemType type;

    private void OnItemPickUp(GameObject player)
    {
        switch (type)
        {
            case ItemType.blastRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.extraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.increaseSpeed:
                player.GetComponent<MovementController>().speed++;
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickUp(other.gameObject);
        }
    }
}
