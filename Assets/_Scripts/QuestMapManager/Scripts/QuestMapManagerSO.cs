using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JuniusGame.QuestMapManager.Scripts {

    [CreateAssetMenu(fileName = "Quest Map Manager SO", menuName = "Quest Map System/Quest Map Manager SO")]
    public class QuestMapManagerSO : ScriptableObject {
        public List<QuestMapSO> questMapsSOs = new List<QuestMapSO>();

        [ContextMenu("Show Relations")]
        public void ShowRelations() {
            foreach(QuestMapSO qms in questMapsSOs) {
                Debug.LogWarning($"<color=yellow>Map Name : {qms.name}</color>");
                List<QuestItemSO> questItems = qms.GetDisplayable();
                foreach(var item in questItems) {
                    Debug.LogWarning($"-{item.name}");
                }               
            }
        }

        public void ResetQuestMaps() {
            foreach (var maps in questMapsSOs) {
                maps.ResetQuestItemSOs();
            }
        }

        [ContextMenu("Reset Maps and Quest Items")]
        public void ResetAll() {
            foreach (var maps in questMapsSOs) {
                maps.ResetAll();
            }
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(QuestMapManagerSO), true)]
    public class QuestMapManagerEditor : Editor {

        QuestMapManagerSO context;

        private void OnEnable() {
            context = target as QuestMapManagerSO;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (GUILayout.Button("Show Relations")) {
                context.ShowRelations();
            }
        }
    }
#endif
}


