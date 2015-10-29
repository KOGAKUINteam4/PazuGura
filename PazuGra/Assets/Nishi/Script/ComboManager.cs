using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ComboManager : MonoBehaviour , IRecieveMessage
{
    [SerializeField]
    GameObject m_CgaugePrefab;
    GameObject m_Cgauge;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ComboStart()
    {
        if (m_Cgauge == null)
        {
            m_Cgauge = Instantiate(m_CgaugePrefab);
            m_Cgauge.transform.SetParent(GameObject.Find("FrontCanvas").transform, false);
        }
        else
        {
            ExecuteEvents.Execute<ComboGauge>(
             m_Cgauge, // 呼び出す対象のオブジェクト
             null,  // イベントデータ（モジュール等の情報）
            (recieveTarget, y) => { recieveTarget.ComboStart(); }); // 操作

            ExecuteEvents.Execute<ComboCounter>(
             m_Cgauge.transform.FindChild("numbers").gameObject, // 呼び出す対象のオブジェクト
             null,  // イベントデータ（モジュール等の情報）
            (recieveTarget, y) => { recieveTarget.ComboStart(); }); // 操作
        }
    }



}
