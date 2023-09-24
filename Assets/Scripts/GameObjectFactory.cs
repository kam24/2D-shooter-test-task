using Assets.Scripts;
using UnityEngine;

public class GameObjectFactory : MonoBehaviour
{
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private LootPresenter _lootTemplate;
    [SerializeField] private Monster _monsterTemplate;

    public Player InstantiatePlayer(Vector3 position, Health health, Gun gun)
    {
        Player player = Instantiate(_playerTemplate, position, Quaternion.identity);
        player.Initialize(health, gun);
        return player;
    }

    public LootPresenter InstantiateLoot(Vector3 position, Loot loot)
    {
        LootPresenter lootPresenter = Instantiate(_lootTemplate, position, Quaternion.identity);
        lootPresenter.Initialize(loot);
        return lootPresenter;
    }

    public Monster InstantiateMonster(Vector3 position)
    {
        Monster monster = Instantiate(_monsterTemplate, position, Quaternion.identity);
        monster.Initialize(this);
        return monster;
    }
}

