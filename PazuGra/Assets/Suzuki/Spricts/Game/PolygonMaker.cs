using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DelegateFunction;
public class PolygonMaker : MonoBehaviour {

    [SerializeField]
    private float mDotDistance = 7;
    private int mCrossPoint = 0;
    private List<Vector2> mPoint;//頂点
    private List<Vector2> mRoot = new List<Vector2>();//Crossの図形用
    private List<GameObject> mPolygon = new List<GameObject>();
    private Texture2D texture2D;
    private int colorstate = 0;


    //Add 虹ブロック用のスプライト
    [SerializeField]
    public Sprite m_RainbowSprit;

    private BlockFactory mBlockFactory;
    private ReduceVertexPolygonCollider2D mRedPolyogn;

    private void Start()
    {
        mBlockFactory = GameObject.Find("Factory").GetComponent<BlockFactory>();
        mRedPolyogn = GameObject.Find("ReduceVertexPolygonCollider2D").GetComponent<ReduceVertexPolygonCollider2D>();
    }

    //初期化
    public void Init()
    {
        mPoint = new List<Vector2>();
        mRoot = new List<Vector2>();
    }

    public void SetTexture(Texture2D texture)
    {
        texture2D = texture;
    }

    public void AddRoot(Vector2 point)
    {
        mRoot.Add(point);
    }

    public void AddPoint(Vector2 point)
    {
        mPoint.Add(point);
    }

    public void RemovePolygons()
    {
        //ポリゴンの削除
        foreach (var i in mPolygon) Destroy(i);
    }

    //Colorの設定
    public Color RandomColor()
    {
        //Add Color White
        Color[] colors = new Color[5] { Color.red, Color.blue, Color.yellow, Color.green, Color.white };
        return colors[colorstate];
    }

    public bool IsMakeDistance(Vector3 mouseWorldPos)
    {
        //距離をはかり、頂点同士を少し離す。とりあえず10
        if (Vector2.Distance(mRoot[mRoot.Count - 1], mouseWorldPos) > mDotDistance)return true;
        else return false;
    }

    //要素数のチェック
    private bool IsRootCount(int count)
    {
        if (mRoot.Count < count) return true;
        return false;
    }

    //三角形を作れるか
    public bool IsMakeLine()
    {
        if (IsRootCount(3)) return false;
        else return true;
    }

    //頂点がなす線が交わっているか。
    private bool IsCross(List<Vector2> mRoot)
    {
        if (IsRootCount(5)) return false;
        int last = mRoot.Count - 1;
        for (int i = 0; i < mRoot.Count - 4; i++)
        {
            if (MyMath.CheckInterSection(mRoot[last], mRoot[last - 1], mRoot[i], mRoot[i + 1]))
            {
                mCrossPoint = i + 1;
                return true;
            }
        }
        return false;
    }

    //頂点をまたいだ場合の処理
    public void OnCross()
    {
        //頂点をまたいだ場合の処理
        if (IsCross(mRoot)){
            CreatePolygonOnCross(mCrossPoint);
            CreatePolygon(mCrossPoint);
            mRoot.Clear();
            mRoot.Add(mPoint[mPoint.Count - 1]);
        }
    }

    //中点をとる関数
    //ラストと開始点に中点をおく。
    //public void AddCenter()
    //{
    //    float distance = Vector2.Distance(mPoint[0], mPoint[mPoint.Count - 1]);
    //    while (distance > 5)
    //    {
    //        int last = mPoint.Count - 1;
    //        Vector2 center = new Vector2((mPoint[0].x + mPoint[last].x) / 2, (mPoint[0].y + mPoint[last].y) / 2);
    //        mPoint.Add(center);
    //        mRoot.Add(center);
    //        distance = Vector2.Distance(mPoint[0], mPoint[last]);
    //    }
    //    //再起はさむ
    //    mPoint.Add(mPoint[0]);
    //    mRoot.Add(mPoint[0]);
    //}

    //対象の頂点をX方向にスケーリングしよう。
    private List<Vector2> VectorScaleShape(List<Vector2> targetList)
    {
        List<Vector2> list = new List<Vector2>();
        foreach (var i in targetList)list.Add(new Vector2(i.x * 2.0f, i.y));
        return list;
    }

    //Blockの生成
    public GameObject CreateUI(GameObject mBaseUI)
    {
        //生成
        GameObject ui = Instantiate(mBaseUI) as GameObject;
        ui.transform.SetParent(GameManager.GetInstanc.GetUIContller().SearchParent(Layers.Layer_Def).transform, false);
        ui.name = mBaseUI.name + "ID : " + ui.GetHashCode().ToString();

        //当たり判定の決定
        PolygonCollider2D col = ui.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
        col.CreatePrimitive(mPoint.Count, Vector2.one, new Vector2(-ui.transform.localPosition.x, -ui.transform.localPosition.y));
        //頂点のスケーリング
        List<Vector2> list = new List<Vector2>();
        for (int i = 0; i < mPoint.Count; i++)
        {
            //list.Add(mPoint[i] * 9.0f / 16.0f);
            //list.Add(new Vector2(mPoint[i].x * 9.0f / 16.0f * 3f, mPoint[i].y * 9.0f / 16.0f * 1.5f));
            list.Add(new Vector2(mPoint[i].x * 9.0f / 16.0f * 1.5f, mPoint[i].y * 9.0f / 16.0f * 1.5f));
        }
        //頂点の設定
        col.points = list.ToArray();
        //面積をポイントとして設定
        BlockInfo info = ui.AddComponent<BlockInfo>();
        //infomationのパラメーターの決定
        info.m_BlockPoint = MathTextureArea(texture2D);
        info.m_ID = ui.GetHashCode();
        //Add fix----------------------------------------------
        if (mBlockFactory.isRainbow)
        {
            colorstate = 4;
            ui.transform.GetChild(0).GetComponent<Image>().color = RandomColor();
            ui.transform.GetChild(0).GetComponent<Image>().sprite = m_RainbowSprit;
        }
        else
        {
            colorstate = Random.Range(0, 4);
        }
        //end---------------------------------------------------
        info.m_ColorState = (ColorState)colorstate;

        ui.GetComponent<Rigidbody2D>().gravityScale = info.m_BlockPoint;

        //Debug.Log("Before : "+col.points.Length);
        mRedPolyogn.RemoveShapes(ui);
        //Debug.Log("After : " + col.points.Length);
        return ui;
    }

    //開始点と終点が近すぎた場合
    private bool IsLastPoint(List<Vector2> targetList)
    {
        if (Vector2.Distance(targetList[0], targetList[targetList.Count-1]) <= 0.2f) return false;
        return true;
    }

    //背景にポリゴンを生成
    public IEnumerator AnsyCreatePolygon(IsFunction func)
    {
        string objectName = "Polygon";
        Mesh mesh = new Mesh();
        mesh.Clear();

        //Add
        if (!IsLastPoint(mRoot)) mRoot.RemoveAt(mRoot.Count - 1);
        //mRoot = VectorScaleShape(mRoot);
        //

        //頂点の数で初期化
        Vector3[] vertices = new Vector3[mRoot.Count];
        for (int i = 0; i < mRoot.Count; i++)
        {
            vertices[i] = mRoot[i];
        }

        //Cubeの位置を頂点としている
        mesh.vertices = vertices;

        //頂点の数で初期化
        Vector2[] verticesXY = new Vector2[mRoot.Count];
        for (int i = 0; i < mRoot.Count; i++)
        {
            Vector3 pos = mRoot[i];
            verticesXY[i] = new Vector2(pos.x, pos.y);
        }

        //メッシュ内の全ての三角形を含む配列
        Triangulator tr = new Triangulator(verticesXY, Camera.main.transform.position);
        int[] indices = tr.Triangulate();
        //Debug.Log(indices.Length);

        mesh.triangles = indices;

        //頂点の数で初期化
        mesh.uv = new Vector2[mRoot.Count];

        mesh.RecalculateNormals();

        mesh.Optimize();
        mesh.RecalculateBounds();

        //メッシュの生成
        GameObject Polygon = new GameObject(objectName);

        MeshRenderer meshRenderer = Polygon.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Diffuse"));

        Polygon.transform.position = new Vector3(1080 / 9, 1920 / 9, 0);


        MeshFilter meshFilter = Polygon.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        Polygon.layer = 8;
        Polygon.GetComponent<Renderer>().material.color = Color.white;

        mPolygon.Add(Polygon);

        func();
        yield return null;
    }

    //Src用Mesh作成
    public void CreatePolygon()
    {
        string objectName = "Polygon";
        Mesh mesh = new Mesh();
        mesh.Clear();

        //頂点の数で初期化
        Vector3[] vertices = new Vector3[mRoot.Count];
        for (int i = 0; i < mRoot.Count; i++)
        {
            vertices[i] = mRoot[i];
        }

        //Cubeの位置を頂点としている
        mesh.vertices = vertices;

        //頂点の数で初期化
        Vector2[] verticesXY = new Vector2[mRoot.Count];
        for (int i = 0; i < mRoot.Count; i++)
        {
            Vector3 pos = mRoot[i];
            verticesXY[i] = new Vector2(pos.x, pos.y);
        }

        //メッシュ内の全ての三角形を含む配列
        Triangulator tr = new Triangulator(verticesXY, Camera.main.transform.position);
        int[] indices = tr.Triangulate();
        //Debug.Log(indices.Length);

        mesh.triangles = indices;

        //頂点の数で初期化
        mesh.uv = new Vector2[mRoot.Count];

        mesh.RecalculateNormals();

        mesh.Optimize();
        mesh.RecalculateBounds();

        //メッシュの生成
        GameObject Polygon = new GameObject(objectName);

        MeshRenderer meshRenderer = Polygon.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Diffuse"));

        Polygon.transform.position = new Vector3(1080 / 9, 1920 / 9, 0);


        MeshFilter meshFilter = Polygon.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        Polygon.layer = 8;
        Polygon.GetComponent<Renderer>().material.color = Color.white;

        mPolygon.Add(Polygon);
    }


    //Src用Mesh作成
    public void CreatePolygon(int crossPoint)
    {
        string objectName = "Polygon";
        Mesh mesh = new Mesh();
        mesh.Clear();

        //頂点の数で初期化
        Vector3[] vertices = new Vector3[mRoot.Count - crossPoint];
        for (int i = 0; i < mRoot.Count - crossPoint; i++)
        {
            vertices[i] = mRoot[crossPoint + i];
        }

        //Cubeの位置を頂点としている
        mesh.vertices = vertices;

        //頂点の数で初期化
        Vector2[] verticesXY = new Vector2[mRoot.Count - crossPoint];
        for (int i = 0; i < mRoot.Count - crossPoint; i++)
        {
            Vector3 pos = mRoot[crossPoint + i];
            verticesXY[i] = new Vector2(pos.x, pos.y);
        }

        //メッシュ内の全ての三角形を含む配列
        Triangulator tr = new Triangulator(verticesXY, Camera.main.transform.position);
        int[] indices = tr.Triangulate();
        mesh.triangles = indices;

        //頂点の数で初期化
        mesh.uv = new Vector2[mRoot.Count - crossPoint];

        mesh.RecalculateNormals();

        mesh.Optimize();
        mesh.RecalculateBounds();

        //メッシュの生成
        GameObject Polygon = new GameObject(objectName);

        MeshRenderer meshRenderer = Polygon.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Diffuse"));

        Polygon.transform.position = new Vector3(1080 / 9, 1920 / 9, 0);


        MeshFilter meshFilter = Polygon.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        Polygon.layer = 8;
        Polygon.GetComponent<Renderer>().material.color = Color.white;

        mPolygon.Add(Polygon);
    }

    //Src用Mesh作成
    public void CreatePolygonOnCross(int crossPoint)
    {
        string objectName = "Polygon";
        Mesh mesh = new Mesh();
        mesh.Clear();

        //頂点の数で初期化
        Vector3[] vertices = new Vector3[crossPoint + 1];
        for (int i = 0; i < crossPoint; i++)
        {
            vertices[i] = mRoot[i];
        }
        vertices[crossPoint] = mRoot[0];
        //Cubeの位置を頂点としている
        mesh.vertices = vertices;

        //頂点の数で初期化
        Vector2[] verticesXY = new Vector2[crossPoint + 1];
        for (int i = 0; i < crossPoint; i++)
        {
            Vector3 pos = mRoot[i];
            verticesXY[i] = new Vector2(pos.x, pos.y);
        }

        verticesXY[crossPoint] = mRoot[0];

        //メッシュ内の全ての三角形を含む配列
        Triangulator tr = new Triangulator(verticesXY, Camera.main.transform.position);
        int[] indices = tr.Triangulate();
        mesh.triangles = indices;

        //頂点の数で初期化
        mesh.uv = new Vector2[crossPoint + 1];

        mesh.RecalculateNormals();

        mesh.Optimize();
        mesh.RecalculateBounds();

        //メッシュの生成
        GameObject Polygon = new GameObject(objectName);

        MeshRenderer meshRenderer = Polygon.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Diffuse"));

        Polygon.transform.position = new Vector3(1080 / 9, 1920 / 9, 0);


        MeshFilter meshFilter = Polygon.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        Polygon.layer = 8;
        Polygon.GetComponent<Renderer>().material.color = Color.white;

        mPolygon.Add(Polygon);
    }

    //Textureの面積を計算
    private float MathTextureArea(Texture2D texture)
    {
        float white = 0;
        float black = 0;
        foreach (var i in texture.GetPixels())
        {
            if (i.a < 0.3f) black++;
            else { white++; }
        }
        return white / black * 100.0f;
    }
}
