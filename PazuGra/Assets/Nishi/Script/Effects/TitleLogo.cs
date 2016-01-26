using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleLogo : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Image image = transform.GetChild(0).gameObject.GetComponent<Image>();
        Image image1 = transform.GetChild(1).gameObject.GetComponent<Image>();
        LeanTween.alpha(image.rectTransform, 1, 1);
        LeanTween.alpha(image1.rectTransform, 1, 1).setOnComplete(() => { MenuMove(); });
        //LeanTween.moveLocalY(transform.GetChild(2).gameObject,-100,1.0f).setOnComplete(() => { Rainbow(); });
    }

    void Rainbow()
    {
        Image image = transform.GetChild(1).gameObject.GetComponent<Image>();
        LeanTween.value(gameObject, 0, 1, 1).setOnUpdate((float val) => { image.fillAmount = val; }).setOnComplete(() => { PenMove(); });

    }

    void PenMove()
    {
        LeanTween.moveLocal(transform.GetChild(2).gameObject,new Vector3(350,-70,0), 1.0f).setOnComplete(() => { MenuMove();});
    }

    void MenuMove()
    {
        LeanTween.moveLocalX(transform.GetChild(2).gameObject, 0, 1.0f);
        LeanTween.moveLocalX(transform.GetChild(3).gameObject, 0, 1.0f);
        LeanTween.moveLocalX(transform.GetChild(4).gameObject, 250.0f, 1.0f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeanTween.cancelAll();
            Image image = transform.GetChild(1).gameObject.GetComponent<Image>();
            image.color = new Color(1, 1, 1, 1);
            image = transform.GetChild(0).gameObject.GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            transform.GetChild(2).localPosition = new Vector3(0, -627.653f, 0);
            transform.GetChild(3).localPosition = new Vector3(0, -1162, 0);
            transform.GetChild(4).localPosition = new Vector3(250, -842.4279f, 0);
            //transform.GetChild(1).GetComponent<RectTransform>().position = new Vector3(350, -70, 0);
            //transform.GetChild(2).GetComponent<RectTransform>().position = new Vector3(0, -100, 0);
            //transform.GetChild(3).GetComponent<RectTransform>().position = new Vector3(0, -627.653f, 0);
            //transform.GetChild(4).GetComponent<RectTransform>().position = new Vector3(0, -1048, 0);
        }
    }
}
