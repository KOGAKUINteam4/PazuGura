using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStartEffect : MonoBehaviour
{
    [SerializeField]
    Image m_Go1;
    [SerializeField]
    Image m_Go2;

    public void Start()
    {
        
        LeanTween.moveLocalX(m_Go1.gameObject, 1000, 1);
        LeanTween.alpha(m_Go1.rectTransform, 0.0f, 1);
        LeanTween.moveLocalX(m_Go2.gameObject, -1000, 1);
        LeanTween.alpha(m_Go2.rectTransform, 0.0f, 1).setOnComplete(() => { End(); });

    }

    private void End()
    {
        GameObject.Find("Pausable").GetComponent<Pausable>().PauseSend(false);
        Destroy(gameObject);
    }
}
