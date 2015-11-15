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
        m_MenuObject = Instantiate(m_GameEndMenu);
        m_MenuObject.transform.SetParent(gameObject.transform.parent, false);
    }

    public void OnReStart()
    {
        m_MenuObject = Instantiate(m_RestartMenu);
        m_MenuObject.transform.SetParent(gameObject.transform.parent, false);
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
