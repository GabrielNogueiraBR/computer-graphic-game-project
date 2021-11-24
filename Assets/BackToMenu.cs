using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public void BackMenu(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
}
