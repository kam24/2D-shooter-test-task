using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DeleteItemButton : MonoBehaviour
{
    public Button Button { get; private set; }
    private Inventory _inventory;
    private InventoryView _inventoryView;
    private InventoryCell _cell;
    private Loot _pistol;
    private Loot _rifle;

    public void Initialize(Inventory inventory, InventoryView inventoryView, Loot rifle, Loot pistol)
    {
        _inventory = inventory;
        _inventoryView = inventoryView;
        _rifle = rifle;
        _pistol = pistol;
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnClick);
    }

    public void Set(InventoryCell cell)
    {
        _cell = cell;
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (_cell.Item.Id == _rifle.Id)
        {
            _inventoryView.ThrowWeapon(WeaponCode.Rifle);
        }
        else if (_cell.Item.Id == _pistol.Id)
        {
            _inventoryView.ThrowWeapon(WeaponCode.Pistol);
        }
        _inventory.RemoveItem(_cell.Item);
        _cell = null;
        gameObject.SetActive(false);
    }
}

