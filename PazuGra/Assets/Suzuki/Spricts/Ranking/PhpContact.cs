using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using DelegateFunction;
using UnityEngine.UI;
public class PhpContact : MonoBehaviour {

    [SerializeField]
    private List<int> mResult = new List<int>();

    public void DateBaseUpdate(int score)
    {
        StartCoroutine(Add(score));
    }

    public void UpdateRankingDate(IsFunction mFunction,IsFunction mFunc)
    {
        StartCoroutine(GetResult(mFunction,mFunc));
    }

    public List<int> GetRankingDate()
    {
        return mResult;
    }

    private IEnumerator Add(int score)
    {
        if(score == 0)yield break;
        //http://
        var url1 = "http://tysonew.xsrv.jp/Result.php";

        WWWForm wwwForm = new WWWForm();

        //指定urlにデータを送信
        wwwForm.AddField("score", score.ToString());

        WWW gettext = new WWW(url1, wwwForm);

        //Debug.Log("Add"+score);

        // レスポンスを待つ
        yield return gettext;
    }

    private IEnumerator GetResult(IsFunction mFunction, IsFunction mFunction2)
    {
        yield return new WaitForSeconds(2.5f);
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("keyword", "data");//不正接続防止用キーワード
        //http://
        WWW result = new WWW("http://tysonew.xsrv.jp/GetResult.php", wwwForm);
        mResult.Clear();
        //GameObject.Find("Php").GetComponent<Text>().text = "false";
        yield return result;

        if (result.error == null)
        {
            string txt = result.text.Remove(0, 2);
            JSONObject rdbUserGet = new JSONObject(txt);
            for (int i = 0; i < rdbUserGet.Count; ++i)
            {
                JSONObject jsonPos = rdbUserGet[i];
                JSONObject jsonUid = jsonPos.GetField("score");
                //Debug.Log("Score : " + jsonUid.str);
                mResult.Add(int.Parse(jsonUid.str));
            }
            yield return new WaitForSeconds(1.0f);
            mFunction();
            if (mFunction2 != null) mFunction2();
            //GameObject.Find("Php").GetComponent<Text>().text = "OK";
        }

        if (result.error != null)
        {
            //Debug.Log(result.error);
            //GameObject.Find("Php").GetComponent<Text>().text = result.text;
            //GameObject.Find("Php").GetComponent<Text>().text = result.error.Length.ToString();
            //log(result.error);
        }
    }



    void Update()
    {
        //log(" Time sinse fromStartup : " + Time.realtimeSinceStartup + "[12345678901234567890]");
        //return;
    }

    // ログの記録
    private static List<string> logMsg = new List<string>();
    public static void log(string msg)
    {
        logMsg.Add(msg);
        // 直近の5件のみ保存する
        if (logMsg.Count > 5)
        {
            logMsg.RemoveAt(0);
        }
    }

    // 記録されたログを画面出力する
    void OnGUI()
    {
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);

        // フォントサイズを10px,白文字にする。
        // styleのnewは遅いため、本来はStart()で実施すべき...
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        style.fontStyle = FontStyle.Normal;
        style.normal.textColor = Color.white;

        // 出力された文字列を改行でつなぐ
        string outMessage = "";
        foreach (string msg in logMsg)
        {
            outMessage += msg + System.Environment.NewLine;
        }

        // 改行でつないだログメッセージを画面に出す
        GUI.Label(rect, outMessage, style);
    }
}
