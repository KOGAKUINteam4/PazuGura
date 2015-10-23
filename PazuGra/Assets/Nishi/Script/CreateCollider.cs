using UnityEngine;
using System.Collections;

public class CreateCollider : MonoBehaviour {

    private PolygonCollider2D m_ParentCollider;
    private PolygonCollider2D m_MyCollider;
    [SerializeField, Tooltip("スケール倍率")]
    private float scale = 1.2f;

    // Use this for initialization
    void Start()
    {
        m_ParentCollider = gameObject.transform.parent.gameObject.GetComponent<PolygonCollider2D>();
        m_MyCollider = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        //親のポリゴンから当たり判定用コリジョンを作り出す
        Vector2[] temp = new Vector2[50];
        for (int i = 0; i <= (m_ParentCollider.points.Length - 1); i++)
        {
            temp[i] = m_ParentCollider.points[i] * scale;
            m_MyCollider.SetPath(0, temp);
        }
    }
}
