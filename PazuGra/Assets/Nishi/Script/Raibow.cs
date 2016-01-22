using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Raibow : MonoBehaviour
{

    public Color[] m_Colors;
    private Color m_NextColor;
    private float t;
    private int i = 0;

    public void Start()
    {
        m_NextColor = m_Colors[0];
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / 2;
        if (t > 0.7)
        {
            t = 0;
            i++;
            if (i >= m_Colors.Length) i = 0;
            m_NextColor = m_Colors[i];
        }

        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, m_NextColor,t);

    }
}
