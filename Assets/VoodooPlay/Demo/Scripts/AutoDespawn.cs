using UnityEngine;

public class AutoDespawn : MonoBehaviour
{    
    public float delay;
    
    private void OnEnable()
    {
        Invoke(nameof(SelfDespawn), delay);
    }
    
    void SelfDespawn(){
        if(this.gameObject.activeSelf){
             PoolManager.instance.Despawn(this.gameObject);
        }
    }    
}
