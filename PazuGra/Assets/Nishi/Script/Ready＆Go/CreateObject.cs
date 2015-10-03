using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;

public class CreateObject : MonoBehaviour, IEventSystemHandler
{

    public GameObject Obj;

    public void CreateObj()
    {
        GameObject temp;
        temp = (GameObject)Instantiate(Obj,Obj.transform.position,Quaternion.identity);
        temp.transform.parent = gameObject.transform.parent;
        temp.transform.position = gameObject.transform.position;
        temp.transform.localScale = Vector3.one;
    }

}
