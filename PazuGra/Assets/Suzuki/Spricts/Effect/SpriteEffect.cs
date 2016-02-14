using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpriteEffect : MonoBehaviour {

    [SerializeField]
    private GameObject mEffect;
    private Vector3 mInstancePoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Create();
        }
	}

    public void Create(GameObject target,Color color)
    {
        int max = 10;
        for (int i = 0; i < max; i++)
        {
            GameObject particle = Instantiate(mEffect, target.transform.position,Quaternion.identity) as GameObject;
            particle.transform.SetParent(GameManager.GetInstanc.GetUIContller().SearchParent(Layers.Layer_Front).transform, false);
            particle.transform.localPosition = target.transform.localPosition;
            particle.GetComponent<Image>().color = color;
            Vector3 point = target.transform.position;
            mInstancePoint = particle.transform.localPosition;
            //iTween.MoveTo(particle, iTween.Hash("position", new Vector3(Mathf.Cos((1 + i) * 360 / max), Mathf.Sin((1 + i) * 360 / max), 0) + point, "time", 0.3f, "oncomplete",
            //    "ReturnParticle", "oncompletetarget", this.gameObject, "oncompleteparams", particle, "easetype", iTween.EaseType.easeInCirc));
            iTween.MoveTo(particle, iTween.Hash("position", new Vector3(Mathf.Cos((1 + i) * 360 / max), Mathf.Sin((1 + i) * 360 / max), 0) + point, "time", 0.3f,"easetype", iTween.EaseType.easeInCirc));

            iTween.MoveTo(particle, iTween.Hash("position", point, "time", 0.2f, "delay", 0.4f, "easetype", iTween.EaseType.easeInCirc));

            iTween.MoveTo(particle, iTween.Hash("position", GameObject.Find("ScoreParent").transform.position, "time", 0.3f, "delay", 0.7f, "oncomplete", "Remove", "oncompletetarget", this.gameObject, "oncompleteparams", particle, "easetype", iTween.EaseType.easeInCirc));
        }
    }

    private void Create()
    {
        int max = 10;
        for (int i = 0; i < max; i++)
        {
            GameObject particle = Instantiate(mEffect);
            particle.transform.localPosition = transform.position;
            particle.transform.SetParent(transform,false);
            mInstancePoint = particle.transform.position;
            //iTween.MoveTo(particle, new Vector3(Mathf.Cos((1 + i) * 360 / max), Mathf.Sin((1 + i) * 360 / max), 0) + transform.position, 0.5f);
            iTween.MoveTo(particle, iTween.Hash("position", new Vector3(Mathf.Cos((1 + i) * 360 / max), Mathf.Sin((1 + i) * 360 / max), 0) + transform.position, "time", 0.3f, "oncomplete",
                "ReturnParticle","oncompletetarget",this.gameObject,"oncompleteparams",particle,"easetype",iTween.EaseType.easeInCirc));
        }
    }

    private void ReturnParticle(GameObject target)
    {

        iTween.MoveTo(target, iTween.Hash("position", mInstancePoint, "time", 0.2f, "oncomplete", "Add", "oncompletetarget", this.gameObject, "oncompleteparams", target,  "easetype",iTween.EaseType.easeInCirc));
    }

    private void Add(GameObject target)
    {
        iTween.MoveTo(target, iTween.Hash("position", GameObject.Find("ScoreParent").transform.position, "time", 0.3f, "oncomplete", "Remove", "oncompletetarget", this.gameObject, "oncompleteparams", target, "easetype", iTween.EaseType.easeInCirc));
    }

    private void Remove(GameObject target)
    {
        Destroy(target);
        //Debug.Log("delete");
    }

}
