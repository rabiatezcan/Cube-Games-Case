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
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private InputField hMaxInput;
    [SerializeField] private InputField speedInput;
    
    private Point_Type pointType = Point_Type.Start;

    private int hMax ;
    private int speed;
    
    private bool isProjectileMoving = false;

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


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.y -= Constants.yAxisOffset;
            touchPosition.z = 0f;

            if (pointType == Point_Type.Start)
            {
                startPoint.position = touchPosition;
                Debug.Log("start " + startPoint.position);
                pointType = Point_Type.End;
            }
            else if (pointType == Point_Type.End)
            {
                endPoint.position = touchPosition;
                Debug.Log("end  " + endPoint.position);
                pointType = Point_Type.Start;
            }
        }
    }


    public void UpdateHMaxValue()
    {
        if (!isProjectileMoving)
            hMax = int.Parse(hMaxInput.text);
    }

    public void UpdateSpeedValue()
    {
        if (!isProjectileMoving)
            speed = int.Parse(speedInput.text);
    }

}