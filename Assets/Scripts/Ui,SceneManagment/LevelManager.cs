using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestProject
{
    public class LevelManager : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}
