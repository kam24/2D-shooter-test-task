using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCellView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private RectTransform _itemView;
    [SerializeField] private Button _itemButton;

    public event Action<InventoryCell> ItemClicked;

    public InventoryCell InventoryCell { get; private set; }

    private void Awake()
    {
        _itemButton.onClick.AddListener(OnClicked);
    }

    private void OnDestroy()
    {
        _itemButton.onClick.RemoveListener(OnClicked);
    }

    public void Render(InventoryCell cell)
    {
        InventoryCell = cell;

        _itemName.text = cell.Item.Name;
        _itemIcon.sprite = Root.ItemsStorage.GetLoot(cell.Item).Icon;

        if (cell.Count > 1)
            _counter.text = cell.Count.ToString();
        else
            _counter.text = null;

        _itemView.gameObject.SetActive(true);
    }

    public void Reset()
    {
        InventoryCell = null;
        _itemName.text = null;
        _counter.text = null;
        _itemIcon.sprite = null;
        _itemView.gameObject.SetActive(false);
    }

    private void OnClicked()
    {
        if (InventoryCell != null)
            ItemClicked?.Invoke(InventoryCell);
    }
}
