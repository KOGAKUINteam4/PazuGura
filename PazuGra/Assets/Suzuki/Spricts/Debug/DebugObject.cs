﻿using UnityEngine;
using System.Collections;

public class DebugObject : MonoBehaviour {

    private GameManager mManager;

	// Use this for initialization
	void Start () {
        mManager = GameManager.GetInstanc;

        BlockManager manager = mManager.GetBlockManager();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
