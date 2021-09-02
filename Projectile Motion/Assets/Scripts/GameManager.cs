using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Point_Type
{
    Start,
    End,
}

public class GameManager : MonoBehaviour
{
    

    #region Pooling

    [SerializeField] private GameObject ballPrefab;
    private int poolGrowCounter = 0;
    private readonly List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < Constants.poolDepth; i++)
        {
            GameObject pooledObject = Instantiate(ballPrefab, transform);
            pooledObject.SetActive(false);
            pool.Add(pooledObject);
        }
    }

    public GameObject GetAvailableObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
                return pool[i];
        }

        if (poolGrowCounter < Constants.maxPoolDepth)
        {
            GameObject pooledObject = Instantiate(ballPrefab, transform);
            pooledObject.SetActive(false);
            pool.Add(pooledObject);
            poolGrowCounter++;
            return pooledObject;
        }
        else
        {
            return null;
        }
    }

    #endregion


}