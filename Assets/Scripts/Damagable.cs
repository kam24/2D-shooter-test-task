using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Damagable : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _blood;
        public Health Health { get; private set; }

        public void Initialize(Health health)
        {
            Health = health;
            Health.Dying += OnDying;
        }

        protected virtual void OnDying()
        {
            Health.Dying -= OnDying;
        }

        public void ApplyDamage(float value)
        {
            Instantiate(_blood, transform.position, Quaternion.identity);
            Health.Decrease(value);
        }
    }
}
