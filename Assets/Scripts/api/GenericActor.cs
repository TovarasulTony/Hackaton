﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActor : MonoBehaviour
{
    protected List<GenericBehavior> mBehaviorsList = new List<GenericBehavior>();

    void Awake()
    {
        AwakeActor();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].StartBehavior();
        }
    }

    void Start()
    {
        StartActor();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].StartBehavior();
        }
    }

    void Update()
    {
        UpdateActor();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].UpdateBehavior();
        }
    }

    void FixedUpdate()
    {
        FixedUpdateActor();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].FixedUpdateBehavior();
        }
    }

    protected void DestroyActor()
    {
        foreach (GenericBehavior behavior in mBehaviorsList)
        {
            behavior.DestroyBehavior();
        }
        Destroy(gameObject);
    }

    protected virtual void AwakeActor() { }
    protected virtual void StartActor() { }
    protected virtual void UpdateActor() { }
    protected virtual void FixedUpdateActor() { }
}