using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	private Player[] players;
	private int activePlayerIndex = -1;
	private bool started = false;
	
	// Update is called once per frame
	void Update () {
		if(started && players != null && activePlayerIndex >= 0) {
			Player activePlayer = players[activePlayerIndex];
			if(activePlayer.NoMovesLeft()) {
				activePlayer.EndTurn();
				activePlayerIndex = (activePlayerIndex + 1) % players.Length;
				Debug.Log("api: " + activePlayerIndex);
				players[activePlayerIndex].StartTurn();
			}
		}
	}

	public void SetPlayers(Player[] players) {
		this.players = players;
	}

	public void Begin() {
		started = true;
		bool setPlayer = false;
		for(int i=0; i<players.Length; i++) {
			if(players[i].isHuman) {
				activePlayerIndex = i;
				setPlayer = true;
			}
		}
		if(!setPlayer) activePlayerIndex = 0;
	}

	public void Pause() {
		started = false;
	}

	public void Resume() {
		started = true;
	}

	public void Finish() {
		started = false;
		activePlayerIndex = -1;
	}

	public string GetActivePlayerName() {
		if(players != null && activePlayerIndex >=0 && activePlayerIndex < players.Length) {
			return players[activePlayerIndex].GetDisplayName();
		}
		return "Error: No active player";
	}
}