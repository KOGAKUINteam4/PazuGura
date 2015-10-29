using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DeadManager : MonoBehaviour {

    List<GameObject> removableBlockList = new List<GameObject>();
    GameObject fast = null;
    [SerializeField]
    GameObject comboGauge;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //クリックはじめ
            OnDragStart();
        }
        //else if (Input.GetMouseButtonUp(0))
        {
            //クリック終わり（消す）
            OnDragEnd();
        }

    }

    private void OnDragStart()
    {
        var layerMask = 1 << LayerMask.NameToLayer("Block");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,1.0f,layerMask);
        if (hit.collider == null)
        {
            return;
        }

        //Debug.Log("names " + hit.collider.name);
        //Debug.Log("tags "+hit.collider.tag);

        if (hit.collider.tag == "Block")
        {
            GameObject hitObj = hit.collider.gameObject.transform.FindChild("Collider").gameObject;
            
            //ブロックの名前の最初に"Block"がついていて　前にクリックものと同じではない
            if (fast == null)
            {
                removableBlockList = new List<GameObject>();
                hitObj.GetComponent<ChainDelete>().DeadFlagOn();
                fast = hitObj;
            }
        }
    }

    private void OnDragEnd()
    {
        int remove_cnt = removableBlockList.Count;
        //リムーブするリストに３つ以上入っていれば
        Debug.Log("List is "+remove_cnt);
        if (remove_cnt >= 3)
        {
            for (int i = 0; i < remove_cnt; i++)
            {
                Destroy(removableBlockList[i]);
                //Debug.Log(removableBlockList[i]);
            }
            ComboSend();
            removableBlockList.Clear();
        }
        else
        {
            for (int i = 0; i < remove_cnt; i++)
            {
                //消えないブロックのフラグ修正
                removableBlockList[i].gameObject.transform.FindChild("Collider").GetComponent<ChainDelete>().FlagClear();
            }
        }
        fast = null;

    }

    public void PushToList(GameObject obj)
    {
        removableBlockList.Add(obj);
    }

    void ComboSend()
    {
        ExecuteEvents.Execute<ComboManager>(
             comboGauge, // 呼び出す対象のオブジェクト
             null,  // イベントデータ（モジュール等の情報）
            (recieveTarget, y) => { recieveTarget.ComboStart(); }); // 操作
    }
}
