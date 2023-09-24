using Assets.Scripts;
using UnityEngine;

public abstract class Character : Damagable
{
    [SerializeField][Min(0)] private float _speed;

    public virtual void Move(Vector2 direction)
    {
        transform.Translate(direction.normalized * _speed * Time.deltaTime);
    }

    protected override void OnDying()
    {
        base.OnDying();
    }
}

