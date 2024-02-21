using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBase : MonoBehaviour
{
    private void OnMouseEnter()
    {
        CursorManager.Instance.SetNpcTalk();
    }

    private void OnMouseExit()
    {
        CursorManager.Instance.SetNormal();
    }
}
