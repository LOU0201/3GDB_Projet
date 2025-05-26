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
    public void AnimBeg()
    {
        _parent.GetComponentInParent<MoveDownWard>().CantMove();
    }

    public void Land()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/V3/Player/Land");
    }
}
