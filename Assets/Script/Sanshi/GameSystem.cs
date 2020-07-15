
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class GameSystem : MonoBehaviour {
 
	//　スタートボタンを押したら実行する
	public void GameStart() {
		SceneManager.LoadScene ("Game");
	}

 
	//　ゲーム終了ボタンを押したら実行する
	public void GameEnd() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
			Application.OpenURL("http://www.yahoo.co.jp/");
		#else
			Application.Quit();
		#endif
	}
	
	// Introductionボタンを押したら実行する
	public void OnRetry() {
		SceneManager.LoadScene("Prologue");
	}
}