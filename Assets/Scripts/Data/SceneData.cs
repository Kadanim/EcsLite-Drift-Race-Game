using TMPro;
using UnityEngine;

namespace Data
{
    public class SceneData : MonoBehaviour
    {
        public Transform PlayerSpawnPoint;
    
        public TextMeshProUGUI DriftScoreText;
    
        [HideInInspector] public Transform PlayerTransform;
    }
}
