using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	public enum GameState
	{
		Stopped,
		Started,
		Win,
		Lose
	}

	[SerializeField]
	private GameObject playerBase;

	public GameState State { get; private set; }


	public void StartGame()
	{
		this.State = GameState.Started;
		EventManager.Instance.TriggerEvent(GameEvents.StartGame);
	}
}
