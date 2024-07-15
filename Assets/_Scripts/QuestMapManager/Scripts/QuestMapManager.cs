using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.QuestMapManager.Scripts {

    public class QuestMapManager : MonoBehaviour {

        [SerializeField] QuestMapManagerSO questMapManagerSO;
        [SerializeField] ParentContent parentContent;
        [SerializeField] QuestMapButtonWrapper questMapBtn;

        [SerializeField] int currentMapIndex;
        [SerializeField] QuestMapSO lastMapSO;

        public void InitializeQuestMapSystem() {

            List<QuestItemSO> showableItems = lastMapSO.GetDisplayable();

            foreach (QuestItemSO qiso in showableItems) {
                //Debug.LogWarning($"{qiso.name}.");
                QuestMapButtonWrapper qmbw = Instantiate(questMapBtn, parentContent.transform);

                qmbw.priceTmp.text = qiso.price.ToString();
                qmbw.questDescription.text = qiso.description;


                //GetQuestItemSO(qiso);
            }
        }

        //public void GetQuestItemSO(QuestItemSO item) {
        //    foreach (QuestItemSO subItem in item.subQuestItemSOs) {
        //        Instantiate(questMapBtn, parentContent.transform);
        //    }
        //}

        public void InitLastMap() {
            lastMapSO = questMapManagerSO.questMapsSOs[currentMapIndex];
        }

        private void Awake() {
            InitLastMap();
            InitializeQuestMapSystem();
        }

    }
}

