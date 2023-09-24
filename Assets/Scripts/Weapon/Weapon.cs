using Assets.Scripts;
using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Weapon: MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;
    [SerializeField][Min(0)] private float _radius;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField][Min(0)] private float _damage;

    protected Gun _gun;
    private Transform _ownerCenter;
    private SpriteRenderer _spriteRenderer;

    public virtual void Initialize(Transform ownerCenter, Gun gun)
    {
        _gun = gun;
        _ownerCenter = ownerCenter;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _gun.TryingShot += TryShoot;
    }

    protected virtual void OnDisable()
    {
        _gun.TryingShot -= TryShoot;
    }

    private void Update()
    {
        RotateToTarget();
    }

    public virtual void TryShoot()
    {
        if (_gun.HasBullets == false)
            return;

        Damagable damagableTarget = FindTarget();
        if (damagableTarget != null)
        {
            _gun.Shoot();
            damagableTarget.ApplyDamage(_damage);
        }
    }

    private Damagable FindTarget()
    {
        var targets = Physics2D.CircleCastAll(_ownerCenter.position, _radius, Vector2.zero, Mathf.Infinity, _targetMask);
        RaycastHit2D targetHit;

        if (targets == null || targets.Length == 0)
        {
            return null;
        }
        else if (targets.Length == 1)
        {
            targetHit = targets[0];
        }
        else
        {
            float minTargetDistance = targets[0].distance;
            RaycastHit2D nearestTarget = targets[0];
            foreach (var target in targets)
            {
                if (target.distance < minTargetDistance)
                {
                    minTargetDistance = target.distance;
                    nearestTarget = target;
                }
            }
            targetHit = nearestTarget;
        }

        if (targetHit.transform.TryGetComponent(out Damagable damageable))
            return damageable;
        else
            throw new InvalidOperationException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_ownerCenter.position, _radius);
    }

    private void RotateToTarget()
    {
        var target = FindTarget();
        if (target == null)
            return;

        Vector2 targetPosition = target.transform.position;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float angleXY = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleXY);

        float angleYX = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        _spriteRenderer.flipY = angleYX <= 0;
    }
}

