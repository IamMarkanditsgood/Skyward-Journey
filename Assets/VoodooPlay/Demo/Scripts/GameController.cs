using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Airplane{
    public GameObject GO;    
    public int maxBombs;
    public int maxHealth;
    public float shootRate;
    public float speed;
}

public class GameController : Singleton<GameController>
{

    public int TanksCount;
    public int BirdCount;
    public int SoldierCount;
    private int score;
    private int BestScore;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = score + value;
            if (value > 1)
            {
                UiController.instance.UpdateDistance(true);
            }
            else
            {
                UiController.instance.UpdateDistance(false);
            }

        }
    }



    public bool GameRun = false;
    // public bool PlayerStarted = false;

    public int BGindex = -1;
    private int LandIndex = 0;
    private int PlaneIndex=0;
    private int ChangeLandDistance;
    private int NextLandChange;
    private bool LandChanged = false;
    public GameObject[] GroundEnemies;
    public GameObject[] AirEnemies;
    public GameObject[] Pickups;
    public GameObject[] LandType;
    public GameObject[] CaveObjects;
    public GameObject[] Buildings;
    public Airplane[] planeList;
    private Transform CurrentPlane;


    // Start is called before the first frame update
    void Start()
    {
        ShowMenu();
    }

    // show the main menu with selected airplane
    public void ShowMenu()
    {
        CurrentPlane = PoolManager.instance.Spawn(planeList[PlaneIndex].GO.name, Vector3.zero, Quaternion.identity,true).transform;
        CameraFollow2D.instance.getTarget(CurrentPlane);
        CreateLevel();
        UiController.instance.ShowMenu();        
    }

    // reset the game status.
    public void ResetGame()
    {
        CameraFollow2D.instance.ResetCamera();
        BGindex = -1;
        LandIndex = 0;
        GameRun = false;        
        // this will trigger unity event that cause all object to despwan.
        EventManager.TriggerEvent("DespawnAll");
        TanksCount = 0;
        BirdCount = 0;
        SoldierCount = 0;
        if (score > BestScore)
        {
            BestScore = score;
            UiController.instance.UpdateBestScore(BestScore);
            score = 0;
        }
        else
        {
            score = 0;
        }

        ShowMenu();

    }

    public void StartGame()
    {
        UiController.instance.ShowGameControls();
    }

    public void CreateLevel()
    {
        ChangeLandDistance = Random.Range(2, 5);
        for (int i = 0; i < 8; i++)
        {
            PoolManager.instance.Spawn("BGBlock", new Vector3(BGindex * 1024f, 0, 0), Quaternion.identity,true);
            BGindex++;
        }
    }

    public void UpdateBG()
    {
        PoolManager.instance.Spawn(LandType[LandIndex].name, new Vector3(BGindex * 1024f, 0, 0), Quaternion.identity,true);
        BGindex++;

        if (!GameRun)
        {
            return;
        }

        if (Random.value < 0.35f)
        {
            AddPickups();
        }


        if (!LandChanged)
        {
            NextLandChange = ChangeLandDistance + BGindex;
            LandIndex = Random.Range(0, LandType.Length);
            LandChanged = true;
           
        }

        if (BGindex == NextLandChange)
        {
            ChangeLandDistance = Random.Range(3, 7);
            LandChanged = false;                    
        }

        if (LandIndex == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Random.value < 0.5f)
                {
                    AddAirEnemies();
                }
                if (Random.value < 0.5f)
                {
                    AddGroundEnemies();
                }
            }
        }

        if (LandIndex == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Random.value < 0.5f)
                {
                    AddAirEnemies();
                }
                 if(Random.value < 0.5f){
                     AddCaveParts();      
                 }            
            }
        }

        if (LandIndex == 2)
        {
             for (int i = 0; i < 5; i++)
            {
                if (Random.value < 0.5f)
                {
                    AddAirEnemies();
                }
                if (Random.value < 0.5f)
                {
                    AddGroundEnemies();
                }
            }

            AddBuildings();
        }
    }

    public void AddAirEnemies()
    {
        Vector3 pos = new Vector3((BGindex - 1) * 1024f, Random.Range(20f, 300f), 0);
        PoolManager.instance.Spawn(AirEnemies[Random.Range(0, AirEnemies.Length)].name, pos, Quaternion.identity,true);
    }

    public void AddGroundEnemies()
    {
        Vector3 pos = new Vector3((BGindex - 1) * 1024f + Random.Range(-500f, 500f), -230f, 0);
        PoolManager.instance.Spawn(GroundEnemies[Random.Range(0, GroundEnemies.Length)].name, pos, Quaternion.identity, true);
    }

    public void AddPickups()
    {
        Vector3 pos = new Vector3(BGindex* 1024f + Random.Range(-500f, 500f), -1000f, 0);
        PoolManager.instance.Spawn(Pickups[Random.Range(0, Pickups.Length)].name, pos, Quaternion.identity, true);
    }

    public void AddCaveParts()
    {   
        if(Random.value>0.5){
            Vector3 pos = new Vector3(BGindex * 1024f + Random.Range(-500f, 500f), Random.Range(500f, 550f), 0);
            PoolManager.instance.Spawn(CaveObjects[Random.Range(0, CaveObjects.Length)].name, pos, Quaternion.identity, true);            
        } else {
            Vector3 pos = new Vector3(BGindex * 1024f + Random.Range(-500f, 500f), Random.Range(-450f, -500f), 0);
            PoolManager.instance.Spawn(CaveObjects[Random.Range(0, CaveObjects.Length)].name, pos, Quaternion.Euler(0,0,180), true);   
        }        
    }

    public void AddBuildings(){
        for(int i=-450;i<450;i+=300){
            if(Random.value>0.5){
                Vector3 pos = new Vector3(BGindex * 1024f + i+ Random.Range(-20,20), -273, 0);
                PoolManager.instance.Spawn(Buildings[Random.Range(0, Buildings.Length)].name, pos, Quaternion.identity, true);            
            }
        }    
    }

    public void MenuChangePlane(int _indx){
        Vector3 tempPos = CurrentPlane.position;
        PoolManager.instance.Despawn(CurrentPlane.gameObject);
        PlaneIndex = PlaneIndex+_indx;
        if(PlaneIndex==planeList.Length){
            PlaneIndex = 0;
        }
        if(PlaneIndex<0){
            PlaneIndex = planeList.Length-1;
        }
//        Debug.Log(PlaneIndex);
        CurrentPlane = PoolManager.instance.Spawn(planeList[PlaneIndex].GO.name, tempPos, Quaternion.identity, true).transform;
        CurrentPlane.GetComponent<PlayerController>().airplane = planeList[PlaneIndex];
        CameraFollow2D.instance.getTarget(CurrentPlane);
    }
}
