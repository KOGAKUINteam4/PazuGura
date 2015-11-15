using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    
    //Line引く用
    [SerializeField]
    private GameObject mLine;
    private GameObject mLineObject;

    //Add 虹のブロックの生成条件
    public bool isRainbow = false;

    private float mCameraView = 30*2;

    // Use this for initialization
    void Start () {
        mCaptureTexture = GameObject.Find("CaptureTexture").GetComponent<CaptureTexture>();
        mPolygonMaker = GameObject.Find("PolygonMaker").GetComponent<PolygonMaker>();
        mMainCamera = GameObject.Find("Sub Camera");
        mPolygonMaker.Init();
	}

    private void Update()
    {
        if (!mIsShoot){
            if (Input.GetMouseButtonDown(0)) CreateBlockOnTouch();
            if (Input.GetMouseButton(0)) CreateBlockOnStay();
            if (Input.GetMouseButtonUp(0))CreateBlockOnRelease();
        }

        UpdateInstanceUI();
    }

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
        mTouchPosition = mouseWorldPos;
        mLineObject = Instantiate(mLine) as GameObject;
    }

    //画面を押されている。
    public void CreateBlockOnStay()
    {
        //インフォメーションの更新
        Vector3 mouseWorldPos = MathPos();
        if (mPolygonMaker.IsMakeDistance(mouseWorldPos)) AddPoint(mouseWorldPos);
        //頂点をまたいだ場合
        mPolygonMaker.OnCross();
        mLineObject.transform.position = new Vector3(MathPos().x / mCameraView, MathPos().y / mCameraView, 0);
        //mLineObject.transform.position = new Vector3(MathPos().x/50.0f, MathPos().y/50.0f, 0);
    }

    //画面を押され、リリースされたとき
    public void CreateBlockOnRelease()
    {
        Destroy(mLineObject);
        if (!mPolygonMaker.IsMakeLine()) return;
        //画面上に生成
        //サブカメラに写るポリゴンの生成
        StartCoroutine(mPolygonMaker.AnsyCreatePolygon(CreateTexture));
        //mPolygonMaker.CreatePolygon();
        //Textureの作成
        //StartCoroutine(RenderTextureOutPut());
        mIsShoot = true;
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

        if (Input.GetMouseButtonUp(0)){
            Vector2 mouse = MathPos();
            Vector2 vec = new Vector2(mouse.x - mTouchPosition.x, mouse.y-mTouchPosition.y);
            Rigidbody2D gravity = mInstanceUI.GetComponent<Rigidbody2D>();
            gravity.isKinematic = false;
            gravity.AddForce(vec.normalized * 50.0f,ForceMode2D.Impulse);
            if (isRainbow)mInstanceUI.transform.GetChild(0).GetComponent<Image>().sprite = mPolygonMaker.m_RainbowSprit;
            else mInstanceUI.transform.GetChild(0).GetComponent<Image>().color = mPolygonMaker.RandomColor();
            //フラグ等の初期化
            isRainbow = false;
            mInstanceUI = null;
            mIsShoot = false;
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
        //panelのローカル座標に
        return mouseWorldPos * mCameraView;
        //return mouseWorldPos * 50;
    }

    //イベント駆動で虹を発生させる
    public void ComboSend()
    {
        isRainbow = true;
    }
}
