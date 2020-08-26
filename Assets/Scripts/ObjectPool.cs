using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

//простой пулл всех объектов, чтобы не нагружать систему Instantiate и Destroy каждый раз
public class ObjectPool : MonoBehaviour
{

    public static ObjectPool ObjPool;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> objInPool;

    void Awake()
    {
        ObjPool = this;
    }

    // Use this for initialization
    void Start()
    {
        objInPool = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.GetComponent<Renderer>().sortingOrder = i + 1; // чтобы не смешивались
                setChildSortingLayerTheSame(obj, objInPool.Count + 1);
                obj.SetActive(false);
                objInPool.Add(obj);
            }
        }
    }

    public void disableObjectsInPool()
    {
        for (int i = 0; i < objInPool.Count; i++)
        {
            objInPool[i].SetActive(false);
        }

    }

    public GameObject GetObjectFromPool(string tag)
    {
        for (int i = 0; i < objInPool.Count; i++)
        {
            if (!objInPool[i].activeInHierarchy && objInPool[i].tag == tag)
            {
                return objInPool[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.GetComponent<Renderer>().sortingOrder = objInPool.Count + 1;
                    setChildSortingLayerTheSame(obj, objInPool.Count + 1);
                    obj.SetActive(false);
                    objInPool.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    void setChildSortingLayerTheSame(GameObject gO, int sortLayer)
    {
        for (int i = 0; i < gO.transform.childCount; i++)
        {
            gO.transform.GetChild(i).GetComponent<Renderer>().sortingOrder = sortLayer;
        }

    }
}
