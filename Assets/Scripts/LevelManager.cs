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
	private GameObject messagePanel;

	[SerializeField]
	private Text messageText;


	private GameState state;
		
	private List<GameCondition> loseConditions = new List<GameCondition>();

	private List<GameCondition> winConditions = new List<GameCondition>();

	public GameState State 
	{ 
		get { return this.state; }
		set
		{
			var oldState = this.state;
			this.state = value;

			if (oldState != this.state)
			{
				if (OnStateChanged != null)
				{
					OnStateChanged(this.state);
				}
			}
		}
	}


	private void Awake()
	{
		var gameConditions = GetComponentsInChildren<GameCondition>();
		for (int i = 0; i < gameConditions.Length; ++i)
		{
			switch (gameConditions[i].Type)
			{
				case GameCondition.GameConditionType.Win:
				gameConditions[i].OnConditionChanged += HandleGameConditionChanged;
				this.winConditions.Add(gameConditions[i]);
				break;

				case GameCondition.GameConditionType.Lose:
				gameConditions[i].OnConditionChanged += HandleGameConditionChanged;
				this.loseConditions.Add(gameConditions[i]);
				break;
			}
		}
	}

	private void OnDestroy()
	{
		if (this.loseConditions != null)
		{
			foreach (var loseCondition in this.loseConditions)
			{
				loseCondition.OnConditionChanged -= HandleGameConditionChanged;
			}
		}
	}


	public void StartGame()
	{
		this.State = GameState.Started;
		EventManager.Instance.TriggerEvent(GameEvents.StartGame);
	}

	private void HandleGameConditionChanged(GameCondition.GameConditionType gameConditonType)
	{
		// if a game condition has changed, check all of them to see if we fulfilled them
		if (gameConditonType == GameCondition.GameConditionType.Lose)
		{
			CheckLoseConditions();
		}
		else if (gameConditonType == GameCondition.GameConditionType.Win)
		{
			// check lose conditions first in case we managed to satisfy all lose conditions as well
			// in this case prioritise lost
			bool lost = CheckLoseConditions();
			if (!lost)
			{
				CheckWinConditions();
			}
		}
	}

	/// <summary>
	/// Check lose conditions
	/// </summary>
	/// <returns>true if all lose conditions were fulfilled</returns>
	private bool CheckLoseConditions()
	{
		int count = 0;
		foreach (var loseCondition in this.loseConditions)
		{
			if (loseCondition.IsFulfilled)
			{
				++count;
			}
		}

		if (count == this.loseConditions.Count)
		{
			this.State = GameState.Lose;
			ShowMessage("YOU LOSE");

			EventManager.Instance.TriggerEvent(GameEvents.GameLost);
			return true;
		}

		return false;
	}

	/// <summary>
	/// Check win conditions
	/// </summary>
	/// <returns>true if all win conditions were fulfilled</returns>
	private bool CheckWinConditions()
	{
		int count = 0;
		foreach (var winCondition in this.winConditions)
		{
			if (winCondition.IsFulfilled)
			{
				++count;
			}
		}

		if (count == this.winConditions.Count)
		{
			this.State = GameState.Lose;
			ShowMessage("YOU WIN");

			EventManager.Instance.TriggerEvent(GameEvents.GameWon);
			return true;
		}

		return false;
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
