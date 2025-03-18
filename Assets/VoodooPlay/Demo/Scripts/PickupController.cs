using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupController : MonoBehaviour
{  
    public int PickupValue; // the value of the pickup
    public Vector2 minMaxTime; // random time between x and y    
    private Vector3 startPos; // cache the start position
    private float timePeriod; // the time it will travel
    public float height = 350f; // the hight it will travel
    private float timeSinceStart;
    private Transform thisTrans;


    private void OnEnable()
    {
        timePeriod = Random.Range(minMaxTime.x, minMaxTime.y);
        thisTrans = GetComponent<Transform>();
        startPos = thisTrans.position;
    }

    // move the pickup object up and down
    void Update()
    {
        Vector3 nextPos = transform.position;
        nextPos.y = startPos.y + height + height * Mathf.Sin(((Mathf.PI * 2) / timePeriod) * timeSinceStart);
        timeSinceStart += Time.deltaTime;
        transform.position = nextPos;
    }

    //return the pick up value when thre player colid with it.
    public int GetValue(){
        PoolManager.instance.Despawn(this.gameObject);
        return PickupValue;
    }


}
