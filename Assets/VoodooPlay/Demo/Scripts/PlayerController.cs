using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using PathologicalGames;


public class PlayerController : MonoBehaviour {
    public Airplane airplane;
    public GameObject projectile;
    private Animator animator;
    private LineRenderer shootLine;
    private Transform thisTrans;
    private Rigidbody2D rgdbody2D;

    private PolygonCollider2D polyCol2D;
    public Transform BombSpawn;
    public Transform GunsSpawn;
    private float Speed;    
    private int Health;
    private int Dir =1;
    public TextMeshProUGUI text;    
    private int currentBombs;
    public LayerMask shootMask;   
    public float shootRecover = 0.5f;
    private float shootTime;
    private float shootDistance = 1000f;
    private bool PlayerDead = false;

    [Header("Visual FX")]
    public GameObject explotionVFX;
    public GameObject feathersVFX;
    //  private int currentPosIndex;

    // private bool startPlay = false;

    private RaycastHit2D hit;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        shootLine = GetComponent<LineRenderer>();
        thisTrans = GetComponent<Transform>();  
        rgdbody2D = GetComponent<Rigidbody2D>();  
        polyCol2D = GetComponent<PolygonCollider2D>();                    
    }

    private void OnEnable()
    {
        Init();
    }


    // setup the airplane
    private void Init()
     {
        PlayerDead = false;
        polyCol2D.isTrigger = true;
        rgdbody2D.isKinematic = true;   
        currentBombs = airplane.maxBombs;
        Health = airplane.maxHealth;   
        Speed = airplane.speed;   
        UiController.instance.UpdateBombs(airplane.maxBombs);   
        UiController.instance.UpdateHealth(Health);         
    }

        
    void Update()
    { 
        // player movment (should work also in Menu mode)
		thisTrans.Translate(Vector3.right * Speed * Time.deltaTime);

        if (!GameController.instance.GameRun)
        {
            return;
        }

        // kill player if collide the sky
        if (thisTrans.position.y > 700f || PlayerDead)
        {
            AudioController.instance.ExplodeAFX();
            GetComponent<Rigidbody2D>().isKinematic = false;
            PlayerDead = true;
            GameController.instance.GameRun = false;
            if ((thisTrans.right.x < -0.1 || thisTrans.right.x > 0.1))
            {
                thisTrans.Rotate(0, 0, -60f * Dir * Time.deltaTime);
            }

            return;
        }

        // fly input
        if (Input.GetKey(KeyCode.Space) )
        {
            float angle = thisTrans.localRotation.eulerAngles.z;
             if((angle>0f && angle<70) || (angle>270f && angle<360)){
                thisTrans.Rotate(0, 0, 150f * Dir * Time.deltaTime);
            }
        }    

        // drop bomob input
        if(Input.GetKeyDown(KeyCode.D) ){
            if(currentBombs>0){
                DropBomb();
                AudioController.instance.DropAFX();
            }           
        }

        // shotting delay timer
        shootTime += + Time.deltaTime;

        // shooting input
        if(Input.GetKey(KeyCode.RightControl) ){  
            if(shootTime>airplane.shootRate){
                shootTime = 0;
                ShootGuns();  
            }      
                
        }       
        if((thisTrans.right.x <-0.1 || thisTrans.right.x >0.1) ){           
                thisTrans.Rotate (0,0 ,-60f * Dir * Time.deltaTime);         	    	
		}        
    }

    // collision check
    private void OnTriggerEnter2D(Collider2D other) {      
    
        if(other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("cave") || other.gameObject.CompareTag("EnemyBuilding"))
        {
            PoolManager.instance.Spawn(explotionVFX.name,transform.position,Quaternion.identity,false);
            AudioController.instance.ExplodeAFX();
            polyCol2D.isTrigger = false;
            rgdbody2D.isKinematic = false;
            UpdateHealth(-1000);           
            Invoke(nameof(Gameover),4f);
            if (this.gameObject.activeSelf) {
                PoolManager.instance.Despawn(this.gameObject);                
            }
        }     
        if(other.gameObject.CompareTag("Bird"))
        {
            AudioController.instance.DuckAFX();
            PoolManager.instance.Spawn (feathersVFX.name, other.transform.position, Quaternion.identity, false);                    
            PoolManager.instance.Despawn(other.gameObject);
            GameController.instance.BirdCount++; 
            UpdateHealth(-1);
         }  

        if(other.gameObject.CompareTag("pickup"))
        {
            AudioController.instance.PickupAFX();    
            currentBombs= Mathf.Min(currentBombs+ other.GetComponent<PickupController>().GetValue(),airplane.maxBombs);
            UiController.instance.UpdateBombs(currentBombs);
         }

        if(other.gameObject.CompareTag("health"))
        {
            AudioController.instance.PickupAFX();
            UpdateHealth(other.GetComponent<PickupController>().GetValue());
        }
    }

    // updtae player health
    private void UpdateHealth(int _amount){
        Health = Mathf.Clamp(Health + _amount,0,airplane.maxHealth);
        if(Health==0){
            PoolManager.instance.Spawn(explotionVFX.name, transform.position,Quaternion.identity, false);
            polyCol2D.isTrigger = false;
            rgdbody2D.isKinematic = false;
            GameController.instance.GameRun = false;
            PlayerDead = true;
            Invoke(nameof(Gameover), 3f);
        }        
        UiController.instance.UpdateHealth(Health);
    }

    private void Gameover(){
        CancelInvoke();
        Speed = 0;
        UiController.instance.ShowGameOver();
    }
       

    private void DropBomb(){
        AudioController.instance.DropAFX();
        PoolManager.instance.Spawn("bomb",BombSpawn.position,Quaternion.identity,false);	
        currentBombs--;
        UiController.instance.UpdateBombs(currentBombs);
    }

    private void ShootGuns(){
        AudioController.instance.ShootAFX();
        projectile = PoolManager.instance.Spawn(projectile.name, GunsSpawn.position, Quaternion.identity, false);
        projectile.GetComponent<ProjectileController>().SetupBullet(1200f, thisTrans.right);

    }
}
