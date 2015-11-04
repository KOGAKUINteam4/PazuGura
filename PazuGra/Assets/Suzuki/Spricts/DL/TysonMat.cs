using UnityEngine;
using System.Collections;
using DelegateFunction;
using UnityEngine.UI;
public class TysonMat : MonoBehaviour {

    [SerializeField]
    Material mat;

    [Range(0, 1)]
    public float range;

    [SerializeField]
    private Image mInage;

    [SerializeField]
    private Slider mSlider;


    private void Start()
    {
        Init(Call);
        
    }

    private void Init(FunctionReturn func)
    {
        func(this.gameObject);
    }

    private void Call(GameObject target)
    {
        Debug.Log("Call : "+target.name);
    }

    void Update()
    {
        mat.SetFloat("_Range", range);
        mat.SetFloat("_Range", mSlider.value);
    }
}
