using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//スタンプのコスト定義

namespace TemplateStampPoint
{
    public class TemplateStamp : MonoBehaviour
    {
        [SerializeField][Range(0,100)]
        private int mStampCost = 0;

        private Text mText;

        public void Init()
        {
            mStampCost = 0;
        }

        public void AddCost(int cost)
        {
            mStampCost += cost;
        }

        public int GetCost()
        {
            return mStampCost;
        }

        private void Start()
        {
            mText = GetComponent<Text>();
        }

        //TODO　値が更新されたら・・・・にする。
        private void Update()
        {
            mText.text = mStampCost.ToString();
        }
    }
}