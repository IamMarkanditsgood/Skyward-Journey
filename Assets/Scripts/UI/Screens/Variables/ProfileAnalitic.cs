using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileAnalitic : BasicScreen
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Button _playerEditor;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _totalDistance;
    [SerializeField] private TMP_Text _bestDistance;
    [SerializeField] private TMP_Text _totalCrashes;

    [SerializeField] private Image _achievement;

    [SerializeField] private Sprite _openedAchievement;

    [SerializeField] private AvatarManager avatarManager;

    private void Start()
    {
        _infoButton.onClick.AddListener(InfoPressed);
        _homeButton.onClick.AddListener(HomePressed);
        _shopButton.onClick.AddListener(ShopPressed);
        _playerEditor.onClick.AddListener(PlayerEditor);
    }

    private void OnDestroy()
    {
        _infoButton.onClick.RemoveListener(InfoPressed);
        _homeButton.onClick.RemoveListener(HomePressed);
        _shopButton.onClick.RemoveListener(ShopPressed);
        _playerEditor.onClick.RemoveListener(PlayerEditor);
    }
    public override void ResetScreen()
    {

    }

    public override void SetScreen()
    {
        avatarManager.SetSavedPicture();
        if (!PlayerPrefs.HasKey("Name"))
        {
            PlayerPrefs.SetString("Name", "UserName");
        }
        _name.text = PlayerPrefs.GetString("Name");
        _totalDistance.text = PlayerPrefs.GetInt("TotalDistance").ToString();
        _bestDistance.text = PlayerPrefs.GetInt("BestDistance").ToString();
        _totalCrashes.text = PlayerPrefs.GetInt("TotalCrashes").ToString();

        SetAchievements();
    }

    private void SetAchievements()
    {
        if (PlayerPrefs.GetInt("Achieve") == 1)
        {
            _achievement.sprite = _openedAchievement;
        }
    }

    private void HomePressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void InfoPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }

    private void ShopPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Shop);
    }

    private void PlayerEditor()
    {
        UIManager.Instance.ShowPopup(PopupTypes.PlayerEditor);
    }
}
