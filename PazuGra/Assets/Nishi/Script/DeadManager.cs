using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadManager : MonoBehaviour {

    List<GameObject> removableBlockList = new List<GameObject>();
    GameObject fast = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //クリックはじめ
            OnDragStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //クリック終わり（消す）
            OnDragEnd();
        }

    }

    private void OnDragStart()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hitObj = hit.collider.gameObject;
            string blockName = hitObj.name;
            Debug.Log("abc"+blockName); //blockの名前を取得してみる
            
            //ブロックの名前の最初に"Block"がついていて　前にクリックものと同じではない
            if (blockName.StartsWith("Block") && fast == null)
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
        if (remove_cnt >= 3)
        {
            for (int i = 0; i < remove_cnt; i++)
            {
                Destroy(removableBlockList[i]);
            }

        }
        fast = null;
    }

    public void PushToList(GameObject obj)
    {
        removableBlockList.Add(obj);
    }
}
