using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TemplateStampPoint;
using System.IO;
//ブロック生成用のクラス

public class BlockFactory : MonoBehaviour , IRecieveMessage {


    [SerializeField]
    private GameObject mBaseUI;
    [SerializeField]
    private GameObject mEffect;

    private GameObject mMainCamera;
    //外部クラス
    private CaptureTexture mCaptureTexture;
    private PolygonMaker mPolygonMaker;

    //保存用
    private GameObject mInstanceUI = null;
    private Vector2 mTouchPosition;

    private bool mIsShoot = false;

    [SerializeField]
    private GameObject mOut;
    
    //Line引く用
    [SerializeField]
    private GameObject mLine;
    private GameObject mLineObject;

    //Add 虹のブロックの生成条件
    public bool isRainbow = false;

    private float mCameraView = 30*2;

    [SerializeField]
    private TemplateStamp mStampCounter;

    // Use this for initialization
    void Start () {
        mCaptureTexture = GameObject.Find("CaptureTexture").GetComponent<CaptureTexture>();
        mPolygonMaker = GameObject.Find("PolygonMaker").GetComponent<PolygonMaker>();
        mMainCamera = GameObject.Find("Sub Camera");
        mOut = Resources.Load("Prefub/BlockUI_Out") as GameObject;
        mPolygonMaker.Init();
        //mStampCounter = GameObject.Find("StampTemplate").GetComponent<TemplateStamp>();
	}

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(mPolygonMaker.IsNullCross)
            Invoke("InvokeFunc", 0.1f);
        }

        if (!mIsShoot){
            if (Input.GetMouseButtonDown(0)) CreateBlockOnTouch();
            if (Input.GetMouseButton(0)) CreateBlockOnStay();
            if (Input.GetMouseButtonUp(0))
            {
                CreateBlockOnRelease();
                if(isRainbow) AudioManager.Instance.SEPlay(AudioList.Rainbow);
            }
        }

        UpdateInstanceUI();
    }

    public bool GetShoot() { return mIsShoot; }

    //頂点の追加
    private void AddPoint(Vector3 mouseWorldPos)
    {
        mPolygonMaker.AddRoot((Vector2)mouseWorldPos);
        mPolygonMaker.AddPoint((Vector2)mouseWorldPos);
    }

    //画面を押された瞬間
    public void CreateBlockOnTouch()
    {
        mPolygonMaker.Init();
        Vector3 mouseWorldPos = MathPos();
        AddPoint(mouseWorldPos);
        mLineObject = Instantiate(mLine) as GameObject;
    }

    //画面を押されている。
    public void CreateBlockOnStay()
    {
        if (mPolygonMaker.IsNullCross) return;
        if (mLineObject == null) return;
        //インフォメーションの更新
        Vector3 mouseWorldPos = MathPos();
        if (mPolygonMaker.IsMakeDistance(mouseWorldPos)) AddPoint(mouseWorldPos);
        //頂点をまたいだ場合
        mPolygonMaker.OnCross();
        mLineObject.transform.position = new Vector3(MathPos().x / mCameraView, MathPos().y / mCameraView, 0);
        //mLineObject.transform.position = new Vector3(MathPos().x/50.0f, MathPos().y/50.0f, 0);
    }

    private void InvokeFunc()
    {
        mPolygonMaker.IsNullCross = false;
    }

    //画面を押され、リリースされたとき
    public void CreateBlockOnRelease()
    {
        Destroy(mLineObject);

        Invoke("InvokeFunc",0.1f);
        if (!mPolygonMaker.IsMakeLine()) return;
        mPolygonMaker.IsNullCross = true;
        //画面上に生成
        //サブカメラに写るポリゴンの生成
        StartCoroutine(mPolygonMaker.AnsyCreatePolygon(CreateTexture));
        //mPolygonMaker.CreatePolygon();
        //Textureの作成
        //StartCoroutine(RenderTextureOutPut());
        mIsShoot = true;
        GameManager.GetInstanc.GetRanking().mDrowCount += 1;
        //if(!mIsStamp)
        mIsStamp = false;
    }

    //画面を押された瞬間
    public void CreateBlockOnTouch(Vector3 point)
    {
        mPolygonMaker.Init();
        AddPoint(point);
        mTouchPosition = point;
    }

    //画面を押されている。
    public void CreateBlockOnStay(Vector3 point)
    {
        AddPoint(point);
        mPolygonMaker.OnCross();
    }

    private void CreateTexture()
    {
        //Textureの作成
        StartCoroutine(RenderTextureOutPut());
    }

    //UIの最小サイズを判定するやつ。
    private bool IsUISize(GameObject ui)
    {
        if (ui.GetComponent<BlockInfo>().m_BlockPoint <= 3.0f) return true;
        return false;
    }

    //フリックの処理(仮)
    private void UpdateInstanceUI()
    {
        if (mInstanceUI == null) return;
        if (mPolygonMaker.IsNullCross) return;


        if (Input.GetMouseButtonDown(0)) mTouchPosition = MathPos();

        if (mTouchPosition.x - Vector3.zero.x == 0) return;

        if (Input.GetMouseButtonUp(0)){
            Vector2 mouse = MathPos();
            Vector2 vec = new Vector2(mouse.x - mTouchPosition.x, mouse.y-mTouchPosition.y);
            Rigidbody2D gravity = mInstanceUI.GetComponent<Rigidbody2D>();
            gravity.isKinematic = false;
            gravity.AddForce(vec.normalized * 10.0f,ForceMode2D.Impulse);

            //if (isRainbow)
            //{
            //    mInstanceUI.transform.GetChild(0).GetComponent<Image>().sprite = mPolygonMaker.m_RainbowSprit;
            //}
            //else mInstanceUI.transform.GetChild(0).GetComponent<Image>().color = mPolygonMaker.RandomColor();
            
            GameObject outLine = Instantiate(mOut);
            outLine.GetComponent<Image>().sprite = mInstanceUI.GetComponent<Image>().sprite;

            outLine.transform.SetParent(mInstanceUI.transform,false);
            outLine.transform.position = mInstanceUI.transform.position;
            outLine.transform.SetSiblingIndex(2);

            mStampCounter.AddCost(1);

            if (isRainbow)
            {
                mInstanceUI.transform.GetChild(0).GetComponent<Image>().color = Color.black;
                outLine.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                outLine.transform.GetChild(0).GetComponent<Image>().sprite = mPolygonMaker.m_RainbowSprit;
            }
            else outLine.transform.GetChild(0).GetComponent<Image>().color = mPolygonMaker.RandomColor();
            
            
            //フラグ等の初期化
            isRainbow = false;
            mInstanceUI = null;
            mIsShoot = false;

            mTouchPosition = Vector3.zero;

            //ここで作る。

            AudioManager.Instance.SEPlay(AudioList.Flick);
        }
    }

    //テクスチャーの設定
    public IEnumerator RenderTextureOutPut()
    {
        mMainCamera.SetActive(true);
        yield return new WaitForEndOfFrame();
        Texture2D texture = mCaptureTexture.CaptureToPNG();
        mPolygonMaker.SetTexture(texture);
        //テクスチャーの設定
        GameObject ui = mPolygonMaker.CreateUI(mBaseUI);
        mInstanceUI = ui;
        ui.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), Vector2.zero);

        //小さい場合は削除する。
        if (IsUISize(ui)){
            Destroy(ui);
            mIsShoot = false;
        }

        mMainCamera.SetActive(false);

        mPolygonMaker.RemovePolygons();
    }

    private Vector3 MathPos()
    {
        //Screen cast World
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mouseWorldPos);
        //panelのローカル座標に
        return mouseWorldPos * mCameraView;
        //return mouseWorldPos * 50;
    }

    //イベント駆動で虹を発生させる
    public void ComboSend()
    {
        isRainbow = true;
    }

    private bool mIsStamp = false;
    TextReader txtReader;
    string description;
    [ContextMenu("Stamp")]
    public void DebugStart(string name,int color)
    {
        if (isRainbow) color = (int)ColorState.Color_ALL;
        if (GetShoot()) return;
        mPolygonMaker.SetStampColor(color);
        if (color == (int)ColorState.Color_ALL) isRainbow = true;
        StartCoroutine(LoadText(name));
    }

    public void StampUpdate(Vector2[] point,int color)
    {
        if (isRainbow) color = (int)ColorState.Color_ALL;
        mPolygonMaker.SetStampColor(color);
        if (color == (int)ColorState.Color_ALL) isRainbow = true;
        StartCoroutine(LoadText(point));
    }

    IEnumerator LoadText(string name)
    {
        string txtBuffer = "";
        string textFileName = name +".txt";
        string path = "";// Application.dataPath + "/" + "Resources/" + "Stamp/";
        List<Vector2> list = new List<Vector2>();

#if UNITY_EDITOR
        path = Application.streamingAssetsPath + "\\" + textFileName;
        FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
        txtReader = new StreamReader(file);
        yield return new WaitForSeconds(0f);
#elif UNITY_ANDROID
        path = "jar:file://" + Application.dataPath + "!/assets" + "/" + textFileName;
        WWW www = new WWW(path);
        yield return www;
        txtReader = new StringReader(www.text);
#endif

        mIsStamp = true;

        int count = 0;
        while ((txtBuffer = txtReader.ReadLine()) != null)
        {
            list.Add(new Vector2(float.Parse(txtBuffer), float.Parse(txtReader.ReadLine())));
            count++;
        }
        CreateBlockOnTouch(list[0]);
        for (int i = 1; i < list.Count; i++) { CreateBlockOnStay(list[i]); } 
        CreateBlockOnRelease();
    }

    IEnumerator LoadText(Vector2[] point)
    {
        List<Vector2> list = new List<Vector2>();
        list.AddRange(point);
        mIsStamp = true;
        CreateBlockOnTouch(list[0]);
        for (int i = 1; i < list.Count; i++) { CreateBlockOnStay(list[i]); }
        CreateBlockOnRelease();
        yield return null;
    }
}
