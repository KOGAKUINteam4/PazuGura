using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeUPEffect : MonoBehaviour {
    [SerializeField]
    Image m_image1;
    [SerializeField]
    Image m_image2;

    public void Start()
    {

        LeanTween.moveLocalX(m_image1.gameObject, 0, 2);
        LeanTween.moveLocalX(m_image2.gameObject, 0, 2).setOnComplete(() => { End(); }); ;

    }

    private void End()
    {
       Destroy(gameObject,1);
    }
}
