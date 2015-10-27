using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface IRecieveMessage : IEventSystemHandler
{
    void AddTime();
    //void ComboStart();
}

