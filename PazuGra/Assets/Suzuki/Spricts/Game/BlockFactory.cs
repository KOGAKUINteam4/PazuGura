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

    //Manager等
    private GameManager mGamaManager;
    private UIContller mUICtr;

    private List<Vector2> mPoint;//頂点
    private Texture2D texture2D;

    //InfoMation
    private BlockManager mBlockMgr;
    private GameObject mPolygon;

	// Use this for initialization
	void Start () {
        mGamaManager = GameManager.GetInstanc;
        mUICtr = GameManager.GetInstanc.GetUIContller();
        mBlockMgr = mGamaManager.GetBlockManager();
        Init();
	}

    //初期化
    private void Init()
    {
        mPoint = new List<Vector2>();
    }

    //画面を押された瞬間
    public void CreateBlockOnTouch()
    {
        Init();
        Vector3 mouseWorldPos = MathPos();
        mPoint.Add((Vector2)mouseWorldPos);
    }

    //画面を押されている。
    public void CreateBlockOnStay()
    {
        //インフォメーションの更新
        Vector3 mouseWorldPos = MathPos();
        //距離をはかり、頂点同士を少し離す。とりあえず10
        if (Vector2.Distance(mPoint[mPoint.Count - 1], mouseWorldPos) > 10){
            mPoint.Add((Vector2)mouseWorldPos);
        }
    }

    //画面を押され、リリースされたとき
    public void CreateBlockOnRelease()
    {
        //画面上に生成
        MakePolygon();
        //Textureの作成
        StartCoroutine(RenderTextureOutPut());
    }

    //最初の頂点を結ぶ
    private void MakePolygon()
    {
        mPoint.Add(mPoint[0]);
        //サブカメラに写るポリゴンの生成
        CreatePolygon();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) CreateBlockOnTouch();
        if (Input.GetMouseButton(0)) CreateBlockOnStay();
        if (Input.GetMouseButtonUp(0)) CreateBlockOnRelease();
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
        yield return new WaitForEndOfFrame();
        CaptureToPNG();
        //テクスチャーの設定
        GameObject ui = CreateUI();
        ui.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0, 0, 256, 256), Vector2.zero);
        ui.transform.GetChild(0).GetComponent<Image>().color = RandomColor();
        Destroy(mPolygon);
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

        mBlockMgr.AddBlock(info);

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

    //Src用Mesh作成
    void CreatePolygon()
    {
        string objectName = "Polygon";
        Mesh mesh = new Mesh();

        //頂点の数で初期化
        Vector3[] vertices = new Vector3[mPoint.Count];
        for (int i = 0; i < mPoint.Count; i++)
        {
            vertices[i] = mPoint[i];
        }
        //Cubeの位置を頂点としている
        mesh.vertices = vertices;

        //頂点の数で初期化
        Vector2[] verticesXY = new Vector2[mPoint.Count];
        for (int i = 0; i < mPoint.Count; i++)
        {
            Vector3 pos = mPoint[i];
            verticesXY[i] = new Vector2(pos.x, pos.y);
        }

        //メッシュ内の全ての三角形を含む配列
        Triangulator tr = new Triangulator(verticesXY, Camera.main.transform.position);
        int[] indices = tr.Triangulate();
        //Debug.Log(indices.Length);
        mesh.triangles = indices;

        //頂点の数で初期化
        mesh.uv = new Vector2[mPoint.Count];

        mesh.RecalculateNormals();

        //メッシュの生成
        mPolygon = new GameObject(objectName);

        MeshRenderer meshRenderer = mPolygon.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Diffuse"));

        mPolygon.transform.position = new Vector3(1080 / 9, 1920 / 9, 0);


        MeshFilter meshFilter = mPolygon.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        mPolygon.layer = 8;
        mPolygon.GetComponent<Renderer>().material.color = Color.white;
    }

    private Vector3 MathPos()
    {
        //Screen cast World
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //panelのローカル座標に
        return mouseWorldPos * 46;
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
