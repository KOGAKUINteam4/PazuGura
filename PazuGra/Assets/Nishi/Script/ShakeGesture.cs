using UnityEngine;
using System.Collections;

public class ShakeGesture : MonoBehaviour {

    [SerializeField]
    float m_power;

    public void Awake()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.acceleration.x >= 2 || Input.acceleration.x <= -2 || Input.GetKeyDown(KeyCode.O))
        {
            var block = GameObject.FindGameObjectsWithTag("Block");
            foreach (GameObject obj in block)
            {
                //obj.GetComponent<Rigidbody2D>().WakeUp();
                obj.GetComponent<Rigidbody2D>().AddForce(Physics2D.gravity * m_power *-1, ForceMode2D.Impulse);
            }
        }
    }
}
