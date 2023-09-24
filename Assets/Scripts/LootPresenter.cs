using Assets.Scripts;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class LootPresenter : MonoBehaviour
{
    public InventoryItem Item { get; private set; }

    public void Initialize(Loot loot)
    {
        Item = new InventoryItem(loot.Id, loot.Name);
        gameObject.name = loot.Name;
        GetComponent<SpriteRenderer>().sprite = loot.Icon;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
            Destroy(gameObject);
    }
}

