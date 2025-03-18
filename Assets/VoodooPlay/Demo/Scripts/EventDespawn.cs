using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventDespawn : MonoBehaviour
{
    private UnityAction someListener;

    void Awake ()
    {
        someListener = new UnityAction (SomeFunction);
    }

    void OnEnable ()
    {
        EventManager.StartListening ("DespawnAll", someListener);
       
    }

    void OnDisable ()
    {
        EventManager.StopListening ("DespawnAll", someListener);
       
    }

    void SomeFunction ()
    {
        PoolManager.instance.Despawn(this.gameObject);
        //Debug.Log(this.name);
    }
}
