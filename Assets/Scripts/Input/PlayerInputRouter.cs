using Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputRouter
{
    private PlayerInput _input;
    private Player _player;
    private Gun _gun;

    public PlayerInputRouter(Player player, Button shotButton)
    {
        _input = new PlayerInput();
        _player = player;
    }

    public void OnEnable()
    {
        _input.Enable();
        _input.Player.Shooting.canceled += OnShootingCanceled;
    }

    private void OnShootingCanceled(InputAction.CallbackContext obj)
    {
        _gun.CancelShoot();
    }

    public void OnDisable()
    {
        _input.Disable();
        _input.Player.Shooting.canceled -= OnShootingCanceled;
    }

    public void Update()
    {
        if (_input.Player.enabled)
        {
            Vector2 inputVector = _input.Player.Movement.ReadValue<Vector2>();
            if (inputVector != Vector2.zero)
                _player.Move(inputVector);

            if (_input.Player.Shooting.IsPressed())
                OnShootingPerformed();
        }
    }

    public PlayerInputRouter BindGun(Gun gun)
    {
        _gun = gun;
        return this;
    }


    private void OnShootingPerformed(/*InputAction.CallbackContext obj*/)
    {
        _gun.TryShoot();
    }
}