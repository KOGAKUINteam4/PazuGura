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
        [SerializeField]
        private Sprite[] m_Nums;

        [SerializeField]
        private Image m_1Digit = null, m_10Digit = null;

        private Vector3 fixPosition;
        //private Text mText;

        public void Init()
        {
            mStampCost = 0;
            m_1Digit.enabled = true;
            m_10Digit.enabled = false;
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
            //mText = GetComponent<Text>();
            m_1Digit.enabled = true;
            m_10Digit.enabled = false;
            fixPosition = m_1Digit.rectTransform.position + new Vector3(0.2f, 0f, 0f);
        }

        //TODO　値が更新されたら・・・・にする。
        private void Update()
        {
            if (mStampCost >= 10)
            {
                m_10Digit.enabled = true;
                m_1Digit.rectTransform.position = fixPosition;
            }
            else
            {
                m_10Digit.enabled = false;
                m_1Digit.rectTransform.position = (fixPosition + new Vector3(-0.2f, 0f, 0f));
            }

            m_10Digit.sprite = m_Nums[(mStampCost / 10) % 10];
            m_1Digit.sprite = m_Nums[(mStampCost / 1) % 10];

            //mText.text = mStampCost.ToString();
            if (GetCost() <= 0) mStampCost = 0;
        }
    }
}