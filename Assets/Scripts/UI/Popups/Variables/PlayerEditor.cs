using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEditor : BasicPopup
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _avatarButton;

    [SerializeField] private TMP_InputField _name;

    [SerializeField] private AvatarManager avatarManager;

    private void Start()
    {
        _saveButton.onClick.AddListener(Savedata);
        _avatarButton.onClick.AddListener(avatarManager.PickFromGallery);
    }

    private void OnDestroy()
    {
        _saveButton.onClick.RemoveListener(Savedata);
        _avatarButton.onClick.RemoveListener(avatarManager.PickFromGallery);
    }
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
        avatarManager.SetSavedPicture();

        if (!PlayerPrefs.HasKey("Name"))
        {
            PlayerPrefs.SetString("Name", "UserName");
        }
        _name.text = PlayerPrefs.GetString("Name");
    }

    private void Savedata()
    {
        avatarManager.Save();
        PlayerPrefs.SetString("Name", _name.text);
        UIManager.Instance.ShowScreen(ScreenTypes.Player);

        Hide();
    }
}
