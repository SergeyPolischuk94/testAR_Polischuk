using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Share_Page : MonoBehaviour
{
    [SerializeField]
    private Button _shareButton;
    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private Image _shot;
    [SerializeField]
    private GameObject _mainMenuPage;
    [SerializeField]
    private NativeToolController _nativeController;

    private void Awake()
    {
        _backButton.onClick.AddListener(OnBackBtn);
        _shareButton.onClick.AddListener(OnShareBtn);
    }

    private void OnEnable()
    {
        _shot.sprite = Sprite.Create(_nativeController.CameraShotTexture, new Rect(0, 0, _nativeController.CameraShotTexture.width,
            _nativeController.CameraShotTexture.height), new Vector2(0.5f, 0.5f));
    }

    private void OnBackBtn()
    {
        _mainMenuPage.SetActive(true);
        gameObject.SetActive(false);
        Destroy(_nativeController.CameraShotTexture);
    }

    private void OnShareBtn()
    {
        _nativeController.OnShareBtnClick();
    }
}
