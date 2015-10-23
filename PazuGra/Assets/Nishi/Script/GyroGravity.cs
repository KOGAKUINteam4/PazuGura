using UnityEngine;
using System.Collections;

public class GyroGravity : MonoBehaviour {

    //# ifdefにする。

    public float acceleration = 1;

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;

	
	}
	
	// Update is called once per frame
	void Update () {
        Physics2D.gravity = Input.gyro.gravity * acceleration;
	}
}
