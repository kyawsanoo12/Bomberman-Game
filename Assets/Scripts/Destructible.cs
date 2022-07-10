
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float DestructibleTime = 1f;

    [Range(0f, 1f)]
    public float itemPickupChance = 0.2f;
    public GameObject[] itemPickUp;

    void Start()
    {
        Destroy(gameObject, DestructibleTime);
    }

    private void OnDestroy()
    {
        if (itemPickUp.Length > 0 && Random.value < itemPickupChance)
        {
            int indexValue = Random.Range(0, itemPickUp.Length);
            Instantiate(itemPickUp[indexValue], transform.position, Quaternion.identity);
        }
    }

}
