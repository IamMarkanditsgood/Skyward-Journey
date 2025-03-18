using UnityEngine;
using System.Collections;

public class CameraFollow2D : Singleton<CameraFollow2D> {
		
	//private bool gameReady = false;
	private Transform trans;
	public Transform target;
	public float delay=10000f;
	public float distance=40;
	public float heigtLimit = 600f;
	float height,width;
	Vector3 InitialPos;
	Camera cam;

	void Start () {
		trans = GetComponent<Transform>();
		cam = Camera.main;
		height = Screen.height/100;
		width = Screen.width/100;
		InitialPos = trans.position;
	}


	public void getTarget(Transform _target){			
		target = _target;
		
	}

	public void ResetCamera(){
		target = null;
		trans.position = InitialPos;
	}


	// Update is called once per frame
	void LateUpdate () {
		if(target ==null){
			//target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
			return;
		}
		if(target){
			trans.position = Vector3.Lerp(trans.position, target.position+new Vector3(200f,0f,-distance),Time.deltaTime*delay);

			if(trans.position.y<height/2) trans.position=new Vector3(trans.position.x, height, trans.position.z);
			if(trans.position.y>height/2+heigtLimit) trans.position=new Vector3(trans.position.x, height+heigtLimit, trans.position.z);
		//	if(trans.position.x<minX+width/2) trans.position=new Vector3(minX+width/2, trans.position.y, trans.position.z);
		//	if(trans.position.x>maxX-width/2) trans.position=new Vector3(maxX-width/2, trans.position.y, trans.position.z);
			}
	}
}
