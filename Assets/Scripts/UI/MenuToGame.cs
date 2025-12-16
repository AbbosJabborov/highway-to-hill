using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuToGame : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(2);
        }
    }
}