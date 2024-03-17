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
        BombRemoteControl
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<PlaceBombController>().bombsCount++;
                break;
            case ItemType.BlastRadius:
                player.GetComponent<PlaceBombController>().bombRadius++;
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<PlayerController>().runSpeed++;
                break;
            case ItemType.BombRemoteControl:
                player.GetComponent<PlaceBombController>().isTimerBombEnabled = true;
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
