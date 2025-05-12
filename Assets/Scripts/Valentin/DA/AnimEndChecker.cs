using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEndChecker : MonoBehaviour
{
    public GameObject _parent;
    public void AnimEnd()
    {
        _parent.GetComponentInParent<MoveDownWard>().TPMove();
    }
}
