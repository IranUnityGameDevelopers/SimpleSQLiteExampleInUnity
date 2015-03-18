using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public DatabaseHandler dataHandler;

	// Use this for initialization
	void Start () {
		dataHandler = new DatabaseHandler("TestDB");

		Debug.Log("---------------------Teams---------------------");
		var teams = dataHandler.getTeams();
		foreach (var item in teams) {
			Debug.Log(item.ToString());
		}
		Debug.Log("---------------------AddTeam---------------------");
		var team = new Team();
		team.Name = "new Team";
		team.Score = 0;
		dataHandler.addTeam(team);
		Debug.Log("---------------------UpdateScoreOfTeam---------------------");
		teams[1].Score = 200;
		dataHandler.updateTeam(teams[1]);
		Debug.Log("---------------------Teams---------------------");
		teams = dataHandler.getTeams();
		foreach (var item in teams) {
			Debug.Log(item.ToString());
		}


	}

}
