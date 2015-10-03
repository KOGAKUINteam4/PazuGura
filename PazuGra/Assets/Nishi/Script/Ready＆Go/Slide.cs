using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slide : MonoBehaviour {

    [SerializeField]
    private float m_speed = 1.0f;
    [SerializeField]
    private Vector3 m_velocity;

    

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position += m_velocity * m_speed * Time.deltaTime;
        

    }
}
