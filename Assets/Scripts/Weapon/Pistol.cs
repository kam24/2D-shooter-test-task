using Assets.Scripts;
using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Pistol : Weapon
{
    private bool _triggerPressed;

    public override void Initialize(Transform ownerCenter, Gun gun)
    {
        base.Initialize(ownerCenter, gun);
        _gun.ShotCanceled += OnShotCanceled;
        _triggerPressed = false;
    }

    public override void TryShoot()
    {
        if (_triggerPressed == true)
            return;

        _triggerPressed = true;

        base.TryShoot();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _gun.ShotCanceled -= OnShotCanceled;
    }

    private void OnShotCanceled()
    {
        _triggerPressed = false;
    }
}

