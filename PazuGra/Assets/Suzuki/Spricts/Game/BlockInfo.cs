using UnityEngine;
using System.Collections;

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

}
