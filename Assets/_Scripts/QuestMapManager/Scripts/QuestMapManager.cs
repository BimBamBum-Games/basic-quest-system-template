using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuniusGame.QuestMapManager.Scripts {

    public class QuestMapManager : MonoBehaviour {

        [SerializeField] QuestMapManagerSO questMapManagerSO;

        //Animasyon esnasinda kapatma acma tuslari gibi tuslarin etkilesimleri durdurulur.
        [SerializeField] CanvasGroup canvasGroup;

        [SerializeField] ParentContent parentContent;

        [SerializeField] QuestMapButtonWrapper questMapBtn;

        [SerializeField] int currentMapIndex;

        [SerializeField] QuestMapSO lastMapSO;

        [SerializeField] QuestMapRectWrapper questMapRectWrapper;

        [SerializeField] ItemSceneField itemSceneField;

        public UserIngredients userIngredients = new() { Coins = 500 };

        List<QuestItemSO> showableItems;

        public bool IsIEnumeratorActive = false;

        List<QuestMapButtonWrapper> questMapButtonWrappers = new();

        //SO ve buttonlarin aksyonlari ve referanslarina iliskin yapilandirmalar burada saglanir.
        public void InitializeQuestMapSystem() {

            showableItems = lastMapSO.GetDisplayable();

            foreach (QuestItemSO qiso in showableItems) {
                if(qiso.hasQuestMapButtonWrapper == false) {
                    qiso.hasQuestMapButtonWrapper = true;

                    //Bu alanlar serilestirilmis alanlar ve onceden preconfigure edilebilirler.
                    questMapBtn.questItemSO = qiso;
                    questMapBtn.userIngredients = userIngredients;
                    questMapBtn.itemSceneField = itemSceneField;
                    questMapBtn.ResetTransformValues();
                    

                    QuestMapButtonWrapper qmbw = Instantiate(questMapBtn, parentContent.transform);

                    //User interractionlarini animasyon esnasinda sabitler.
                    qmbw.SetCanvasGroupForUserInterraction(canvasGroup);

                    //Bu action alan prefab olusmadigi icin onceden atanamiyor cunku serilesitirilmis alan degildir.
                    qmbw.AddActionOnButtonClickEnd(CheckUpdateQuestItems);

                    //Destroy aninda ek ienumeratorlar.
                    qmbw.AddOnRequiredWarmUpAction(questMapRectWrapper.WarmUppSync);
                    qmbw.AddOnRequiredSartAction(questMapRectWrapper.TurnOffSync);
                    qmbw.AddOnRequiredEndAction(questMapRectWrapper.TurnOnnSync);
                }
            }
        }


        //Scriptible Objelere ait button attandiysa ilgili alani resetleyerek bir sonrakinde hazir duruma geri getirir.
        public void ResetQuestMapManagerSO() {
            questMapManagerSO.ResetQuestMaps();
        }

        //Current Map SO icin referans tutar.
        public void InitLastMap() {
            lastMapSO = questMapManagerSO.questMapsSOs[currentMapIndex];
        }

        //Bu metod diger iki metodu wraplar.
        public void CheckUpdateQuestItems() {
            InitLastMap();
            InitializeQuestMapSystem();
            Debug.LogWarning("UPDATED");
        }

        //Init islemleri awake esnasinda saglanir.
        private void Awake() {
            ResetQuestMapManagerSO();
            CheckUpdateQuestItems();
        }
    }

    [Serializable]
    public class UserIngredients {
        public int Coins;
    }
}

