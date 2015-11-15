using UnityEngine;
using System.Collections;

public class CaptureTexture : MonoBehaviour {

    [SerializeField]
    private RenderTexture target;
    //サブカメラから見たTextureの保存
    public Texture2D CaptureToPNG()
    {
        RenderTexture.active = target;
        Texture2D tex2d = new Texture2D(target.width, target.height, TextureFormat.ARGB32, false);
        tex2d.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
        RenderTexture.active = null;
        tex2d.Apply();
        return tex2d;
    }
}
