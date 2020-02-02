using UnityEngine;
using UnityEngine.SceneManagement;

namespace DSC.Event.Helper
{
    public class DSC_Event_LoadScene : MonoBehaviour
    {
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadSceneAsync(int sceneIndex)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        public void LoadSceneAsync(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void LoadNextScene()
        {
            int nIndex = GetActiveScene() + 1;
            if(nIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Don't have next scene to load.");
                return;
            }

            SceneManager.LoadScene(nIndex);
        }

        public void LoadPreviousScene()
        {
            int nIndex = GetActiveScene() - 1;
            if (nIndex < 0)
            {
                Debug.LogWarning("Don't have previous scene to load.");
                return;
            }

            SceneManager.LoadScene(nIndex);
        }

        #region Helper

        protected int GetActiveScene()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        #endregion
    }
}