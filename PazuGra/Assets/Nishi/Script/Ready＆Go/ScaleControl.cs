using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleControl : MonoBehaviour {

    [SerializeField, Tooltip("スケール変化のスピード")]
    private float m_speed = 1.0f;

    private Image m_image;
    private Vector3 m_scale;

    // Use this for initialization
    void Start () {
        m_image = GetComponent<Image>();
        m_image.rectTransform.localScale = new Vector3(0.2f,0.2f,1);

    }
	
	// Update is called once per frame
	void Update () {
        //スケールのxが1以上なら消す
        if(m_image.rectTransform.localScale.x > 1.0f)
        {
            Destroy(gameObject);
        }
        m_scale += new Vector3(m_speed, m_speed, 1) * Time.deltaTime;

        m_image.rectTransform.localScale = m_scale;

    }
}
