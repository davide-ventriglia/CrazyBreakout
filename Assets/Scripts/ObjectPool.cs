using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public Queue<GameObject> queue = new Queue<GameObject>();

    public void CreateObjectPool(ObjectPool_SO parameters, Transform parent) {
        for(int i=0; i<parameters.size; i++){
            GameObject obj = GameObject.Instantiate(parameters.objectPrefab, Vector3.zero, Quaternion.identity, parent);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }
        Debug.Log("Created pool: " + parameters.label);
    }

    public GameObject GetPooledObject(){
        GameObject obj = queue.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnPooledObject(GameObject obj){
        obj.SetActive(false);
        queue.Enqueue(obj);
    }
}
