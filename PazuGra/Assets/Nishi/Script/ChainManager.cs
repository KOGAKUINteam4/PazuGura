using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainManager : MonoBehaviour {

    private List<GameObject> m_removeList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var layerMask = 1 << LayerMask.NameToLayer("Block");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1.0f, layerMask);
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
        foreach(GameObject obj in objs )
        {
            if(obj.GetComponent<Chain>().IsCheck())
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
}
