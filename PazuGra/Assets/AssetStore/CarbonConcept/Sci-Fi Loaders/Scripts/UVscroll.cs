using UnityEngine;
using System.Collections;

public class UVscroll : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        rend.material.SetTextureOffset("_BumpMap", new Vector2(offset, 0));
    }
}