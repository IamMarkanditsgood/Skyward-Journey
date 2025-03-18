using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collector : MonoBehaviour
{
    // this will despwan any objects in the left side of the screen.

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("BG"))
        {
            PoolManager.instance.Despawn(other.gameObject);
            // the it's a background object it will trigger spawn new one in the right side.
            GameController.instance.UpdateBG();
            // update the score counter +1
            if (GameController.instance.GameRun)
            {
                GameController.instance.Score = 1;
            }
        } else {
            // other objects just return to pool.
            PoolManager.instance.Despawn(other.gameObject);
        }


    }
}
