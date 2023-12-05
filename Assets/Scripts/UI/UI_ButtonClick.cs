using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClick : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioClip _clickSound;

    public static event Action<AudioClip> OnClickSound;

    private void OnEnable() => 
        _button.onClick.AddListener(ClickButton);

    private void OnDisable() => 
        _button.onClick.RemoveAllListeners();

    private void ClickButton()
    {
        if (_clickSound)
        {
            OnClickSound?.Invoke(_clickSound);
        }
    }
}
