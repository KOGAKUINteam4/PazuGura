using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//使用するUIを生成したり、削除したりする

public class UIContller : MonoBehaviour {

    private List<GameObject> mActiveUI;
    //レイヤー管理用
    private List<GameObject> mLayerSet;

    public UIContller()
    {
        mActiveUI = new List<GameObject>();
        mLayerSet = new List<GameObject>();
    }

    private void Start()
    {
        InitParent();
    }

    //レイヤーオブジェクトの取得
    private void InitParent()
    {
        foreach (Transform i in GameObject.Find("LayerSet").transform){
            mLayerSet.Add(i.gameObject);
        }
    }
 
    public List<GameObject> GetUIList()
    {
        return mActiveUI;
    }

    //レイヤーペアレントのサーチ
    private GameObject SearchParent(int index)
    {
        return mLayerSet[index];
    }

    //生成するオブジェクト
    //座標
    //親
    public GameObject CreateUI(GameObject instance, Vector2 position, Layers parentIndex = Layers.Layer_Front)
    {
        GameObject instanceUI = Instantiate(instance,position,Quaternion.identity) as GameObject;
        instanceUI.transform.parent = SearchParent((int)parentIndex).transform;
        return instance;
    }

}
