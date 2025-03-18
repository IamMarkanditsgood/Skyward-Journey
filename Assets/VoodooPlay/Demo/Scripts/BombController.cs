using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombController : MonoBehaviour
{

    [Header("Visual FX")]
    public GameObject explotionVFX;
    public GameObject holeVFX;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 250f, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        PoolManager.instance.Spawn(explotionVFX.name, transform.position,Quaternion.identity,false);
        AudioController.instance.ExplodeAFX();
                   
        if(other.gameObject.CompareTag("ground")){            
            PoolManager.instance.Spawn(holeVFX.name, transform.position,Quaternion.identity,false);
        }   
        if(other.gameObject.CompareTag("Tank")){
            if (other.gameObject.activeSelf) {
                PoolManager.instance.Despawn(other.gameObject);
            }
            other.gameObject.GetComponent<GroundUnitController>().Gethit();
            GameController.instance.TanksCount++;
            GameController.instance.Score=10;
        }       

        if(other.gameObject.CompareTag("Soldier")){                       
            other.gameObject.GetComponent<GroundUnitController>().Gethit();
            GameController.instance.SoldierCount++;
            GameController.instance.Score=5;
        }

        if (this.gameObject.activeSelf) {
            PoolManager.instance.Despawn(this.gameObject);
        }
        
    }
}
