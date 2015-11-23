using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChainManager : MonoBehaviour
{

    private List<GameObject> m_removeList = new List<GameObject>();
    [SerializeField]
    private GameObject m_ComboGauge;
    [SerializeField]
    private GameObject m_BlockFactory;
    private int m_Maxchain = 0;
    private bool m_isClick = false;  //クリックが完了したか

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CrickStart();
        }
        if (Input.GetMouseButtonUp(0) && m_isClick)
        {
            //ColliderSwitch(true);
            PushList();
            Remove();
            m_isClick = false;
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
                if (!hitObj.GetComponent<Chain>().IsCheck())
                {
                    hitObj.GetComponent<Chain>().SetCheck(true);
                    m_isClick = true;
                }
                
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
            float score = 0;
            GameManager gameManager = GameManager.GetInstanc;
            foreach (GameObject obj in m_removeList)
            {
                score += obj.GetComponent<BlockInfo>().m_BlockPoint;
                gameManager.GetScoreUI().AddScore(obj.GetComponent<BlockInfo>().m_BlockPoint);
                //Destroy(obj);
                if (m_Maxchain < m_removeList.Count)
                {
                    m_Maxchain = m_removeList.Count;
                }
                StartCoroutine(coRoutine(obj));
            }
            gameManager.GetScoreUI().UpdateScore(score);
            gameManager.GetScoreUI().CreateEffect();
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

    public int GetMaxChain()
    {
        return m_Maxchain;
    }

    IEnumerator coRoutine(GameObject obj)
    {
        while (obj.transform.GetChild(0).GetComponent<UIPixRange>().range <= 1)
        {
            obj.transform.GetChild(0).GetComponent<UIPixRange>().range += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(obj);

        yield break;
    }

}
