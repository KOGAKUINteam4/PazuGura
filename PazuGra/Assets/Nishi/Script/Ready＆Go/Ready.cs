using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// アルファを強くしていき、Goオブジェクトを生成する
/// </summary>
public class Ready : MonoBehaviour
{
    public GameObject nextImaget = null;

    [SerializeField, Tooltip("アルファのスピード")]
    private float m_speed = 1.0f;

    private Image m_image;
    private Color m_alpha;

    public void Awake()
    {
        m_image = GetComponent<Image>();
        m_alpha = m_image.color;
    }



    // Update is called once per frame
    void Update()
    {
        m_alpha.a += m_speed * Time.deltaTime;
        m_image.color = m_alpha;
        if (m_image.color.a >= 1)
        {
            GameObject temp;
            temp = (GameObject)Instantiate(nextImaget);
            //GOオブジェクトの位置の補正
            temp.transform.parent = gameObject.transform.parent;

            temp.transform.GetComponent<RectTransform>().position = Vector3.zero;
            temp.transform.GetComponent<RectTransform>().localScale = Vector3.one;

            //temp.transform.GetComponent<RectTransform>().position = GetComponent<Image>().rectTransform.position;

            Destroy(gameObject);
        }

    }
}
