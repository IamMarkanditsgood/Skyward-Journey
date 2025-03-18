using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundUnitController : MonoBehaviour
{
    public Vector2 SpeedRange;
    public float HitForce;
    public float Delay;

    [Header("Visual FX")]
    public GameObject explotionVFX;
    
    private Rigidbody2D rgdbody2D;    
    private float speed;


    // Start is called before the first frame update

    private void OnEnable()
    {
        speed = Random.Range(SpeedRange.x, SpeedRange.y);
        if (rgdbody2D != null)
        {
            rgdbody2D.velocity = Vector2.zero;
        }
    }
    
    void Start()
    {    
        rgdbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rgdbody2D.AddForce(Vector2.left * speed);
        //  if(thisTrans.position.x<CamTran.position.x - 1000f){             
        //     PoolManager.Pools["pool"].Despawn(thisTrans);
        // }        
    }

    public void Gethit(){
        rgdbody2D.AddForce(Vector2.up * HitForce,ForceMode2D.Impulse);
        Invoke(nameof(DespawnSelf),Delay);

    }

    private void DespawnSelf(){
        PoolManager.instance.Spawn(explotionVFX.name, transform.position,Quaternion.identity,false);
        if (this.gameObject.activeSelf) {
          PoolManager.instance.Despawn(this.gameObject);
        }
        
    }
}
