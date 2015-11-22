using UnityEngine;
using System.Collections;
using System.IO;
//インフォメーション用のクラス
//Factoryクラス等で生成時にパラメーターが決定される。
//生成されたブロック情報

public class BlockInfo : Block {

    public BlockInfo()
    {
        m_ID = 0;
        m_Col2d = new PolygonCollider2D();
        m_Color = Color.white;
        m_position = Vector2.zero;


        //m_Col2d.SetPath
        //m_Col2d.points
    }

    [ContextMenu("Export")]
    private void ExportPoint()
    {
        string name = "star";
        string path = Application.dataPath + "/" + "Resources/"+"Stamp/" + name + "Stamp" + ".txt";
        //Debug.Log(path);
        StreamWriter sw = new StreamWriter(path, false);
        foreach (var i in GetComponent<PolygonCollider2D>().points)
        {
            //sw.WriteLine(i.x);
            //sw.WriteLine(i.y);
            //sw.WriteLine((int)i.x);
            //sw.WriteLine((int)i.y);
            sw.WriteLine(Mathf.Ceil(i.x));
            sw.WriteLine(Mathf.Ceil(i.y));
        }
        sw.Flush();
        sw.Close();
    }

}
