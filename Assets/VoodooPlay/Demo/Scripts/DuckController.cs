using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    private Transform thisTrans;
    //private Transform CamTran;
    public Vector2 SpeedRange;
    private float speed;
    // Start is called before the first frame update

    private void OnEnable()
    {
        speed = Random.Range(SpeedRange.x, SpeedRange.y);
    }
    
    void Start()
    {
        thisTrans = GetComponent<Transform>();       
    }

    // Update is called once per frame
    void Update()
    {
        thisTrans.Translate(-Vector3.right * speed * Time.deltaTime);               
    }
}
