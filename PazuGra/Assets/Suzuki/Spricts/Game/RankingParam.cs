using UnityEngine;
using System.Collections;

public class RankingParam : MonoBehaviour {

    public int mScore;
    public int mChain;
    public int mCombo;
    public int mDrowCount;

    public void Init()
    {
        mScore = 0;
        mChain = 0;
        mCombo = 0;
        mDrowCount = 0;
    }
}
