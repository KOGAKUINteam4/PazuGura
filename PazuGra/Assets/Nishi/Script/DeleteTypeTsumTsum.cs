using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeleteTypeTsumTsum : MonoBehaviour {

    private GameObject firstBlock;
    private GameObject lastBlock;
    private string currentName;
    List<GameObject> removableBlockList = new List<GameObject>();

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && firstBlock == null)
        {
            OnDragStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //クリックを終えた時
            OnDragEnd();
        }
        else if (firstBlock != null)
        {
            OnDragging();
        }
    }

    private void OnDragStart()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObj = hit.collider.gameObject;
            string ballName = hitObj.name;
            //識別　最初に"Piyo"と書いてあれば
            if (ballName.StartsWith("Block"))
            {
                firstBlock = hitObj;
                lastBlock = hitObj;
                currentName = hitObj.name;
                removableBlockList = new List<GameObject>();
                hitObj.GetComponent<SpriteRenderer>().color = Color.blue;
                PushToList(hitObj);
            }
        }
    }

    private void OnDragging()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hitObj = hit.collider.gameObject;

            //最初に触ったブロックと同じ名前　かつ　最後に触れたボールではない（同じブロックに触れること防止）
            if (hitObj.name == currentName && lastBlock != hitObj)
            {
                //距離を測る　今のオブジェとヒットしているオブジェの距離
                float distance = Vector2.Distance(hitObj.transform.position, lastBlock.transform.position);
                if (distance < 1.0f)
                {
                    lastBlock = hitObj;
                    hitObj.GetComponent<SpriteRenderer>().color = Color.blue;
                    PushToList(hitObj);
                }
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
        firstBlock = null;
        lastBlock = null;
    }

    void PushToList(GameObject obj)
    {
        removableBlockList.Add(obj);
    }

}
