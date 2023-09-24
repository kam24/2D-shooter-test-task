using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [SerializeField] private GameObjectFactory _factory;
    [SerializeField] private LootCollection _lootCollection;
    [SerializeField] private Camera _camera;
    [SerializeField] private Loot _pistol;
    [Header("UI Elements")]
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private Button _shotButton;
    [SerializeField] private Button _retryButton;
    [SerializeField] private GunBulletsView _gunBulletsView;
    [SerializeField] private RectTransform _deathPanel;

    public static ItemsStorage ItemsStorage { get; private set; }

    private PlayerInputRouter _inputRouter;
    private Player _player;
    private Inventory _inventory;

    private InventoryDataHandler _inventoryDataHandler;

    private void Awake()
    {
        ItemsStorage = new ItemsStorage(_lootCollection);

        _inventoryDataHandler = new();
        List<InventoryCell> _inventoryCells = _inventoryDataHandler.LoadData();

        _inventory =
            _inventoryCells != null
            ? new Inventory(ItemsStorage.BulletsId, _inventoryCells)
            : new Inventory(ItemsStorage.BulletsId);

        if (_inventory.GetBulletsCount() == 0)
            AddBulletsToInventory(15);

        var pistol = _inventory.Cells.FirstOrDefault(x=> x.Item.Id == _pistol.Id);
        if (pistol == null)
            _inventory.AddItem(new InventoryItem(_pistol.Id, _pistol.Name), 1);


        CreatePlayer();

        _inventoryView.Initialize(_inventory);
        _gunBulletsView.Initialize(_inventory);

        _retryButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    private void AddBulletsToInventory(int count)
    {
        Loot bulletLoot = ItemsStorage.GetLoot(ItemsStorage.BulletsId);
        InventoryItem bulletItem = new(bulletLoot.Id, bulletLoot.Name);
        _inventory.AddItem(bulletItem, count);
    }

    private void CreatePlayer()
    {
        var gun = new Gun(_inventory);
        _player = _factory.InstantiatePlayer(Vector2.zero, new Health(10), gun);
        _camera.transform.SetParent(_player.transform);
        _inputRouter = new PlayerInputRouter(_player, _shotButton)
            .BindGun(gun);
        _player.SwitchWeapon(WeaponCode.Pistol);
    }

    private void OnEnable()
    {
        _inputRouter.OnEnable();
        _inventoryView.EquipedWeaponChanged += OnEquipedWeaponChanged;
        _inventoryView.WeaponThrown += OnWeaponThrown;
        _player.Health.Dying += OnPlayerDying;
        _player.CollectingLoot += OnCollectingLoot;
    }

    private void OnWeaponThrown(WeaponCode from)
    {
        _player.ThrowWeapon(from);
    }

    private void OnEquipedWeaponChanged(WeaponCode weapon)
    {
        _player.SwitchWeapon(weapon);
    }

    private void OnCollectingLoot(InventoryItem item)
    {
        _inventory.AddItem(item, 1);
    }

    private void OnPlayerDying()
    {
        _player.Health.Dying -= OnPlayerDying;
        _inputRouter.OnDisable();
        _deathPanel.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _inventoryView.EquipedWeaponChanged -= OnEquipedWeaponChanged;
        _inventoryView.WeaponThrown -= OnWeaponThrown;
        _player.Health.Dying -= OnPlayerDying;
        _player.CollectingLoot -= OnCollectingLoot;
        _inputRouter.OnDisable();
        _inventoryDataHandler.SaveData(_inventory.Cells);
    }

    private void Update()
    {
        _inputRouter.Update();
    }   
}
