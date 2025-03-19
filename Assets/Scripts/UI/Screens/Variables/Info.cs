using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Info : BasicScreen
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _profileButton;

    private void Start()
    {
        _profileButton.onClick.AddListener(PlyerPressed);
        _homeButton.onClick.AddListener(HomePressed);
        _shopButton.onClick.AddListener(ShopPressed);
    }

    private void OnDestroy()
    {
        _profileButton.onClick.RemoveListener(PlyerPressed);
        _homeButton.onClick.RemoveListener(HomePressed);
        _shopButton.onClick.RemoveListener(ShopPressed);
    }
    public override void ResetScreen()
    {

    }

    public override void SetScreen()
    { 
    }
    private void HomePressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void PlyerPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Player);
    }

    private void ShopPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Shop);
    }
}
