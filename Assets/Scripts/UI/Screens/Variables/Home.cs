using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Home : BasicScreen
{
    [SerializeField] private Button Shop;
    [SerializeField] private Button Info;
    [SerializeField] private Button Player;
    [SerializeField] private Button Play;

    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private AvatarManager avatarManager;


    private void Start()
    {

        Player.onClick.AddListener(PlyerPressed);
        Shop.onClick.AddListener(ShopPressed);
        Info.onClick.AddListener(InfoPressed);
        Play.onClick.AddListener(PlayPressed);
    }

    private void OnDestroy()
    {
        Player.onClick.RemoveListener(PlyerPressed);
        Shop.onClick.RemoveListener(ShopPressed);
        Info.onClick.RemoveListener(InfoPressed);
        Play.onClick.RemoveListener(PlayPressed);
    }

    public override void ResetScreen()
    {
 
    }

    public override void SetScreen()
    {
        avatarManager.SetSavedPicture();
        _score.text = PlayerPrefs.GetInt("Score").ToString();
        if(!PlayerPrefs.HasKey("Name"))
        {
            PlayerPrefs.SetString("Name", "UserName");
        }
        _name.text = PlayerPrefs.GetString("Name");
    }

    private void ShopPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Shop);
    }
    private void PlyerPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Player);
    }

    private void PlayPressed()
    {
        SceneManager.LoadScene("Demo");
    }

    private void InfoPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }
}
