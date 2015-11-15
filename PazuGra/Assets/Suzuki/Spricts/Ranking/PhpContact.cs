﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using DelegateFunction;
public class PhpContact : MonoBehaviour {

    [SerializeField]
    private List<int> mResult = new List<int>();

    public void DateBaseUpdate(int score)
    {
        StartCoroutine(Add(score));
    }

    public void UpdateRankingDate(IsFunction mFunction)
    {
        StartCoroutine(GetResult(mFunction));
    }

    public List<int> GetRankingDate()
    {
        return mResult;
    }

    private IEnumerator Add(int score)
    {
        var url1 = "tysonew.xsrv.jp/Result.php";

        WWWForm wwwForm = new WWWForm();

        //指定urlにデータを送信
        wwwForm.AddField("score", score.ToString());

        WWW gettext = new WWW(url1, wwwForm);

        // レスポンスを待つ
        yield return gettext;

        Debug.Log(gettext.text);
    }

    private IEnumerator GetResult(IsFunction mFunction)
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("keyword", "data");//不正接続防止用キーワード
        WWW result = new WWW("tysonew.xsrv.jp/GetResult.php", wwwForm);
        yield return result;

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
            mFunction();
        }

        if (result.error != null)
        {
            Debug.Log(result.error);
        }
    }
}