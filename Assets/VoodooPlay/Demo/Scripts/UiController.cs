using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiController : Singleton<UiController>
{

    public enum state { mainMenu,gameRun,gameOver}

    public state gameState;

    [Header("Panels")]
    public GameObject GameUI;
    public GameObject GameMenu;    
    public GameObject GameOver;


    [Header("Labels")]
    public TextMeshProUGUI DistanceText;    
    public TextMeshProUGUI BombsText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI BestScoreText;

    public void ShowMenu(){
        gameState = state.mainMenu;
        GameUI.SetActive(false);
        GameMenu.SetActive(true);        
        GameOver.SetActive(false);
    }

    public void ShowGameControls(){
        gameState = state.gameRun;
        GameUI.SetActive(true);
        GameMenu.SetActive(false);        
        GameOver.SetActive(false);
    }

        public void ShowGameOver(){
        gameState = state.gameOver;
        GameUI.SetActive(false);
        GameMenu.SetActive(false);        
        GameOver.SetActive(true);
    }

    
    public void UpdateDistance(bool _pulse){
        DistanceText.text = GameController.instance.Score.ToString();  
    }

    public void UpdateBombs(int _count){
        BombsText.text = _count.ToString();  

    }

     public void UpdateHealth(int _count){
        HealthText.text = _count.ToString();  
    }

    public void UpdateBestScore(int _score){
        BestScoreText.text= _score.ToString();
    }

    private void Update()
    {
        if (gameState==state.gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            gameState = state.mainMenu;
            GameController.instance.ResetGame();        
        }

        if (gameState == state.mainMenu && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameController.instance.MenuChangePlane(-1);
        }
        if (gameState == state.mainMenu && Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameController.instance.MenuChangePlane(1);
        }
        if (gameState == state.mainMenu && Input.GetKeyDown(KeyCode.Return) && !GameController.instance.GameRun)
        {
            gameState = state.gameRun;
            ShowGameControls();
            GameController.instance.GameRun = true;
        }

    }

}
