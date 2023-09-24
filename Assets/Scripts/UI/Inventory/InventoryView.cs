using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventoryCellView _cellViewTemplate;
    [SerializeField] private RectTransform _cellViewParent;
    [SerializeField][Min(0)] private int _cellsCount;
    [SerializeField] private DeleteItemButton _deleteItemButton;
    [SerializeField] private EquipGunButton _equipGunButton;
    [SerializeField] private Loot _pistol;
    [SerializeField] private Loot _rifle;

    private Inventory _inventory;
    private List<InventoryCellView> _cellViews;

    public event Action<WeaponCode> EquipedWeaponChanged;
    public event Action<WeaponCode> WeaponThrown;

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
        _deleteItemButton.Initialize(inventory, this, _rifle, _pistol);
        _equipGunButton.Initialize(this, _rifle, _pistol);
        _cellViews = new List<InventoryCellView>();
        for (int i = 0; i < _cellsCount; i++)
        {
            var cell = Instantiate(_cellViewTemplate, _cellViewParent);
            cell.ItemClicked += OnItemClicked;
            _cellViews.Add(cell);
        }
        _inventory.CellChanged += Render;
        Render();
    }

    public void EquipWeapon(WeaponCode weapon)
    {
        EquipedWeaponChanged?.Invoke(weapon);
    }

    public void ThrowWeapon(WeaponCode from)
    {
        WeaponThrown?.Invoke(from);
    }

    private void OnEnable()
    {
        _deleteItemButton.Button.onClick.AddListener(OnDeleteButtonClick);
    }

    private void OnDisable()
    {
        _deleteItemButton.Button.onClick.RemoveListener(OnDeleteButtonClick);
    }

    private void OnDestroy()
    {
        _inventory.CellChanged -= Render;
    }

    private void OnDeleteButtonClick()
    {
        _equipGunButton.gameObject.SetActive(false);
    }

    private void Render()
    {
        int index = 0;
        _cellViews.ForEach(view =>
        {
            if (index < _inventory.Cells.Count)
                view.Render(_inventory.Cells[index]);
            else
                view.Reset();

            index++;
        });
    }

    private void OnItemClicked(InventoryCell cell)
    {
        _deleteItemButton.Set(cell);
        _deleteItemButton.gameObject.SetActive(true);

        if (cell.Item.Id == _pistol.Id || cell.Item.Id == _rifle.Id)
        {
            _equipGunButton.Set(cell);
            _equipGunButton.gameObject.SetActive(true);
        }
        else
        {
            _equipGunButton.gameObject.SetActive(false);
        }
    }
}

