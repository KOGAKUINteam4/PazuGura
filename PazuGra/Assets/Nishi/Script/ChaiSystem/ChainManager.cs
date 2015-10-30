using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ChainManager : MonoBehaviour
{

    private List<GameObject> m_removeList = new List<GameObject>();
    [SerializeField]
    private GameObject m_ComboGauge;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var layerMask = 1 << LayerMask.NameToLayer("Block");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1.0f, layerMask);
            if (hit.collider == null)
            {
                return;
            }
            if (hit.collider.tag == "Block")
            {
                GameObject hitObj = hit.collider.gameObject.transform.FindChild("Collider").gameObject;

                m_removeList = new List<GameObject>();
                hitObj.GetComponent<Chain>().SetCheck(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            PushList();
            Remove();
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
        Debug.Log(m_removeList.Count);
        if (m_removeList.Count >= 3)
        {
            foreach (GameObject obj in m_removeList)
            {
                Destroy(obj);
            }
            ComboSend();
            m_removeList.Clear();
        }
        else
        {
            foreach (GameObject obj in m_removeList)
            {
                obj.transform.FindChild("Collider").GetComponent<Chain>().SetCheck(false);
            }
        }
    }

    void ComboSend()
    {
        ExecuteEvents.Execute<ComboManager>(
             m_ComboGauge, // 呼び出す対象のオブジェクト
             null,  // イベントデータ（モジュール等の情報）
            (recieveTarget, y) => { recieveTarget.ComboStart(); }); // 操作
    }
}
