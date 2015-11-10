using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GyroGravity : MonoBehaviour
{
    private Vector3 m_InitialValue = Vector3.zero;             //基準の傾き
    private Vector3 m_currentValue = Vector3.zero;             //現在の傾き
    private Vector3 m_gravityVelocity = new Vector3(0f, -1f, 0f);   //重力の向き
    private float m_ZAngle;                                        //角度

    public void Awake()
    {
        Input.gyro.enabled = true;
        Reset();
        
    }

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        m_currentValue = Input.gyro.gravity;
        m_currentValue.Normalize();

        m_ZAngle = Vector2.Angle(m_InitialValue, m_currentValue);

        if (Vector2Cross(m_InitialValue, m_currentValue) < 0)
        {
            m_ZAngle *= -1;
        }

        //m_ZAngle = AngleCheck(m_InitialRotation.z, m_currentRotation.z);

        //AngleClamp(m_ZAngle,-45,45);

        if(!Input.gyro.enabled)m_ZAngle = 0;

        CalcGravity();

    }

    /// <summary>
    /// 現在ZのAngleが基準からどれくらい傾いているか計算
    /// </summary>
    /// <returns></returns>
    float AngleCheck(float initial, float current)
    {
        float Angle = initial - current;
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
        m_InitialValue = Input.gyro.gravity;
        m_InitialValue.Normalize();
        Physics2D.gravity = new Vector3(0, -1, 0);
    }

    public float Vector2Cross(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.y - rhs.x * lhs.y;
    }

}