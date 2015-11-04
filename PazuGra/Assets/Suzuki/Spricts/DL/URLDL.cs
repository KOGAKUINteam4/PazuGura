using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
public class URLDL : MonoBehaviour {

    [SerializeField]
    private string mURL = "tysonew.xsrv.jp/Tyson.txt";

    [SerializeField]
    private string mPhp = "tysonew.xsrv.jp/Test.php";

    [SerializeField]
    private List<int> mResult = new List<int>();

    private void Start()
    {

        //StartCoroutine(Add());
        //
        
        StartCoroutine(GetResult());

        //StartCoroutine(DL());
    }

    private IEnumerator Add()
    {
        var url1 = "tysonew.xsrv.jp/Result.php";

        WWWForm wwwForm = new WWWForm();

        //指定urlにデータを送信
        wwwForm.AddField("score", "1");

        WWW gettext = new WWW(url1, wwwForm);

        // レスポンスを待つ
        yield return gettext;

        Debug.Log(gettext.text);
    }

    private IEnumerator Post()
    {
        string url = mPhp;

        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("w", "\nTysonEW");
        WWW req  = new WWW(url,wwwForm);
        Debug.Log("書き込み"+req);
        yield return req;
        if (req.error != null)
        {
            Debug.Log("エラー");
        }
        if (req.error == null)
        {
            Debug.Log("登録");
        }
    }

    private IEnumerator GetResult()
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("keyword", "data");//不正接続防止用キーワード
        WWW result = new WWW("tysonew.xsrv.jp/GetResult.php",wwwForm);
        yield return result;

        //Debug.Log(result.text);
        //var json = MiniJSON.Json.Deserialize(result.text) as List<object>;// as Dictionary<string,object>;
        //Debug.Log(json);

        //Debug.Log("score : " + json["score"]);

        if (result.error == null)
        {
            string txt = result.text.Remove(0, 2);
            JSONObject rdbUserGet = new JSONObject(txt);
            for (int i = 0; i < rdbUserGet.Count; ++i)
            {
                JSONObject jsonPos = rdbUserGet[i];
                JSONObject jsonUid = jsonPos.GetField("score");
                Debug.Log("Score : " + jsonUid.str);
                mResult.Add(int.Parse(jsonUid.str));
            }

        }

        if (result.error != null)
        {
            Debug.Log(result.error);
        }
    }

	 IEnumerator DL () {

         Debug.Log("DLStart");
		using(WWW www = new WWW(mURL)){
 
			yield return www;
            Debug.Log(www.text);
			if(!string.IsNullOrEmpty(www.error)){
 
				Debug.LogError("www Error:" + www.error);
				yield break;
 
			}
 
			Debug.Log(www.text);
            Debug.Log("DLEND");
		}
	}


    [ContextMenu("Debug")]
    private void Print()
    {
        foreach (int i in mResult)
        {
            Debug.Log("Result : "+i);
        }
    }

}
