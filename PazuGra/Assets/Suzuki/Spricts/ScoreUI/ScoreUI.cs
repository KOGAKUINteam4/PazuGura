﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//6桁の桁数で作る。
public class ScoreUI : MonoBehaviour {

    [SerializeField]
    private Sprite[] mSprite = new Sprite[10];
    private GameObject[] mCounter = new GameObject[6];
    private GameObject[] mScoreCounter = new GameObject[3];
    [SerializeField]
    private float mSocre = 0;

    private GameObject mScoreParent;

	// Use this for initialization
	void Start () {
        Init();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddScore(700);
            UpdateScore(700);
            CreateEffect();
        }
    }

    private void Init()
    {
        int counter = 0;
        foreach(Transform i in gameObject.transform){
            mCounter[counter] = i.gameObject;
            counter++;
        }
        counter = 0;
        foreach (Transform i in GameObject.Find("Score").transform){
            mScoreCounter[counter] = i.gameObject;
            counter++;
        }
        mScoreParent = GameObject.Find("Score");
        mScoreParent.SetActive(false);
    }

    public void AddScore(float score)
    {
        mSocre += score;
    }

    public int GetScore()
    {
        return (int)mSocre;
    }

    private void UpdateScore(float score)
    {
        for (int i = 1; i < 3; i++){
            mScoreCounter[i - 1].GetComponent<Image>().sprite = GetScoreSprite(i+3,score);
        }
        for (int i = 1; i < 6; i++){
            SetNumber(i);
        }
    }

    private void CreateEffect()
    {
        mScoreParent.SetActive(true);
        // 現在位置からx:-20までランダムに揺れる
        iTween.ShakePosition(mScoreParent, 
            iTween.Hash("y", -0.1, "islocal", false, "time", 0.4f, "oncomplete", "NotActive","oncompletetarget", this.gameObject));
    }

    private void NotActive()
    {
        mScoreParent.SetActive(false);
    }

    //桁数に応じた数字のスプライトを返す。
    private Sprite GetScoreSprite(int score,float value)
    {
        int scoreValue = (int)value;
        string length = scoreValue.ToString();
        for (int i = length.Length; i < 6; i++) { length = "0" + length;}
        return mSprite[int.Parse(length.Substring(score-1, 1))];
    }

    public void SetNumber(int num)
    {
        mCounter[num - 1].GetComponent<Image>().sprite = GetScoreSprite(num,mSocre);
    }
}