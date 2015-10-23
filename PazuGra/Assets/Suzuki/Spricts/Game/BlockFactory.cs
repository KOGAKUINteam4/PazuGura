using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//ブロック生成用のクラス

public class BlockFactory : MonoBehaviour {

    [SerializeField]
    private RenderTexture target;
    [SerializeField]
    private GameObject mBaseUI;

    [SerializeField][Range(1.0f,10.0f)]
    private float mDotDistance;

    [SerializeField]
    private GameObject mEffect;

    private GameObject mMainCamera;

    //Manager等
    private GameManager mGamaManager;
    private UIContller mUICtr;

    private List<Vector2> mPoint;//頂点
    private List<Vector2> mRoot = new List<Vector2>();//Crossの図形用
    private Texture2D texture2D;

    //InfoMation
    private BlockManager mBlockMgr;
    private List<GameObject> mPolygon = new List<GameObject>();

    //保存用
    private int mCrossPoint = 0;
    private GameObject mInstanceUI = null;
    private Vector2 mTouchPosition;

    private bool mIsShoot = false;

	// Use this for initialization
	void Start () {
        mGamaManager = GameManager.GetInstanc;
        mUICtr = GameManager.GetInstanc.GetUIContller();
        mBlockMgr = mGamaManager.GetBlockManager();
        mMainCamera = GameObject.Find("Sub Camera");
        Init();
	}

    //初期化
    private void Init()
    {
        mPoint = new List<Vector2>();
        mRoot = new List<Vector2>();
    }

    private void Update()
    {


        if (!mIsShoot)
        {
            if (Input.GetMouseButtonDown(0)) CreateBlockOnTouch();
            if (Input.GetMouseButton(0)) CreateBlockOnStay();
            if (Input.GetMouseButtonUp(0)) CreateBlockOnRelease();
        }

        UpdateInstanceUI();
    }

    //頂点がなす線が交わっているか。
    private bool IsCross()
    {
        if (mRoot.Count < 5) return false;
        int last = mRoot.Count - 1;
        for (int i = 0; i < mRoot.Count - 4; i++){
            if (MyMath.CheckInterSection(mRoot[last], mRoot[last - 1], mRoot[i], mRoot[i + 1])){
                mCrossPoint = i + 1;
                return true;
            }
        }
        return false;
    }

    //頂点をまたいだ場合の処理
    private void OnCross()
    {
        //頂点をまたいだ場合の処理
        if (IsCross()){
            CreatePolygonOnCross(mCrossPoint);
            CreatePolygon(mCrossPoint);
            mRoot.Clear();
            mRoot.Add(mPoint[mPoint.Count - 1]);
        }
    }

    //画面を押された瞬間
    public void CreateBlockOnTouch()
    {
        Init();
        Vector3 mouseWorldPos = MathPos();
        mRoot.Add((Vector2)mouseWorldPos);
        mPoint.Add((Vector2)mouseWorldPos);
        mTouchPosition = mouseWorldPos;
    }

    //画面を押されている。
    public void CreateBlockOnStay()
    {
        //インフォメーションの更新
        Vector3 mouseWorldPos = MathPos();
        //距離をはかり、頂点同士を少し離す。とりあえず10
        if (Vector2.Distance(mRoot[mRoot.Count - 1], mouseWorldPos) > mDotDistance){
            mPoint.Add((Vector2)mouseWorldPos);
            mRoot.Add((Vector2)mouseWorldPos);
        }
        //頂点をまたいだ場合
        OnCross();
    }

    //画面を押され、リリースされたとき
    public void CreateBlockOnRelease()
    {
        AddCenter();
        //画面上に生成
        MakePolygon();
        //Textureの作成
        StartCoroutine(RenderTextureOutPut());
        mIsShoot = true;
    }

    //最初の頂点を結ぶ
    private void MakePolygon()
    {
        //サブカメラに写るポリゴンの生成
        CreatePolygon();
    }

    //UIの最小サイズを判定するやつ。
    private bool IsUISize(GameObject ui)
    {
        if (ui.GetComponent<BlockInfo>().m_BlockPoint <= 7.0f) return true;
        return false;
    }

    //フリックの処理(仮)
    private void UpdateInstanceUI()
    {
        if (mInstanceUI == null) return;

        if (Input.GetMouseButtonUp(0)){
            Vector2 mouse = MathPos();
            Vector2 vec = new Vector2(mouse.x - mTouchPosition.x, mouse.y-mTouchPosition.y);
            Rigidbody2D gravity = mInstanceUI.GetComponent<Rigidbody2D>();
            gravity.isKinematic = false;
            gravity.AddForce(vec.normalized * 50.0f,ForceMode2D.Impulse);// = vec.normalized * 10.0f;
            mInstanceUI.transform.GetChild(0).GetComponent<Image>().color = RandomColor();
            mInstanceUI = null;
            mIsShoot = false;
        }
    }



    //サブカメラから見たTextureの保存
    private void CaptureToPNG()
    {
        RenderTexture.active = target;
        Texture2D tex2d = new Texture2D(target.width, target.height, TextureFormat.ARGB32, false);
        tex2d.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
        RenderTexture.active = null;
        texture2D = tex2d;
        tex2d.Apply();
    }

    //Colorの設定
    private Color RandomColor()
    {
        Color[] colors = new Color[4] {Color.red,Color.blue,Color.yellow,Color.green};
        //return colors[Random.Range(0,4)];
        return colors[Random.Range(0, 1)];
    }

    //テクスチャーの設定
    public IEnumerator RenderTextureOutPut()
    {
        mMainCamera.SetActive(true);
        yield return new WaitForEndOfFrame();
        CaptureToPNG();
        //テクスチャーの設定
        GameObject ui = CreateUI();
        ui.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0, 0, 256, 256), Vector2.zero);

        //小さい場合は削除する。
        if (IsUISize(ui))
        {
            Destroy(ui);
            mIsShoot = false;
        }

        mMainCamera.SetActive(false);

        //ポリゴンの削除
        foreach (var i in mPolygon) Destroy(i);
    }

    //Blockの生成
    private GameObject CreateUI()
    {
        //生成
        GameObject ui = Instantiate(mBaseUI) as GameObject;
        ui.transform.SetParent(mUICtr.SearchParent(Layers.Layer_Def).transform, false);
        ui.name = mBaseUI.name + "ID : "+ ui.GetHashCode().ToString();

        //当たり判定の決定
        PolygonCollider2D col = ui.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
        col.CreatePrimitive(mPoint.Count, Vector2.one, new Vector2(-ui.transform.localPosition.x, -ui.transform.localPosition.y));
        //頂点のスケーリング
        List<Vector2> list = new List<Vector2>();
        for (int i = 0; i < mPoint.Count; i++){
            list.Add(mPoint[i] * 9.0f / 16.0f);
        }
        //頂点の設定
        col.points = list.ToArray();
        //面積をポイントとして設定
        BlockInfo info = ui.AddComponent<BlockInfo>();
        //infomationのパラメーターの決定
        info.m_BlockPoint = MathTextureArea(texture2D);
        info.m_ID = ui.GetHashCode();

        ui.GetComponent<Rigidbody2D>().gravityScale = info.m_BlockPoint;
        //Debug.Log("面積 : "+info.m_BlockPoint);

        mBlockMgr.AddBlock(info);

        //保存用のUI
        mInstanceUI = ui;

        return ui;
    }

    //Textureの面積を計算
    private float MathTextureArea(Texture2D texture)
    {
        float white = 0;
        float black = 0;
        foreach (var i in texture.GetPixels()){
            if (i.a < 0.3f) black++;
            else { white++; }
        }
        return white / black * 100.0f;
    }

    //中点をとる関数
    //ラストと開始点に中点をおく。
    private void AddCenter()
    {
        float distance = Vector2.Distance(mPoint[0],mPoint[mPoint.Count - 1]) ;
        while(distance > 5)
        {
            int last = mPoint.Count - 1;
            Vector2 center = new Vector2((mPoint[0].x + mPoint[last].x) / 2, (mPoint[0].y + mPoint[last].y) / 2);
            mPoint.Add(center);
            mRoot.Add(center);
            distance = Vector2.Distance(mPoint[0],mPoint[last]);
        }
        //再起はさむ
        mPoint.Add(mPoint[0]);
        mRoot.Add(mPoint[0]);
    }

    //Src用Mesh作成
    private void CreatePolygon()
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
    void CreatePolygon(int crossPoint)
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
    void CreatePolygonOnCross(int crossPoint)
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

    private Vector3 MathPos()
    {
        //Screen cast World
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //panelのローカル座標に
        return mouseWorldPos * 50;
    }

    //白と黒の比率
    private void DebugTexture(Texture2D texture)
    {
        float white = 0;
        float black = 0;
        Debug.Log(texture.GetPixel(0, 0));
        foreach (var i in texture.GetPixels())
        {
            if (i.a < 0.3f) black++;
            else white++;
        }
        Debug.Log("white : " + white);
        Debug.Log("black : " + black);

        Debug.Log(white / black * 100 + "%");
    }
}
