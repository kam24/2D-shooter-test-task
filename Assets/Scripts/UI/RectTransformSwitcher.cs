using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RectTransformSwitcher : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private SwitchAction _switchAction;

    private enum SwitchAction
    {
        Enable,
        Disable
    }

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        switch (_switchAction)
        {
            case SwitchAction.Enable:
                _rectTransform.gameObject.SetActive(true);
                break;
            case SwitchAction.Disable:
                _rectTransform.gameObject.SetActive(false);
                break;
        }
    }
}

