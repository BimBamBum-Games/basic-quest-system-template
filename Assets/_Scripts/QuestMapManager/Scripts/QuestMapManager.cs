using System.Collections.Generic;
using UnityEngine;

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
                QuestMapButtonWrapper qmbw = Instantiate(questMapBtn, parentContent.transform);
                qmbw.priceTmp.text = qiso.price.ToString();
                qmbw.questDescription.text = qiso.description;
            }
        }

        public void InitLastMap() {
            lastMapSO = questMapManagerSO.questMapsSOs[currentMapIndex];
        }

        private void Awake() {
            InitLastMap();
            InitializeQuestMapSystem();
        }

    }
}

