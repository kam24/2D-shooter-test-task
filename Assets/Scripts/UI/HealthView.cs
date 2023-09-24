using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private Damagable _damagable;

    private Health _health => _damagable.Health;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _health.Changed += OnHealthChanged;
        OnHealthChanged();
    }

    private void OnHealthChanged()
    {
        _slider.value = _health.Value / _health.MaxValue;
    }
}