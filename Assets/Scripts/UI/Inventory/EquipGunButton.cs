using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EquipGunButton : MonoBehaviour
{
    private Button _button;
    private InventoryCell _cell;
    private Loot _pistol;
    private Loot _rifle;
    private InventoryView _inventoryView;

    public void Initialize(InventoryView inventoryView, Loot rifle, Loot pistol)
    {
        _pistol = pistol;
        _rifle = rifle;
        _inventoryView = inventoryView;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void Set(InventoryCell cell)
    {
        _cell = cell;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        WeaponCode weapon = WeaponCode.None;
        if (_cell.Item.Id == _rifle.Id)
            weapon = WeaponCode.Rifle;
        else if (_cell.Item.Id == _pistol.Id)
            weapon = WeaponCode.Pistol;
        _inventoryView.EquipWeapon(weapon);
    }
}

