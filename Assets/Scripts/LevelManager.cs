using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{
	public enum GameState
	{
		Stopped,
		Started,
		Win,
		Lose
	}

	public event Action<GameState> OnStateChanged;

	[SerializeField]
	private GameObject playerBase;

	[SerializeField]
	private List<LoseCondition> loseConditions;

	[SerializeField]
	private GameObject messagePanel;

	[SerializeField]
	private Text messageText;

	// [SerializeField]
	// private List<WinCondition> winConditions;

	private GameState state;

	public GameState State 
	{ 
		get { return this.state; }
		set
		{
			var oldState = this.state;
			this.state = value;

			if (this.state != value)
			{
				OnStateChanged(this.state);
			}
		}
	}


	private void Awake()
	{
		var loseConditions = GetComponentsInChildren<LoseCondition>();
		this.loseConditions = new List<LoseCondition>(loseConditions);
		foreach (var loseCondition in this.loseConditions)
		{
			loseCondition.OnConditionChanged += HandleLoseConditionChanged;
		}
	}

	private void OnDestroy()
	{
		if (this.loseConditions != null)
		{
			foreach (var loseCondition in this.loseConditions)
			{
				loseCondition.OnConditionChanged -= HandleLoseConditionChanged;
			}
		}
	}


	public void StartGame()
	{
		this.State = GameState.Started;
		EventManager.Instance.TriggerEvent(GameEvents.StartGame);
	}

	private void HandleLoseConditionChanged()
	{
		// if a lose condition has changed, check all of them to see if we lost the game
		int count = 0;
		foreach (var loseCondition in this.loseConditions)
		{
			if (loseCondition.FulfilledCondition)
			{
				++count;
			}
		}

		if (count == this.loseConditions.Count)
		{
			// we lost
			this.State = GameState.Lose;
			ShowMessage("YOU LOSE");

			EventManager.Instance.TriggerEvent(GameEvents.GameLost);
		}
	}

	private void ShowMessage(string text)
	{
		if (this.messagePanel != null && this.messageText != null)
		{
			this.messageText.text = text;
			this.messagePanel.SetActive(true);
		}
	}
}
