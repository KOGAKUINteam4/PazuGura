using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    public int m_ID;                    //使うかはわからないID
    public float m_BlockPoint;          //スコア加算
    public Vector2 m_position;          //使うかは知らない
    public PolygonCollider2D m_Col2d;   //当たり判定生成用
}
