using UnityEngine;
using System.Collections;
namespace TemplateStampPoint
{
    public class TemplateStamp : MonoBehaviour
    {
        Vector3 mStarLeft = new Vector3(-100,30,0);
        Vector3 mStarBottomLeft = new Vector3(-77, -160, 0);
        Vector3 mStarTop = new Vector3(0, 160, -10);
        Vector3 mZ = new Vector3(0,0,-10);

        public Vector3[] StarTemplate;
        private void Start()
        {
            StarTemplate = new Vector3[6] { new Vector3(-100, 30, 0) + mZ, new Vector3(100, 30, 0) + mZ, mStarBottomLeft + mZ,mStarBottomLeft + mZ + new Vector3(3,3,0), mStarTop, new Vector3(77, -160, 0) + mZ };
        }
    }
}