using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    
    private Rigidbody2D rbody2D;
    public GameObject hitVFX;
    public GameObject explotionVFX;


    private void OnEnable()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        Invoke(nameof(Despown), 1.5f);
    }
        

    public void SetupBullet(float _power, Vector3 _dir)
    {
        rbody2D.AddForce(_dir * _power,ForceMode2D.Impulse);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("ground") || collision.transform.CompareTag("cave"))
        {
            PoolManager.instance.Spawn(hitVFX.name, collision.transform.position, Quaternion.identity, false);
            Despown();
        }
        if (collision.transform.CompareTag("Bird"))
        {
            PoolManager.instance.Spawn("FX_feather", collision.transform.position, Quaternion.identity, false);
            PoolManager.instance.Despawn(collision.gameObject);
            GameController.instance.BirdCount++;
            GameController.instance.Score = 3;
            Despown();
        }

        if (collision.transform.CompareTag("Tank"))
        {
            PoolManager.instance.Spawn (explotionVFX.name, collision.transform.position, Quaternion.identity, false);
            PoolManager.instance.Despawn(collision.gameObject);
            GameController.instance.TanksCount++;
            GameController.instance.Score = 10;
            Despown();
        }

        if (collision.transform.CompareTag("Soldier"))
        {
            PoolManager.instance.Spawn (hitVFX.name, collision.transform.position, Quaternion.identity, false);
            PoolManager.instance.Despawn(collision.gameObject);
            GameController.instance.SoldierCount++;
            GameController.instance.Score = 5;
            Despown();
        }
    }

    private void Despown() {
        rbody2D.velocity = Vector3.zero;
        PoolManager.instance.Despawn(this.gameObject);
    }
}
