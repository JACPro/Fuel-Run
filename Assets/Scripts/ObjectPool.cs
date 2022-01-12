using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool sharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> activeObjects;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int initialPoolSize = 30;
    

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start() {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < initialPoolSize; i++)
        {
            temp = Instantiate(objectPrefab, objectPrefab.transform.position, objectPrefab.transform.rotation);
            temp.SetActive(false);
            temp.gameObject.transform.SetParent(this.gameObject.transform, false);
            pooledObjects.Add(temp);
        }    
    }

    public GameObject GetObject()
    {
        for (int i=0; i < initialPoolSize; i++)
        {
            GameObject pooledObject = pooledObjects[i];
            if(!pooledObject.activeInHierarchy)
            {
                pooledObject.SetActive(pooledObject);
                activeObjects.Add(pooledObject);
                return pooledObject;
            }
        }   
        GameObject myObject = Instantiate(objectPrefab, objectPrefab.transform.position, objectPrefab.transform.rotation);
        myObject.transform.SetParent(this.gameObject.transform, false);
        pooledObjects.Add(myObject);
        activeObjects.Add(myObject);
        return myObject;
    }

    public GameObject RemoveObject()
    {
        int activeCount = activeObjects.Count;

        if (activeCount == 0) return null;
        
        GameObject temp = activeObjects[activeCount - 1];

        temp.SetActive(false);
        activeObjects.RemoveAt(activeCount - 1);
        return temp;
    }
}