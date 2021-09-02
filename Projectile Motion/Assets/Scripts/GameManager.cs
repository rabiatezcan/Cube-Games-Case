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

    private int hMax = 15;
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
                pointType = Point_Type.End;
            }
            else if (pointType == Point_Type.End)
            {
                endPoint.position = touchPosition;
                StartCoroutine(ThrowTheBall());
                isProjectileMoving = false;
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

    IEnumerator ThrowTheBall()
    {
        isProjectileMoving = true;
        GameObject ball = null;
        ball = GetAvailableObject();
        if (ball == null)
        {
            yield break;
        }

        ball.SetActive(true);
        ball.transform.position = startPoint.position;

        Vector3 velocity = new Vector3();

        Vector3 direction = endPoint.position - startPoint.position;
        float range = direction.magnitude;
        Vector3 unitDirection = direction.normalized;

        float maxYAxisPos = startPoint.position.y + hMax;

        velocity.y = Mathf.Sqrt(-2f * -Constants.gravitationalForce * (maxYAxisPos - startPoint.position.y));

        float timeTohMax = Mathf.Sqrt(-2f * (maxYAxisPos - startPoint.position.y) / -Constants.gravitationalForce);
        float timeToEnd = Mathf.Sqrt(-2f * (maxYAxisPos - endPoint.position.y) / -Constants.gravitationalForce);
        float totalTime = timeTohMax + timeToEnd;

        float horizontalVelocity = range / totalTime;
        velocity.x = horizontalVelocity * unitDirection.x;
        velocity.z = horizontalVelocity * unitDirection.z;

        float time = 0f;
        while (time < totalTime)
        {
            ball.transform.Translate(velocity.x * Time.deltaTime,
                (velocity.y - (Constants.gravitationalForce * time)) * Time.deltaTime, velocity.z * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        
        ball.SetActive(false);
    }
}