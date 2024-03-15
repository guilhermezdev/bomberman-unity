using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupController : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().bombsCount++;
                break;
            case ItemType.BlastRadius:
                // TODO: implement
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<PlayerController>().runSpeed++;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            OnItemPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
