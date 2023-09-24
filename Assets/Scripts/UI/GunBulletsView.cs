using System;
using TMPro;
using UnityEngine;

[RequireComponent (typeof(TextMeshProUGUI))]
public class GunBulletsView : MonoBehaviour
{
    private TextMeshProUGUI _textField;
    private Inventory _inventory;

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
        _inventory.CellChanged += OnShot;

        _textField = GetComponent<TextMeshProUGUI>();

        OnShot();
    }

    private void OnShot()
    {
        _textField.text = _inventory.GetBulletsCount().ToString();
    }

    public void OnDestroy()
    {
        _inventory.CellChanged -= OnShot;
    }
}

