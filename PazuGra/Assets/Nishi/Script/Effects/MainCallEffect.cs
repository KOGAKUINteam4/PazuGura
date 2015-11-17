using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainCallEffect : MonoBehaviour {

    [SerializeField]
    Image m_Hole;
    [SerializeField]
    Image m_Shadow;

    RectTransform m_Rectpos;

    // Use this for initialization
    void Start () {
        m_Rectpos = gameObject.GetComponent<RectTransform>();


	
	}
	
	// Update is called once per frame
	void Update () {

        Color color = new Color(0.0f, 0.0f,0.0f,0.01f);

        m_Hole.color += color;
        m_Shadow.color += color;

        if (m_Hole.color.a >= 1)
        {
            if (gameObject.transform.position.y <= -100)
            {
                m_Hole.rectTransform.localScale += new Vector3(0.1f, 0.1f, 0.0f);
            }
            else
            {
                Vector3 pos = new Vector3(0.0f, -1.0f, 0.0f);
                gameObject.transform.position += pos;
            }
        }
	
	}
}
