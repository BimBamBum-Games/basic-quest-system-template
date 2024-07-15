using UnityEngine;

namespace _Scripts.QuestMapManager.Scripts {

    [CreateAssetMenu(fileName = "Quest Item SO", menuName = "Quest Map System/Quest Item SO")]
    public class QuestItemSO : ScriptableObject {

        //Spawn edilecek gameobjesi
        public GameObject itemGO;

        //Spawn edilecek item adi
        public string questItemName;

        //Item description
        public string description;

        //Item degeri
        public int price;

        //Satin alinirlik durumu
        public bool IsPurchased = false;

        //Spawn position
        public Vector3 localPosition;

        //Quest priotiy degeri
        public int priority;

        //Suanki secilen item belirteci
        public bool IsCurrent = false;

        //Scriptible Obje adinin questItemName icine otomatik aktarilmasi.
        public void OnEnable() {
            questItemName = name;
        }

    }
}
