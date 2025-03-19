using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : BasicScreen
{
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Button _profileButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _mainButton;

    [SerializeField] private Image _palne;
    [SerializeField] private Image _infoPlane;

    [SerializeField] private Sprite[] _planeSprites;
    [SerializeField] private Sprite[] _planeInfoSprites;
    [SerializeField] private Sprite _defaultButton;
    [SerializeField] private Sprite _useButton;
    [SerializeField] private Sprite[] _buyButton;
    [SerializeField] private int[] _prices;

    private int currentPlane;


    private void Start()
    {
        if (PlayerPrefs.GetInt($"Plane{0}") != 1)
        {
            PlayerPrefs.SetInt($"Plane{0}", 1);
        }

        _profileButton.onClick.AddListener(PlyerPressed);
        HomeButton.onClick.AddListener(HomePressed);
        _infoButton.onClick.AddListener(InfoPressed);
        _mainButton.onClick.AddListener(MainButton);
        _nextButton.onClick.AddListener(Next);
        _prevButton.onClick.AddListener(Prev);
    }

    private void OnDestroy()
    {
        _profileButton.onClick.RemoveListener(PlyerPressed);
        HomeButton.onClick.RemoveListener(HomePressed);
        _infoButton.onClick.RemoveListener(InfoPressed);
        _mainButton.onClick.RemoveListener(MainButton);
        _nextButton.onClick.RemoveListener(Next);
        _prevButton.onClick.RemoveListener(Prev);
    }

    public override void ResetScreen()
    {

    }

    public override void SetScreen()
    {
        currentPlane = PlayerPrefs.GetInt("Plane");
        SetImage();
    }

    private void SetImage()
    {
        _palne.sprite = _planeSprites[currentPlane];
        _infoPlane.sprite = _planeInfoSprites[currentPlane];
        if (PlayerPrefs.GetInt($"Plane{currentPlane}") == 1)
        {
            if(PlayerPrefs.GetInt("Plane") == currentPlane)
            {
                _mainButton.GetComponent<Image>().sprite = _defaultButton;
            }
            else
            {
                _mainButton.GetComponent<Image>().sprite = _useButton;
            }
        }
        else
        {
            _mainButton.GetComponent<Image>().sprite = _buyButton[currentPlane];
        }
    }

    private void Next()
    {
        if (currentPlane + 1 < _planeSprites.Length)
        {
            currentPlane++;
        }
        else
        {
            currentPlane = 0;
        }
        SetImage();
    }

    private void Prev()
    {
        if (currentPlane > 0)
        {
            currentPlane--;
        }
        else
        {
            currentPlane = _planeSprites.Length - 1;
        }
        SetImage();
    }
    private void MainButton()
    {
        if (PlayerPrefs.GetInt($"Plane{currentPlane}") == 1)
        {
            if (PlayerPrefs.GetInt("Plane") != currentPlane)
            {
                PlayerPrefs.GetInt("Plane", currentPlane);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Score") >= _prices[currentPlane])
            {
                PlayerPrefs.SetInt($"Plane{currentPlane}", 1);
            }
        }
        SetImage();
    }

    private void HomePressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void PlyerPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Player);
    }

    private void InfoPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }
}
