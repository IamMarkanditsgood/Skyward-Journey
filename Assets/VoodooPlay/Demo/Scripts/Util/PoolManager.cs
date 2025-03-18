using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager>
{	// The object prefabs which the pool can handle.
	public List<GameObject> objectPrefabs = new List<GameObject>();

	// The pooled objects currently available.
	public List<GameObject>[] pooledObjects;
	
	// The amount of objects of each type to buffer.
	public List<int> amountToBuffer = new List<int>();
	
	public int defaultBufferAmount = 3;

	// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
	protected GameObject containerObject;

	public override void Awake()
	{
		base.Awake();
		StartPool();
	}
	
	// Use this for initialization
	void StartPool ()
	{
		containerObject = new GameObject("ObjectPool");
		
		//Loop through the object prefabs and make a new list for each one.
		//We do this because the pool can only support prefabs set to it in the editor,
		//so we can assume the lists of pooled objects are in the same order as object prefabs in the array
		pooledObjects = new List<GameObject>[objectPrefabs.Count];
		
		int i = 0;
		foreach ( GameObject objectPrefab in objectPrefabs )
		{
			objectPrefab.SetActive(false);
			pooledObjects[i] = new List<GameObject>(); 
			
			int bufferAmount;
			
			if(i < amountToBuffer.Count) bufferAmount = amountToBuffer[i];
			else
				bufferAmount = defaultBufferAmount;
			
			for ( int n=0; n<bufferAmount; n++)
			{
				GameObject newObj = Instantiate(objectPrefab) as GameObject;
				newObj.name = objectPrefab.name;
				Despawn(newObj);
			}
			
			i++;
			objectPrefab.SetActive(true);
		}
	}
	

	// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
	// then null will be returned.

	public GameObject Spawn ( string objectType , bool onlyPooled )
	{
		for(int i=0; i<objectPrefabs.Count; i++)
		{
			GameObject prefab = objectPrefabs[i];
			if(prefab.name == objectType)
			{
				
				if(pooledObjects[i].Count > 0)
				{
					GameObject pooledObject = pooledObjects[i][0];
					pooledObjects[i].RemoveAt(0);
					pooledObject.transform.parent = null;
					pooledObject.SetActive(true);
					
					return pooledObject;
					
				} else if(!onlyPooled) {
					return Instantiate(objectPrefabs[i]) as GameObject;
				}
				
				break;
				
			}
		}
		
		//If we have gotten here either there was no object of the specified type or non were left in the pool with onlyPooled set to true
		return null;
	}

	public GameObject Spawn ( string objectType ,Vector3 objectPosition, Quaternion objectRotation, bool onlyPooled )
	{
		for(int i=0; i<objectPrefabs.Count; i++)
		{
			GameObject prefab = objectPrefabs[i];
			if(prefab.name == objectType)
			{
				
				if(pooledObjects[i].Count > 0)
				{
					GameObject pooledObject = pooledObjects[i][0];
					pooledObjects[i].RemoveAt(0);
					pooledObject.transform.parent = null;
					pooledObject.SetActive(true);
					pooledObject.transform.position = objectPosition;
					pooledObject.transform.rotation = objectRotation;

					
					return pooledObject;
					
				} else if(!onlyPooled) {
					return Instantiate(objectPrefabs[i]) as GameObject;
				}
				
				break;
				
			}
		}
		
		//If we have gotten here either there was no object of the specified type or non were left in the pool with onlyPooled set to true
		return null;
	}

	// Pools the object specified.  Will not be pooled if there is no prefab of that type.
	public void Despawn ( GameObject obj )
	{
		for ( int i=0; i<objectPrefabs.Count; i++)
		{
			if(objectPrefabs[i].name == obj.name)
			{
				obj.SetActive(false);
				obj.transform.parent = containerObject.transform;
				pooledObjects[i].Add(obj);
				return;
			}
		}
	}
	
}