using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    public int m_ID;                    //使うかはわからないID
    public float m_BlockPoint;          //スコア加算
    public Vector2 m_position;          //使うかは知らない
    public PolygonCollider2D m_Col2d;   //当たり判定生成用
    public Color m_Color;               //色
    public Vector2 m_Velocity;          //傾きの慣性ベクトル
    public ColorState m_ColorState;     //floatで比べたくないから状態で
}
