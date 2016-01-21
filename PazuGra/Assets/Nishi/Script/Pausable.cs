﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/// <summary>
/// Rigidbodyの速度を保存しておくクラス
/// </summary>
public class RigidbodyVelocity
{
    public Vector3 velocity;
    public float angularVeloccity;
    public RigidbodyVelocity(Rigidbody2D rigidbody)
    {
        velocity = rigidbody.velocity;
        angularVeloccity = rigidbody.angularVelocity;
    }
}

public class Pausable : MonoBehaviour , IPauseSend
{

    /// <summary>
    /// 現在Pause中か？
    /// </summary>
    [SerializeField]
    private bool pausing;

    /// <summary>
    /// 無視するGameObject
    /// </summary>
    [SerializeField]
    private GameObject[] ignoreGameObjects;

    /// <summary>
    /// ポーズ状態が変更された瞬間を調べるため、前回のポーズ状況を記録しておく
    /// </summary>
    bool prevPausing;

    /// <summary>
    /// Rigidbodyのポーズ前の速度の配列
    /// </summary>
    RigidbodyVelocity[] rigidbodyVelocities;

    /// <summary>
    /// ポーズ中のRigidbodyの配列
    /// </summary>
    Rigidbody2D[] pausingRigidbodies;

    /// <summary>
    /// ポーズ中のMonoBehaviourの配列
    /// </summary>
    MonoBehaviour[] pausingMonoBehaviours;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // ポーズ状態が変更されていたら、Pause/Resumeを呼び出す。
        if (prevPausing != pausing)
        {
            if (pausing) Pause();
            else Resume();
            prevPausing = pausing;
        }
    }

    /// <summary>
    /// 中断
    /// </summary>
    void Pause()
    {
        // Rigidbodyの停止
        // 子要素から、スリープ中でなく、IgnoreGameObjectsに含まれていないRigidbodyを抽出
        Predicate<Rigidbody2D> rigidbodyPredicate =
            obj => !obj.IsSleeping() &&
                   Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        pausingRigidbodies = Array.FindAll(transform.GetComponentsInChildren<Rigidbody2D>(), rigidbodyPredicate);
        rigidbodyVelocities = new RigidbodyVelocity[pausingRigidbodies.Length];
        for (int i = 0; i < pausingRigidbodies.Length; i++)
        {
            // 速度、角速度も保存しておく
            rigidbodyVelocities[i] = new RigidbodyVelocity(pausingRigidbodies[i]);
            pausingRigidbodies[i].Sleep();
        }

        // MonoBehaviourの停止
        // 子要素から、有効かつこのインスタンスでないもの、IgnoreGameObjectsに含まれていないMonoBehaviourを抽出
        Predicate<MonoBehaviour> monoBehaviourPredicate =
            obj => obj.enabled &&
                   obj != this &&
                   Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        pausingMonoBehaviours = Array.FindAll(transform.GetComponentsInChildren<MonoBehaviour>(), monoBehaviourPredicate);
        foreach (var monoBehaviour in pausingMonoBehaviours)
        {
            if (Ignore(monoBehaviour))
            {
                continue;
            }
            //if(!monoBehaviour.GetComponent<Outline>())
            //monoBehaviour.enabled = false;
            if (monoBehaviour.GetType() != typeof(Outline))
            {
                monoBehaviour.enabled = false;
            }
            //if (monoBehaviour.GetComponent<Outline>() != null) monoBehaviour.enabled = true;
        }

    }

    /// <summary>
    /// 再開
    /// </summary>
    void Resume()
    {
        // Rigidbodyの再開
        for (int i = 0; i < pausingRigidbodies.Length; i++)
        {
            if (pausingRigidbodies[i] != null)
            {
                pausingRigidbodies[i].WakeUp();
                pausingRigidbodies[i].velocity = rigidbodyVelocities[i].velocity;
                pausingRigidbodies[i].angularVelocity = rigidbodyVelocities[i].angularVeloccity;
            }
        }

        // MonoBehaviourの再開
        foreach (var monoBehaviour in pausingMonoBehaviours)
        {
            if (monoBehaviour != null)
            {
                monoBehaviour.enabled = true;
            }
        }
    }

    bool Ignore(MonoBehaviour obj)
    {
        return obj as CanvasScaler     ||
               obj as GraphicRaycaster ||
               obj as Image            ||
               obj as Mask;
    }

    public bool IsPause()
    {
        return pausing;
    } 

    public void PauseSend(bool result)
    {
        pausing = result;
    }
}