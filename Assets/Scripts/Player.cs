using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Pistol _pistol;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private SpriteRenderer _head;

    public event Action<InventoryItem> CollectingLoot;

    private WeaponCode _currentWeapon;

    public void Initialize(Health health, Gun gun)
    {
        base.Initialize(health);
        _pistol.Initialize(this.transform, gun);
        _rifle.Initialize(this.transform, gun);
    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        FlipHead(direction);
    }

    public void SwitchWeapon(WeaponCode weapon)
    {
        switch (weapon)
        {
            case WeaponCode.None:
                SetNoneWeapon();
                break;
            case WeaponCode.Pistol:
                _currentWeapon = WeaponCode.Pistol;
                _pistol.gameObject.SetActive(true);
                _rifle.gameObject.SetActive(false);
                break;
            case WeaponCode.Rifle:
                _currentWeapon = WeaponCode.Rifle;
                _pistol.gameObject.SetActive(false);
                _rifle.gameObject.SetActive(true);
                break;
        }
    }

    public void ThrowWeapon(WeaponCode from)
    {
        if (from == _currentWeapon)
            SetNoneWeapon();
    }

    private void SetNoneWeapon()
    {
        _currentWeapon = WeaponCode.None;
        _pistol.gameObject.SetActive(false);
        _rifle.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LootPresenter lootPresenter))
            CollectingLoot?.Invoke(lootPresenter.Item);
    }

    private void FlipHead(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        _head.flipX = angle <= 0;
    }
}