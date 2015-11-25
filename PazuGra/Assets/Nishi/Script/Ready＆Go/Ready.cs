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
    [SerializeField]
    private GameObject m_GoPrefab;

    public void Awake()
    {
        m_image = GetComponent<Image>();
    }

    public void Start()
    {
        LeanTween.alpha(m_image.rectTransform, 1, 1.5f).setOnComplete(() => { CreateEnd(); });
    }

    private void CreateEnd()
    {
        GameObject temp = Instantiate(m_GoPrefab);
        temp.transform.SetParent(transform.parent, false);
        Destroy(gameObject);
    }


}
