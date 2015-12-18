using UnityEngine;
using System.Collections;

public class LineEffectManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_LineEffectPrefab;

    private float timer = 0;
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if(timer > 0.7f)
        {
            timer = 0;
            int i = 0;
            if (Random.Range(0, 2) >= 1)
            {
                i = 1;
            }
            else
            {
                i = -1; 
            }
            Vector3 pos = new Vector3(520 * i, Random.Range(-1000, 1000), 0);
            GameObject temp =  (GameObject)Instantiate(m_LineEffectPrefab, pos, Quaternion.identity);
            temp.transform.SetParent(transform,false);
        }
	
	}
}
