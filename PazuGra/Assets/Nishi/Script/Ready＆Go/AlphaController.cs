using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlphaController : MonoBehaviour {

    [SerializeField,Tooltip("alphaのスピード")]
    private float m_changeSpeed;

    private Image m_image;
    private Color m_color;


	// Use this for initialization
	void Start () {
        m_image = GetComponent<Image>();
        m_color = m_image.color;
	}
	
	// Update is called once per frame
	void Update () {
        m_color.a += m_changeSpeed * Time.deltaTime;
        m_image.color = m_color;
	
	}
}
