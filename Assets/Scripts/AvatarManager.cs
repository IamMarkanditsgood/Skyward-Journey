using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] private RawImage _avatarHome;
    [SerializeField] private RawImage _avatarProfile;
    [SerializeField] private RawImage _avatarEditor;

    [SerializeField] private Texture2D _basicAvatar;

    private int maxSize = 1000;
    private string _path;
    private Texture2D _texture;

    public void SetSavedPicture()
    {
        if (PlayerPrefs.HasKey("AvatarPath"))
        {
            string path = PlayerPrefs.GetString("AvatarPath");
            _avatarHome.texture = NativeGallery.LoadImageAtPath(path, maxSize);
            _avatarProfile.texture = NativeGallery.LoadImageAtPath(path, maxSize);
            _avatarEditor.texture = NativeGallery.LoadImageAtPath(path, maxSize);
        }
        else
        {
            _avatarHome.texture = _basicAvatar;
            _avatarProfile.texture = _basicAvatar;
            _avatarEditor.texture = _basicAvatar;
        }
    }

    public void PickFromGallery()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                _path = path;

                _texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (_texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                _avatarEditor.texture = _texture;
                _avatarHome.texture = _texture;
                _avatarProfile.texture = _texture;
            }
        }, "Select an image", "image/*");

        Debug.Log("Permission result: " + permission);
    }

    public void Save()
    {
        PlayerPrefs.SetString("AvatarPath", _path);
    }
}
