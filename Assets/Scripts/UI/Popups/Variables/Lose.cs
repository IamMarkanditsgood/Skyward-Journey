using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Lose : BasicPopup
{
    [SerializeField] private TMP_Text _score;

    [SerializeField] private Button _playAgain;
    [SerializeField] private Button _home;


    private void Start()
    {
        _playAgain.onClick.AddListener(StartAgain);
        _home.onClick.AddListener(Home);
    }

    private void OnDestroy()
    {
        _playAgain.onClick.RemoveListener(StartAgain);
        _home.onClick.RemoveListener(Home);
    }
    public override void ResetPopup()
    {
       
    }

    public override void SetPopup()
    {
        _score.text = PlayerPrefs.GetInt("CurrentScore").ToString();
    }

    private void StartAgain()
    {
        SceneManager.LoadScene("Demo");
    }

    private void Home()
    {     
        SceneManager.LoadScene("MainMenu");
    }
}
