using UnityEngine;
using System.Collections;
using System;

public class LogFunctions : MonoBehaviour {

    void Print(Action<object[]> action , params object[] param)
    {
        action(param);
    }



	// Use this for initialization
	void Start () {
        //Print((object[] i) => { foreach (var p in i) { Debug.Log(p.ToString()); };}, 0, 0, 0, 0);

        //Print((object[] i) => { 
        //    foreach (var p in i) { Debug.Log(p.ToString()); };
        //}
        //    , 0, 0, 0, 0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
