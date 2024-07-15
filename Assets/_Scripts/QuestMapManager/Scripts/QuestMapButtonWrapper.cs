using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.QuestMapManager.Scripts {
    public class QuestMapButtonWrapper : MonoBehaviour {
        [field: SerializeField] public Button button;
        [field: SerializeField] public TextMeshProUGUI questDescription;
        [field: SerializeField] public TextMeshProUGUI priceTmp;
        [field: SerializeField] public QuestMapButtonWrapper questMapBtn;
    }
}


