using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chain : MonoBehaviour {

    
    private BlockInfo m_MyInfo;

    /// <summary>
    /// true:チェック済み false::チェックされてない;
    /// </summary>
    [SerializeField]
    private bool m_isCheck = false;
    //private List<GameObject> m_otherBlocks = new List<GameObject>();

    // Use this for initialization
    void Start () {
        m_MyInfo = gameObject.transform.parent.GetComponent<BlockInfo>();

    }
	
	// Update is called once per frame
	void Update () {
        if(m_isCheck)
        {
            gameObject.transform.parent.GetComponent<Rigidbody2D>().WakeUp();
        }
	
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (m_isCheck)
        {
            if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
            {
                var temp = other.transform.FindChild("Collider");
                if (!temp.GetComponent<Chain>().IsCheck())
                {
                    temp.GetComponent<Chain>().SetCheck(true);
                }
            }
        }
    }

    public bool IsCheck()
    {
        return m_isCheck;
    }
    public void SetCheck(bool check)
    {
        m_isCheck = check;
    }
    //接触したオブジェクトの色を判定
    private bool ColorChack(ColorState state)
    {
        if (state == m_MyInfo.m_ColorState) return true;
        return false;
    }
}
