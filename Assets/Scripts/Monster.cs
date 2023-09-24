using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : Character
{
    [SerializeField][Min(0)] private float _searchRadius;
    [SerializeField][Min(0)] private float _attackRadius;
    [SerializeField][Min(0)] private float _damage;
    [SerializeField][Min(0)] private float _attackTime;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private List<Loot> _lootList;

    private GameObjectFactory _factory;
    private Character _target;
    private bool _targetFound;
    private Coroutine _attackingTarget;

    public void Initialize(GameObjectFactory factory)
    {
        _factory = factory;
    }

    protected override void OnDying()
    {
        InstantiateLoot();
        base.OnDying();
        Destroy(gameObject);
    }

    private void Awake()
    {
        Initialize(new Health(4));
    }

    private void Update()
    {
        if (_targetFound)
        {
            if (IsTargetInCircle(_searchRadius) == false)
            {
                ResetTarget();
                return;
            }
            if (_attackingTarget == null && IsTargetInCircle(_attackRadius))
                _attackingTarget = StartCoroutine(AttackTarget());

            if (_attackingTarget == null)
                Move(_target.transform.position - transform.position);
        }
        else
        {
            RaycastHit2D targetHit = TryFindTarget();
            if (targetHit.collider != null && targetHit.transform.gameObject.TryGetComponent(out Player target))
            {
                _target = target;
                _target.Health.Dying += ResetTarget;
                _targetFound = true;
            }
        }
    }

    private RaycastHit2D TryFindTarget()
    {
        return Physics2D.CircleCast(transform.position, _searchRadius, Vector2.up, _targetMask);
    }

    private bool IsTargetInCircle(float radius)
    {
        float distanceToTarget = (_target.transform.position - transform.position).magnitude;
        return distanceToTarget <= radius;
    }

    private void ResetTarget()
    {
        _target.Health.Dying -= ResetTarget;
        _target = null;
        _targetFound = false;
        if (_attackingTarget != null)
            StopCoroutine(_attackingTarget);
    }

    private void InstantiateLoot()
    {
        int randomIndex = UnityEngine.Random.Range(0, _lootList.Count);
        Loot randomLoot = _lootList[randomIndex];
        _factory.InstantiateLoot(transform.position, randomLoot);
    }

    private IEnumerator AttackTarget()
    {
        while (IsTargetInCircle(_attackRadius))
        {
            _target.ApplyDamage(_damage);
            yield return new WaitForSeconds(_attackTime);
        }
        _attackingTarget = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
