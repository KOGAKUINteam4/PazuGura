using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ChainManager : MonoBehaviour
{

    private List<GameObject> m_removeList = new List<GameObject>();
    [SerializeField]
    private GameObject m_ComboGauge;
    [SerializeField]
    private GameObject m_BlockFactory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CrickStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //ColliderSwitch(true);
            PushList();
            Remove();
        }

    }

    void CrickStart()
    {
        var layerMask = 1 << LayerMask.NameToLayer("Block");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1.0f, layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Block")
            {

                ColliderSwitch(true);
                GameObject hitObj = hit.collider.gameObject.transform.GetChild(1).gameObject;

                m_removeList = new List<GameObject>();
                hitObj.GetComponent<Chain>().SetCheck(true);
            }
        }
    }

    void PushList()
    {
        
        var objs = GameObject.FindGameObjectsWithTag("Collider");
        foreach (GameObject obj in objs)
        {
            if (obj.GetComponent<Chain>().IsCheck())
            {
                m_removeList.Add(obj.transform.parent.gameObject);
            }
        }
    }

    void Remove()
    {
        if (m_removeList.Count >= 3)
        {
            foreach (GameObject obj in m_removeList)
            {
                Destroy(obj);
            }
            ComboGaugeStart();
            if(m_removeList.Count >= 5)
            {
                ExecuteEvents.Execute<IRecieveMessage>(
                    m_BlockFactory, // 呼び出す対象のオブジェクト
                    null,  // イベントデータ（モジュール等の情報）
                    (recieveTarget, y) => { recieveTarget.ComboSend(); }); // 操作
            }
            m_removeList.Clear();
        }
        else
        {
            foreach (GameObject obj in m_removeList)
            {
                obj.transform.FindChild("Collider").GetComponent<Chain>().SetCheck(false);
            }
        }

        ColliderSwitch(false);
    }

    /// <summary>
    /// true:ON false:OFF
    /// </summary>
    /// <param name="isbool"></param>
    void ColliderSwitch(bool isbool)
    {
        var cols = GameObject.FindGameObjectsWithTag("Collider");
        foreach (GameObject col in cols)
        {
            col.GetComponent<PolygonCollider2D>().enabled = isbool;
        }
    }

    void ComboGaugeStart()
    {
        ExecuteEvents.Execute<IRecieveMessage>(
             m_ComboGauge, // 呼び出す対象のオブジェクト
             null,  // イベントデータ（モジュール等の情報）
            (recieveTarget, y) => { recieveTarget.ComboSend(); }); // 操作
    }
}
