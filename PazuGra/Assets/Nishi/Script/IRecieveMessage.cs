using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface IRecieveMessage : IEventSystemHandler
{
    void ComboSend();
}

