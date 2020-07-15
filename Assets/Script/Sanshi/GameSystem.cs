﻿
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

 
public class GameSystem : MonoBehaviour {
 
	//　スタートボタンを押したら実行する
	public void StartGame() {
		SceneManager.LoadScene ("Game");
	}

 
	//　ゲーム終了ボタンを押したら実行する
	public void EndGame() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
			Application.OpenURL("http://www.yahoo.co.jp/");
		#else
			Application.Quit();
		#endif
	}

	// Introductionボタンを押したら実行
	public void OnRetry() {
		SceneManager.LoadScene ("Prologue");
	}
}