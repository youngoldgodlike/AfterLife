using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components
{
    public class LoadSceneComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        
        public void Load() => SceneManager.LoadScene(_tag);
    }
}
