using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{

    [SerializeField]
    private Color color = new Color(1, 1, 1, 1);
    private float timer = 0;

    public void Color()
    {
        GetComponent<Image>().color = color;
    }

    public void Update()
    {
        if(GetComponent<Image>().color == color)
        {
            timer += Time.deltaTime;
            if(timer > 0.5f)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, 1);
                timer = 0;
            }
        }
    }
}
