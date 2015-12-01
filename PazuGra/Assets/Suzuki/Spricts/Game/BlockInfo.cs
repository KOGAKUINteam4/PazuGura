using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
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
        StreamWriter sw = new StreamWriter(path, false);
        foreach (var i in GetComponent<PolygonCollider2D>().points)
        {
            sw.WriteLine(Mathf.Ceil(i.x));
            sw.WriteLine(Mathf.Ceil(i.y));
        }
        sw.Flush();
        sw.Close();
    }

    [ContextMenu("ExportTexture")]
    private void ExportTexture()
    {
        string name = "Art";
        string path = Application.dataPath + "/" + "Resources/" + "Stamp/" + name + "Stamp" + ".png";

        Texture2D tex = transform.GetComponent<Image>().mainTexture as Texture2D;
        byte[] png = tex.EncodeToPNG();
        File.WriteAllBytes(path, png);
    }

}
