using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Main_Page : MonoBehaviour
{
    [SerializeField]
    private Button _mainMenuButton;
    [SerializeField]
    private Button _soundButton;
    [SerializeField]
    private Button _helpButton;
    [SerializeField]
    private Button _supportButton;
    [SerializeField]
    private Button _cameraButton;
    [SerializeField]
    private GameObject _buttonsHolder;
    [SerializeField]
    private GameObject _cameraShape;
    [SerializeField]
    private GameObject _sharePage;
    [SerializeField]
    private GameObject _helpPage;
    [SerializeField]
    private GameObject _uiFader;
    [SerializeField]
    private NativeToolController _nativeController;
    [SerializeField]
    private AudioSource _audioSource;


    private Dictionary<string, Sprite> _uiSprites;
    private Image _mainImage;
    private Image _soundImage;
    private bool _isSoundOn;
    private bool _isActiveHolder;

    private void Awake()
    {
        _isActiveHolder = false;
        _isSoundOn = true;
        _uiSprites = new Dictionary<string, Sprite>();
        Sprite[] temp = Resources.LoadAll<Sprite>("UI");
        foreach (Sprite val in temp)
        {
            _uiSprites.Add(val.name, val);
        }
        _mainImage = _mainMenuButton.GetComponent<Image>();
        _soundImage = _soundButton.GetComponent<Image>();

        _mainMenuButton.onClick.AddListener(OnMainMenuBtn);
        _soundButton.onClick.AddListener(OnSoundBtn);
        _helpButton.onClick.AddListener(OnHelpBtn);
        _supportButton.onClick.AddListener(OnSupportBtn);
        _cameraButton.onClick.AddListener(OnCameraShotBtn);

        AdaptivePage();
    }

    private void OnMainMenuBtn()
    {
        if (_isActiveHolder)
        {
            _buttonsHolder.SetActive(false);
            _isActiveHolder = false;
            _mainImage.sprite = _uiSprites["Gamburger"];
        }
        else
        {
            _buttonsHolder.SetActive(true);
            _isActiveHolder = true;
            _mainImage.sprite = _uiSprites["Ex"];
        }
        _mainImage.SetNativeSize();
    }

    private void OnSoundBtn()
    {
        if (_isSoundOn)
        {
            _isSoundOn = false;
            _soundImage.sprite = _uiSprites["SoundOff"];
            _audioSource.mute = true;
        }
        else
        {
            _isSoundOn = true;
            _soundImage.sprite = _uiSprites["SoundOn"];
            _audioSource.mute = false;
        }
        _soundImage.SetNativeSize();
    }

    private void OnHelpBtn()
    {
        _helpPage.SetActive(true);
    }

    private void OnSupportBtn()
    {
        _nativeController.Support();
    }

    private void OnCameraShotBtn()
    {
        _uiFader.SetActive(false);
        StartCoroutine(OpenSharePage());
        _nativeController.CameraShot();
    }

    private IEnumerator OpenSharePage()
    {
        yield return new WaitForSeconds(0.25f);
        _sharePage.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _uiFader.SetActive(true);
    }

    private void AdaptivePage()
    {
        float height = Screen.height / 5;
        RectTransform shape = _cameraShape.GetComponent<RectTransform>();
        shape.sizeDelta = new Vector2(shape.sizeDelta.x, height);

        if(Screen.height + Screen.width > 2000)
        {
            _cameraButton.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
        }
    }
}
