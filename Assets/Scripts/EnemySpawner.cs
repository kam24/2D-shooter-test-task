using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _range;
    [SerializeField] private int _enemyCount;
    [SerializeField] private GameObjectFactory _factory;

    private void Start()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            _factory.InstantiateMonster(GetRandomPosition());
        }
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(-_range.x, _range.x);
        float y = Random.Range(-_range.y, _range.y);
        return new Vector2(x, y);
    }
}

