using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace AmNuamRunner
{
    public class MainMenuSceneChange : MonoBehaviour
    {
        [SerializeField] private float _loadDelay = 0.3f;

        public void SceneChange(int sceneIndex)
        {
            Time.timeScale = 1;
            GetComponent<Button>().interactable = false;

            DOVirtual.DelayedCall(_loadDelay, () =>
            {
                SceneManager.LoadScene(sceneIndex);
            });
        }
    }
}