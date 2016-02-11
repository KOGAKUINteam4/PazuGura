using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TutorialButton : MonoBehaviour
{

    [SerializeField]
    private GameObject m_Ready = null;

    //public void Update()
    //{
    //    if (TutorialManager.Instance.GetStep() != 10)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            TutorialManager.Instance.StepUp();
    //        }
    //    }
    //}

    public void TutorialMethod()
    {
       TutorialManager.Instance.StepUp();
    }

    public void TutorialEnd()
    {
        TutorialManager.Instance.StepReset();
        GameManager.GetInstanc.SetTutorial(false);
        Destroy(gameObject);
        //PrefabManager.Instance.Next(PrefabName.Title); ;
        ExecuteEvents.Execute<IPauseSend>(
                GameObject.Find("Pausable"),
                null,
                (events, y) => { events.PauseSend(false); });
        m_Ready.GetComponent<TitleCallEffect>().NextChange(PrefabName.Title);
        EffectManager.Instance.Create(m_Ready, GameObject.Find("PuauseUI").transform);
    }
}
