using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneTransition : MonoBehaviour {

	public void GoToScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
