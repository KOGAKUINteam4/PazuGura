using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// アルファを強くしていき、Goオブジェクトを生成する
/// </summary>
public class Ready : MonoBehaviour
{
    private Image m_image;

    public void Awake()
    {
        m_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_image.color.a >= 1)
        {
            ExecuteEvents.Execute<CreateObject>(
                gameObject,
                null,
                (target, y) => target.CreateObj());
            Destroy(gameObject);
        }
    }
}
