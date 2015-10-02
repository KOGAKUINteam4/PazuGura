using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// アルファを強くしていき、Goオブジェクトを生成する
/// </summary>
public class Ready : MonoBehaviour
{
    public GameObject nextImaget;

    [SerializeField, Tooltip("アルファのスピード")]
    private float m_speed = 1.0f;

    private Image m_image;
    private Color m_alpha;

    public void Awake()
    {
        m_image = GetComponent<Image>();
        m_alpha = Color.white;
        m_alpha.a = 0;
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
            temp.GetComponent<Image>().rectTransform.position = GetComponent<Image>().rectTransform.position;

            Destroy(gameObject);
        }

    }
}
