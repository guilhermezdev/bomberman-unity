using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{
    public GameObject explosionCenterPrefab;
    public GameObject explosionMidPrefab;
    public GameObject explosionEndPrefab;

    public LayerMask stageLayer;

    public int bombRadius = 1;

    private void OnDestroy()
    {
        Vector2 position = transform.position;

        CreateExplosion(explosionCenterPrefab, position, Vector2.zero);
        ExplodeBomb(position, Vector2.up, bombRadius);
        ExplodeBomb(position, Vector2.down, bombRadius);
        ExplodeBomb(position, Vector2.left, bombRadius);
        ExplodeBomb(position, Vector2.right, bombRadius);
    }
    private void ExplodeBomb(Vector2 position, Vector2 direction, int length)
    {
        if (length == 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2.0f, 0f, stageLayer))
        {
            return;
        }

        CreateExplosion(length > 1 ? explosionMidPrefab : explosionEndPrefab, position, direction);

        ExplodeBomb(position, direction, length - 1);

    }

    private void CreateExplosion(GameObject explosionPrefab, Vector2 position, Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);

        float angleDegrees = angle * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angleDegrees);

        Instantiate(explosionPrefab, position, rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        Debug.Log(collision.name);
        if(collision.tag == "Explosion")
        {
            Debug.Log("abacate");
            Destroy(gameObject);
        }
    }
}
