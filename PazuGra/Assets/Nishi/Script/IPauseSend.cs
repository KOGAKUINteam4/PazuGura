using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface IPauseSend : IEventSystemHandler
{
    void PauseSend(bool result);
}
