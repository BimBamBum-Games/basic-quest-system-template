using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMapRectWrapper : MonoBehaviour
{
    //Turn Panel Off button
    [SerializeField] Button turnOffBtn;

    //Canvas altindaki wrapper komponenti altindaki tum cocuklari kapatir.
    private RectTransform questMapRectWrapper;

    public void Awake() {
        questMapRectWrapper = GetComponent<RectTransform>();
    }

    public void OnEnable() {
        turnOffBtn.onClick.AddListener(TurnOffQuestMapPanel);
    }

    public void OnDisable() {
        turnOffBtn.onClick.RemoveListener(TurnOffQuestMapPanel);
    }

    //Ilk Raise esnasindaki tween hareketidir.
    public IEnumerator WarmUppSync() {
        Tween tween = questMapRectWrapper.DOScale(1.2f, 0.2f).SetEase(Ease.InOutSine);
        yield return tween.WaitForCompletion();
    }

    //Paneli komple kapat. Callback olarak kapatma buttonuna eklenecektir.
    public void TurnOffQuestMapPanel() {
        gameObject.SetActive(false);
    }

    //Paneli komple acar. Callback olarak acma buttonuna eklenecektir.
    public void TurnOnnQuestMapPanel() {
        gameObject.SetActive(true);
    }

    //Acilis esnasinda veya eski haline gelme durumundaki tween hareketidir.
    public IEnumerator TurnOnnSync() {
        Tween tween = questMapRectWrapper.DOScale(1, 0.6f).SetEase(Ease.InOutSine)
            .OnStart(DebugLoggerOnTurnOnnStart)
            .OnComplete(DebugLoggerOnTurnOnnEnd);
        yield return tween.WaitForCompletion();
    }

    //Kapanis esnasindaki tween hareketidir.
    public IEnumerator TurnOffSync() {
        Tween tween = questMapRectWrapper.DOScale(0, 0.3f).SetEase(Ease.InOutSine)
            .OnStart(DebugLoggerOnTurnOffStart)
            .OnComplete(DebugLoggerOnTurnOffEnd);
        yield return tween.WaitForCompletion();
    }

    //Debug metodlar.
    public void DebugLoggerOnTurnOnnStart() {
        Debug.LogWarning("Turned Onn Started!");
    }

    public void DebugLoggerOnTurnOnnEnd() {
        Debug.LogWarning("Turned Onn Ended!");
    }

    public void DebugLoggerOnTurnOffStart() {
        Debug.LogWarning("Turned Off Started!");
    }

    public void DebugLoggerOnTurnOffEnd() {
        Debug.LogWarning("Turned Off Ended!");
    }
}
