using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFonctions : MonoBehaviour 
{
	public GameObject Player;

		public GameObject loadingImage;

		public void LoadScene(int level)
		{
			loadingImage.SetActive(true);
			SceneManager.LoadScene(level);
		}

		public void LoadSameScene()
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}

		public void PauseGame()
		{
			Time.timeScale =0;
		}

}
