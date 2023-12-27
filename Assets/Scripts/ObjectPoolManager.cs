using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    [SerializeField]
    public List<ObjectPool_SO> poolParametersList = new List<ObjectPool_SO>();
    private Dictionary<string,ObjectPool> objectPools = new Dictionary<string,ObjectPool>();

    public override void Init(){
        if(objectPools==null || objectPools.Count==0){
            InitPools();
        }
    }

    void InitPools(){
        foreach(var poolParameters in poolParametersList){
            var poolName = poolParameters.label;
            ObjectPool op = new ObjectPool(); 
            op.CreateObjectPool(poolParameters,transform);
            objectPools.Add(poolName, op);
        }
    }

    public ObjectPool GetPool(string label){
        foreach(var poolName in objectPools.Keys){
            if(poolName==label){
                return objectPools[label];
            }
        }
        return null;
    }
    
    public int GetActiveChildCount(){
        int activeChild = 0;
        for(int i=0; i<transform.childCount; i++){
            if(transform.GetChild(i).gameObject.activeInHierarchy){
                activeChild ++;
            }
        }
        return activeChild;
    }

}
