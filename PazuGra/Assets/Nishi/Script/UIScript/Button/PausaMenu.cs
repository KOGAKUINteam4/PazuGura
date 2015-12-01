using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PausaMenu : MonoBehaviour {

    [SerializeField]
    GameObject m_RestartMenu = null;
    [SerializeField]
    GameObject m_GameEndMenu = null;

    GameObject m_MenuObject = null;
    Button[] m_Buttons;

    // Use this for initialization
    void Start () {
        m_Buttons = gameObject.GetComponentsInChildren<Button>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if(m_MenuObject != null)
        {
            
            ButtonSwitch(false);
        }
        else
        {
            ButtonSwitch(true);
        }
	}

    public void OnGameEND()
    {
        Move(() => GameEnd());
        //m_MenuObject = Instantiate(m_GameEndMenu);
        //m_MenuObject.transform.SetParent(gameObject.transform, false);
    }

    public void OnReStart()
    {
        Move(() => ReStart());
        //m_MenuObject = Instantiate(m_RestartMenu);
        //m_MenuObject.transform.SetParent(gameObject.transform, false);
    }

    private void Move(System.Action lambda)
    {
        LeanTween.moveLocalX(transform.GetChild(1).gameObject, -1500, 0.5f);
        LeanTween.moveLocalX(transform.GetChild(2).gameObject, -1500, 0.5f).setDelay(0.1f);
        LeanTween.moveLocalX(transform.GetChild(3).gameObject, -1500, 0.5f).setDelay(0.3f);
        LeanTween.moveLocalX(transform.GetChild(4).gameObject, -1500, 0.5f).setDelay(0.5f).setOnComplete(() => { lambda();});
    }

    private void ReStart()
    {
        m_MenuObject = Instantiate(m_RestartMenu);
        m_MenuObject.transform.SetParent(gameObject.transform, false);
    }

    private void GameEnd()
    {
        m_MenuObject = Instantiate(m_GameEndMenu);
        m_MenuObject.transform.SetParent(gameObject.transform, false);
    }

    public void OnCancel()
    {
        Destroy(gameObject);
        ExecuteEvents.Execute<IPauseSend>(
                GameObject.Find("Pausable"),
                null,
                (events, y) => { events.PauseSend(false); });
    }

    private void ButtonSwitch(bool result)
    {
        foreach(Button but in m_Buttons)
        {
            but.enabled = result;
        }
    }
}
