using Assets.Scripts;
using System;

public class Gun
{
    public event Action Shot;
    public event Action TryingShot;
    public event Action ShotCanceled;

    public int Bullets => _inventory.GetBulletsCount();
    public bool HasBullets => _inventory.GetBulletsCount() > 0;

    private Inventory _inventory;

    public Gun(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void TryShoot()
    {
        TryingShot?.Invoke();
    }

    public void CancelShoot()
    {
        ShotCanceled?.Invoke();
    }

    public void Shoot()
    {
        if (HasBullets == false)
            throw new InvalidOperationException();

        _inventory.RemoveBullet();
        Shot?.Invoke();
    }
}