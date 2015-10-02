using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slide : MonoBehaviour {

    [SerializeField]
    private float m_speed;

    [SerializeField, Tooltip("アルファのスピード")]
    private float m_alphaSpeed = 1.0f;

    private Image m_image;
    private Color m_alpha;

    private Vector3 velocity = Vector3.zero;
    [Tooltip("タイマー")]
    private float m_timer = 0;

	// Use this for initialization
	void Start () {
        velocity.x = m_speed;
        m_image = GetComponent<Image>();
        m_alpha = m_image.color;
    }
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;
        //一秒以下であれば動かない
        if (m_timer < 0.5f) return;

        gameObject.transform.position += velocity * Time.deltaTime;
        m_alpha.a -= m_alphaSpeed * Time.deltaTime;
        m_image.color = m_alpha;
        if (m_image.color.a <= 0)
        {
            Destroy(gameObject);
        }

    }
}
