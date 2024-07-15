using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.QuestMapManager.Scripts {

    [CreateAssetMenu(fileName = "Quest Map SO", menuName = "Quest Map System/Quest Map SO")]
    public class QuestMapSO : ScriptableObject {
        public string MapName;
        public GameObject MapLandGO;
        public List<QuestItemSO> itemsSOs = new();

        public void OnEnable() {
            MapName = name;
        }

        private List<QuestItemSO> itemsToDisplay = new List<QuestItemSO>();

        private Dictionary<int, List<QuestItemSO>> priorityGroups = new Dictionary<int, List<QuestItemSO>>();

        void GroupItemsByPriority() {
 
            //Kullanmadan once diziyi temizle.
            priorityGroups.Clear();

            foreach (var item in itemsSOs) {

                //Item priorityler dictionaryde yoksa ekle ve deger olarak listesini olustur.
                if (priorityGroups.ContainsKey(item.priority) == false) {
                    priorityGroups[item.priority] = new List<QuestItemSO>();
                }

                //Key olusturuldugunda veya var ise de olusan keydeki listeye itemi ekler.
                priorityGroups[item.priority].Add(item);
            }         
        }

        void FindQuestItemsToDisplay() {

            itemsToDisplay.Clear();

            bool dont_look_at_further = false;

            //Her bir list grubunu gezer.
            foreach (var ls in priorityGroups) {

                //Her bir list grubundaki o gruba ait itemleri gezer.
                foreach (QuestItemSO item in ls.Value) {
                    if (item.IsPurchased == false) {
                        itemsToDisplay.Add(item);
                        dont_look_at_further = true;
                    }
                }

                if(dont_look_at_further == true) {
                    break;
                }

            }

        }

        public List<QuestItemSO> GetDisplayable() {

            GroupItemsByPriority();
            FindQuestItemsToDisplay();
            return itemsToDisplay;
        }
    }
}

