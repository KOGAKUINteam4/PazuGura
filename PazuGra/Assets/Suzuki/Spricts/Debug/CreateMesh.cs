using UnityEngine;
using System.Collections;

public class CreateMesh : MonoBehaviour {

    public int divisionX = 200;
    public int divisionY = 200;
    public float sizeX = 10f;
    public float sizeY = 10f;
    public float opCupCoef = 6f;
    public string saveAsAnAssetInPath = "Assets/OpMesh.asset";

    Vector3 Op(float x, float y)
    {
        float z = (
            opCupCoef * Mathf.Exp(
                -(
                    Mathf.Pow(2f / 3f * Mathf.Abs(x) - 1f, 2f)
                    + Mathf.Pow(2f / 3f * y, 2f)
                )
                - 1f / 3f * Mathf.Pow(2f / 3f * y + .5f, 3f)
            )
            + 2f / 3f * Mathf.Exp(
                -Mathf.Pow(2.818f, 11f) * Mathf.Pow(
                    Mathf.Pow(Mathf.Abs(2f / 3f * x) - 1f, 2f)
                    + Mathf.Pow(2f / 3f * y, 2f)
                , 2f)
            )
            - Mathf.Pow(2f / 3f * x, 4f)
        ) / 8f;
        return new Vector3(x, y, -z);
    }

    Mesh CreateMeshFunc()
    {
        int countX = divisionX + 1;
        int countY = divisionY + 1;
        var vertices = new Vector3[countX * countY];
        var uv = new Vector2[countX * countY];
        int k = 0;
        for (int i = 0; i <= divisionY; i++)
        {
            for (int j = 0; j <= divisionX; j++)
            {
                float u = (float)j / divisionX;
                float v = (float)i / divisionY;
                float x = (u - .5f) * sizeX;
                float y = (v - .5f) * sizeY;
                vertices[k] = Op(x, y);
                uv[k++].Set(u, v);
            }
        }

        var triangles = new int[6 * divisionX * divisionY];
        int l = 0, kTL = 0, kTR = 1, kBL = countX, kBR = countX + 1;
        for (int i = 0; i < divisionY; i++)
        {
            for (int j = 0; j < divisionX; j++)
            {
                triangles[l++] = kTL;
                triangles[l++] = kBL++;
                triangles[l++] = kBR;
                triangles[l++] = kTR++;
                triangles[l++] = kTL++;
                triangles[l++] = kBR++;
            }
            kTL++; kTR++; kBL++; kBR++;
        }

        var mesh = new Mesh();
        mesh.name = "OpMesh";
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        return mesh;
    }

	// Use this for initialization
	void Start () {
        CreateMeshFunc();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
