using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChainManager : MonoBehaviour
{

    private List<GameObject> m_removeList = new List<GameObject>();
    [SerializeField]
    private GameObject m_ComboGauge = null;
    [SerializeField]
    private GameObject m_BlockFactory = null;
    private int m_Maxchain = 0;

    private bool m_isListUp = false; //リストアップの終了
    private GameObject mHit;
    private GameObject mBack;

    private GameObject m_ClickObj;

    // Update is called once per frame

    void LateUpdate()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    //ColliderSwitch(true);
        //    //if (!m_isListUp)
        //    //{
        //    PushList();
        //    if (m_removeList.Count > 3)
        //    {
        //        foreach (var i in m_removeList)
        //        {
        //            Color temp = i.gameObject.transform.GetChild(0).GetComponent<Image>().color;
        //            i.gameObject.transform.GetChild(0).GetComponent<Image>().color =
        //                Color.Lerp(temp, new Color(1, 1, 1, 1), 1.0f);
        //        }
        //        m_isListUp = true;
        //    }
        //    else
        //    {
        //        foreach (GameObject obj in m_removeList)
        //        {
        //            if (obj != null) obj.transform.FindChild("Collider").GetComponent<Chain>().SetCheck(false);
        //        }
        //    }
        //    //}
        //}
        if (Input.GetMouseButtonUp(0))
        {
            PushList();
            Remove();
        }
        if (Input.GetMouseButtonDown(0))
        {
            CrickStart();
            //ColliderSwitch(true);
            //PushList();

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
                GameObject hitObj = hit.collider.gameObject.transform.GetChild(1).gameObject;
                mHit = hitObj;
                m_ClickObj = hit.collider.gameObject;

                m_removeList = new List<GameObject>();
                if (!hitObj.GetComponent<Chain>().IsCheck())
                {
                    hitObj.GetComponent<Chain>().SetCheck(true);
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
                if (!m_removeList.Contains(obj.transform.parent.gameObject))
                    m_removeList.Add(obj.transform.parent.gameObject);
            }
        }
    }

    void Remove()
    {
        if (m_removeList.Count >= 3)
        {
            AudioManager.Instance.SEPlay(AudioList.Chain);
            float score = 0;
            GameManager gameManager = GameManager.GetInstanc;
            foreach (GameObject obj in m_removeList)
            {
                score += obj.GetComponent<BlockInfo>().m_BlockPoint;
                gameManager.GetScoreUI().AddScore(obj.GetComponent<BlockInfo>().m_BlockPoint);
                Color[] state = { Color.red,Color.black,Color.yellow,Color.green,Color.white};
                transform.GetComponent<SpriteEffect>().Create(obj, state[(int)obj.GetComponent<BlockInfo>().m_ColorState]);
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
            if (m_removeList.Count >= 5)
            {
                ExecuteEvents.Execute<IRecieveMessage>(
                    m_BlockFactory, // 呼び出す対象のオブジェクト
                    null,  // イベントデータ（モジュール等の情報）
                    (recieveTarget, y) => { recieveTarget.ComboSend(); }); // 操作
            }
            foreach (GameObject obj in m_removeList)
            {
                obj.transform.FindChild("Collider").GetComponent<Chain>().SetCheck(false);
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
        foreach (var i in m_removeList)
        {
            if (i.name == mHit.transform.parent.name)
            {
                mBack = mHit;
            }
        }
        obj.transform.GetChild(0).GetComponent<Image>().material = Resources.Load("Mat/UI-Mask") as Material;
        while (obj.transform.GetChild(0).GetComponent<UIPixRange>().range <= 1)
        {
            obj.tag = "Finish";
            obj.transform.GetChild(0).GetComponent<UIPixRange>().range += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < 5; i++)
        {
            if (mBack.transform.parent.GetComponent<BlockInfo>().m_ColorState == (ColorState)i)
            {
                Sprite sprite = mBack.transform.parent.GetComponent<Image>().sprite;
                GameObject traget = GameObject.Find("StampSet").transform.GetChild(i + 1).GetChild(0).gameObject;
                traget.GetComponent<Image>().sprite = sprite;
                traget.transform.parent.GetComponent<StampCounter>().StampUpdate(mBack.transform.parent.GetComponent<PolygonCollider2D>().points);
            }
        }
        Destroy(obj);

        yield break;
    }

    public void InitChain()
    {
        m_Maxchain = 0;
    }

}
