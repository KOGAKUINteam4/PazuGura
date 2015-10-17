using UnityEngine;
using System.Collections;

public class GyroGravity : MonoBehaviour {

    private Vector3 m_InitialRotation = Vector3.zero;             //基準の傾き
    private Vector3 m_currentRotation = Vector3.zero;             //現在の傾き
    private Vector3 m_gravityVelocity = new Vector3(0f, -1f, 0f);   //重力の向き
    private float m_ZAngle;                                        //角度

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        m_currentRotation = Input.gyro.attitude.eulerAngles;

        m_ZAngle = AngleCheck();

        AngleClamp(m_ZAngle, -45, 45);

        CalcGravity();

    }

    /// <summary>
    /// 現在ZのAngleが基準からどれくらい傾いているか計算
    /// </summary>
    /// <returns></returns>
    float AngleCheck()
    {
        float Angle = m_InitialRotation.z - m_currentRotation.z;
        if (Angle >= 180)
        {
            Angle = Angle - 360.0f;
        }
        if (Angle <= -180)
        {
            Angle = Angle + 360.0f;
        }
        return Angle;
    }

    /// <summary>
    /// Angleをclampするする
    /// </summary>
    void AngleClamp(float value, float min, float max)
    {
        m_ZAngle = Mathf.Clamp(value, min, max);
    }

    //重力を計算する
    void CalcGravity()
    {
        m_gravityVelocity.x = Mathf.Cos((m_ZAngle + 270) * Mathf.PI / 180);
        m_gravityVelocity.y = Mathf.Sin((m_ZAngle + 270) * Mathf.PI / 180);

        Vector2 V = new Vector2(m_gravityVelocity.x, m_gravityVelocity.y);
        V.Normalize();
        Physics2D.gravity = V;
    }

    //傾きをリセットする
    public void Reset()
    {
        m_InitialRotation = Input.gyro.attitude.eulerAngles;
    }
}
