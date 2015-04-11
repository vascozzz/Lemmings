using UnityEngine;

public class MainMenuController : MonoBehaviour 
{
	public GameObject instrs;

	public void Play() {
		Application.LoadLevel("Level1");
	}

	public void ShowInstrs() {
		instrs.SetActive(true);
	}

	public void Exit() {
		Application.Quit();
	}
}
