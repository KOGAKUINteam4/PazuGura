using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ComboManager : MonoBehaviour, IRecieveMessage
{
    [SerializeField]
    GameObject m_CgaugePrefab;
    GameObject m_Cgauge;

    private int m_MaxCombo;

    public void ComboSend()
    {
        if (m_Cgauge == null)
        {
            m_Cgauge = Instantiate(m_CgaugePrefab);
            m_Cgauge.transform.SetParent(GameObject.Find("FrontCanvas").transform, false);
        }
        else
        {
            ExecuteEvents.Execute<IRecieveMessage>(
             m_Cgauge, // 呼び出す対象のオブジェクト
             null,  // イベントデータ（モジュール等の情報）
            (recieveTarget, y) => { recieveTarget.ComboSend(); }); // 操作

            ExecuteEvents.Execute<IRecieveMessage>(
             m_Cgauge.transform.FindChild("numbers").gameObject, // 呼び出す対象のオブジェクト
             null,  // イベントデータ（モジュール等の情報）
            (recieveTarget, y) => { recieveTarget.ComboSend(); }); // 操作
        }

        if (m_MaxCombo < m_Cgauge.GetComponentInChildren<ComboCounter>().GetCombo())
        {
            m_MaxCombo = m_Cgauge.GetComponentInChildren<ComboCounter>().GetCombo();
        }
    }
}
