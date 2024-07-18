using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAnimationBase : MonoBehaviour {

    public RectTransform ItselfRct;
    public abstract IEnumerator ExecuteAnimation(Vector3 nextPos);
}