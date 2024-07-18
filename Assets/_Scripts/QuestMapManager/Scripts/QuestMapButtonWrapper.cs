using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace JuniusGame.QuestMapManager.Scripts {

    public class QuestMapButtonWrapper : MonoBehaviour {
        [field: SerializeField] public Button button;
        [field: SerializeField] public TextMeshProUGUI questDescription;
        [field: SerializeField] public TextMeshProUGUI priceTmp;
        [field: SerializeField] public QuestMapButtonWrapper questMapBtn;

        public QuestItemSO questItemSO;
        public UserIngredients userIngredients;
        
        public static bool IsIEnumeratorActive = false;

        private Coroutine questMapButtonwrapperCrt;

        Sequence tweenSequence;

        public ItemAnimationBase itemAnimationBase;
        public ItemSceneField itemSceneField;

        //Animasyonlar esnasinda user interractionlarini kisitlar.
        private CanvasGroup canvasGroupForUserInterraction;

        private void OnEnable() {
            button.onClick.AddListener(() => { OnButtonClickSync(); });
            questDescription.text = questItemSO.description;
            priceTmp.text = questItemSO.price.ToString();
        }

        //Bu scriptle birlikte CanvasGroup baglanirsa animasyon oynatilirken tetiklemeler raycastler otomatikman engellenir.

        public void SetCanvasGroupForUserInterraction(CanvasGroup cnvgrp) {
            canvasGroupForUserInterraction = cnvgrp;
        }

        public void DeActivate_UserInterractionWithCanvasGroup() {
            canvasGroupForUserInterraction.blocksRaycasts = true;
        }

        public void Activate_UserInterractionWithCanvasGroup() {
            canvasGroupForUserInterraction.blocksRaycasts = false;
        }


        //Varsayilan scale degerine geri ceker.
        public void ResetTransformValues() {
            transform.localScale = Vector2.one;
        }

        //Disaridan Guncelleme tetiklemesi kullandirilir.
        public Action OnButtonClickEnd { get; set; }
        public void AddActionOnButtonClickEnd(Action onButtonClickEnd) {
            Debug.LogWarning("ACTIONLAR ATANDI!");
            OnButtonClickEnd = onButtonClickEnd;
        }

        //IEnumerator tipi action.
        public delegate IEnumerator QuestMapButtonWrapperAction();
        QuestMapButtonWrapperAction OnRequiredWarmUpAction, OnRequiredStartAction, OnRequiredMiddleAction, OnRequiredEndAction;

        public void AddOnRequiredSartAction(QuestMapButtonWrapperAction action) {
            OnRequiredStartAction = action;
        }

        public void AddOnRequiredEndAction(QuestMapButtonWrapperAction action) {
            OnRequiredEndAction = action;
        }

        public void AddOnRequiredWarmUpAction(QuestMapButtonWrapperAction action) {
            OnRequiredWarmUpAction = action;
        }

        public void AddOnRequiredMiddleAction(QuestMapButtonWrapperAction action) {
            OnRequiredMiddleAction = action;
        }

        private void OnDisable() {
            //Her durumda onclick delegesini temizlemek mantiklidir. //Baska prefablarda null durumu meydana getirirse scriptler null olan objeler vardir demektir.
            button.onClick.RemoveAllListeners();
            OnButtonClickEnd = null;
            DOTween.Kill(tweenSequence); 
        }

        //QuestItem buttonuna basildiginda eger yeterli kaynak varsa satin alma yapar.

        public void OnButtonClickSync() {
            questMapButtonwrapperCrt = StartCoroutine(OnButtonClick());
        }

        public IEnumerator OnButtonClick() {

            //Debug.LogWarning($"Button Clicked > {questItemSO.questItemName}."); Test.

            if(IsIEnumeratorActive == false) {

                IsIEnumeratorActive = true;

                if (questItemSO.IsPurchased == false) {

                    if (userIngredients.Coins >= questItemSO.price) {

                        userIngredients.Coins -= questItemSO.price;

                        //Ornek olusturup mapa yerlestir. Transform SetParent false cekilirse dunya degil local koordinatlara gore olcekleme yapilir. Dogru calisir UI icin.
                        itemAnimationBase = questItemSO.GetFactoryQuestItemSO();
                        itemAnimationBase.transform.SetParent(itemSceneField.GetComponent<RectTransform>(), false);
                        itemAnimationBase.gameObject.SetActive(true);

                        questItemSO.IsPurchased = true;

                    }
                }

                yield return PlayAnimationAfterPay();
                
                IsIEnumeratorActive = false;

                questMapButtonwrapperCrt = null;

            }    
        }

        //Kaybolma animasyonu kosullar saglanirsa gerceklesmis olur.
        public IEnumerator PlayAnimationAfterPay() {

            //User hareketlerini sinirlandir.
            Activate_UserInterractionWithCanvasGroup();

            bool canWaitTillTweenEnd = true;

            Sequence mySequence = DOTween.Sequence();

            Tween t1 = transform.DOScale(1.1f, 0.32f).SetEase(Ease.OutCubic);
            Tween t2 = transform.DOScale(0, 0.32f).SetEase(Ease.Linear);

            mySequence.Append(t1).Append(t2).OnComplete(() => canWaitTillTweenEnd = false);

            //canWaitTillTweenEnd false olana kadar bekletilsin ki ikinci bir tetikleme gelip cakismalar olmasin.
            yield return new WaitUntil(() => canWaitTillTweenEnd == false);

            yield return OnRequiredWarmUpAction.Invoke();
            yield return OnRequiredStartAction?.Invoke();
            yield return OnRequiredMiddleAction?.Invoke();

            //Tamamen test kontrol amacli randomlama ile item yerlestirme yapiliyor.
            if(itemAnimationBase != null) {
                Vector3 randomVec = new Vector3(UnityEngine.Random.Range(-540, 540), UnityEngine.Random.Range(-960, 960));
                yield return itemAnimationBase.ExecuteAnimation(randomVec);
            }

            yield return OnRequiredEndAction?.Invoke();

            Debug.LogWarning($"Quest Map Button Wrapper Name > {name}.");
            OnButtonClickEnd?.Invoke();

            //User hareketlerini aktif tekrar et.
            DeActivate_UserInterractionWithCanvasGroup();
            Destroy(gameObject);
        }
    }
}


