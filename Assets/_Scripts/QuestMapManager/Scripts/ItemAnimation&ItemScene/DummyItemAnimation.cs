using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyItemAnimation : ItemAnimationBase {
    public override IEnumerator ExecuteAnimation(Vector3 nextPos) {
        Tween t1 = ItselfRct.DOAnchorPos(nextPos, 1f).SetEase(Ease.InOutCubic);
        yield return t1.WaitForCompletion();
    }
}
