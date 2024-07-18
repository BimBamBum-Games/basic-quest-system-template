using System.Collections;
using UnityEngine;

namespace JuniusGame.QuestMapManager.Scripts {

    [CreateAssetMenu(fileName = "Quest Item SO", menuName = "Quest Map System/Quest Item SO")]
    public class QuestItemSO : ScriptableObject {

        //Spawn edilecek gameobjesi
        public ItemAnimationBase itemGO;

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

        //Eger bu item icin guncelleme yapilirken zaten bir button varsa tekrar button olusturmamasi icin kontrolu yapilir.
        public bool hasQuestMapButtonWrapper;

        //Scriptible Obje adinin questItemName icine otomatik aktarilmasi.
        public void OnEnable() {
            questItemName = name;
            Debug.Log($"Resetted By Itself! > {name}.");
        }

        //En ustten cagrilacak tetiklenecek. Daha onceden OnEnable ve OnDisable metodlarinda cagrilabiliyordu ancak suanda cagrilamiyor.
        public void ResetFields() {
            hasQuestMapButtonWrapper = false;
        }

        public void ResetItemFields() {
            IsPurchased = false;
            hasQuestMapButtonWrapper = false;
        }
        
        //Hazir ornegini olustur ve dondur.
        public ItemAnimationBase GetFactoryQuestItemSO() {

            if (itemGO != null) {
                ItemAnimationBase iab = Instantiate(itemGO);
                return iab;
            }
            
            else return null;
        }
    }
}
