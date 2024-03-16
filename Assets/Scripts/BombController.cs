using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    public GameObject explosionCenterPrefab;
    public GameObject explosionMidPrefab;
    public GameObject explosionEndPrefab;

    public GameObject brickDestroyPrefab;

    public LayerMask stageLayer;

    private Tilemap destructibleTilemap;

    public int bombRadius = 1;

    private void Start()
    {
        destructibleTilemap = GameObject.FindWithTag("Destructible").GetComponent<Tilemap>();
        Debug.Log(destructibleTilemap);
    }

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
            DestroyTile(position);
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

    private void DestroyTile(Vector2 position)
    {
        Vector3Int cell = destructibleTilemap.WorldToCell(position);
        TileBase tile = destructibleTilemap.GetTile(cell);

        if(tile != null)
        {
            destructibleTilemap.SetTile(cell, null);
            Instantiate(brickDestroyPrefab, position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
}
