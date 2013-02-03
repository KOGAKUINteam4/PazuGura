using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
public class ScreenCapture : MonoBehaviour {
    
    [SerializeField]
    private RenderTexture target;
    [SerializeField]
    private GameObject mBaseUI;

    private UIContller mUICtr;
    private List<Texture2D> mTexList = new List<Texture2D>(); 

    // Use this for initialization
    void Start()
    {
        mUICtr = GameManager.GetInstanc.GetUIContller();
    }
    private void CaptureToPNG()
    {
        RenderTexture.active = target;
        Texture2D tex2d = new Texture2D(target.width, target.height, TextureFormat.ARGB32, false);
        tex2d.ReadPixels(new Rect(0, 0, target.width, target.height),0, 0);
        RenderTexture.active = null;

        mTexList.Add(tex2d);

        DebugTexture(tex2d);
        tex2d.Apply();
        //byte[] pngData = tex2d.EncodeToPNG();
        //File.WriteAllBytes(Application.dataPath + "/" + Def.Res + "/" + Def.DeleteTexture + "/Src.png", pngData);
    }


    public IEnumerator RenderTextureOutPut()
    {
        yield return new WaitForEndOfFrame();
        CaptureToPNG();
        CreateUI().GetComponent<Image>().sprite = Sprite.Create(mTexList[mTexList.Count - 1], new Rect(0, 0, 256, 256), Vector2.zero);
    }

    private GameObject CreateUI()
    {
        GameObject ui = Instantiate(mBaseUI) as GameObject;
        ui.transform.SetParent(mUICtr.SearchParent(Layers.Layer_Def).transform,false);
        return ui;
    }

    //白と黒の比率
    private void DebugTexture(Texture2D texture)
    {
        float white = 0;
        float black = 0;
        Debug.Log(texture.GetPixel(0, 0));
        foreach (var i in texture.GetPixels())
        {
            //Debug.Log(i);
            if (i.a < 0.3f) black++;
            else white++;
        }
        Debug.Log("white : " + white);
        Debug.Log("black : " + black);

        Debug.Log(white / black * 100 + "%");
    }

    public void Trigger()
    {
        // Capture Trigger
        //gameObject.SetActive(false);

        Invoke("CaptureToPNG", 1.0f);
        //CaptureToPNG();
    }
}
