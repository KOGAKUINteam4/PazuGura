using UnityEngine;
using System.Collections;

public class LineEffect : MonoBehaviour {

    [SerializeField]
    private float m_Speed = 1.0f;

    private Vector3 m_Velocity = Vector3.zero;
    private float timer;
    private Vector3 m_FastVelocity = Vector3.zero;
    private bool isCurve = false;

    // Use this for initialization
    void Start () {
        if (transform.localPosition.x > 0)
        {
            m_FastVelocity = new Vector3(-1, 0, 0);
            VelocityChange(new Vector3(-1, 0, 0));
        }
        else
        {
            m_FastVelocity = new Vector3(1, 0, 0);
            VelocityChange(new Vector3(1, 0, 0));
        }

    }
	
	// Update is called once per frame
	void Update () {
        
        transform.position += m_Velocity * m_Speed  * Time.deltaTime;

        if (!isCurve)
        {
            if (Random.Range(0, 100) > 95 && !isVelocity())
            {
                if (Random.Range(0, 100) >= 50)
                {
                    VelocityChange(Vector3.up);
                }
                else
                {
                    VelocityChange(Vector3.down);
                }
                isCurve = true;
            }
        }

        if(isVelocity())
        {
            timer += Time.deltaTime;
            if(timer >= Random.Range(0.5f,1))
            {
                timer = 0;
                VelocityChange(m_FastVelocity);
            }
        }

        if(transform.localPosition.x > 700 || transform.localPosition.x < -700)
        {
            Destroy(gameObject, 0.5f);
        }
	}

    public void VelocityChange(Vector3 velocity)
    {
        m_Velocity = velocity;
    }

    bool isVelocity()
    {
        return Vector3.up == m_Velocity || Vector3.down == m_Velocity;
    }
}
