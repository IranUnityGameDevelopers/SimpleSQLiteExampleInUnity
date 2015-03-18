using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using SQLite4Unity3d;
using System.Collections.Generic;

public class DatabaseHandler {

	private ISQLiteConnection _connection;
	
	public DatabaseHandler(string DatabaseName){
		
		var factory = new Factory();
		
		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
		if (!File.Exists(filepath))
		{
			Debug.Log("Database not in Persistent path");
			// if it doesn't ->
			// open StreamingAssets directory and load the db ->
			
			#if UNITY_ANDROID 
			var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
			while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
			// then save to Application.persistentDataPath
			File.WriteAllBytes(filepath, loadDb.bytes);
			#elif UNITY_IOS
			var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			#elif UNITY_WP8
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			
			#elif UNITY_WINRT
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			#endif
			
			Debug.Log("Database written");
		}
		
		var dbPath = filepath;
		#endif
		_connection = factory.Create(dbPath);
		Debug.Log("Final PATH: " + dbPath);     
		
	}

	public void addTeam(Team _team)
	{
		_connection.BeginTransaction();
		_connection.Insert(_team);
		_connection.Commit();
	}

	public void updateTeam(Team newTeam)
	{
		_connection.Update(newTeam);
	}

	public List<Team> getTeams()
	{
		_connection.BeginTransaction();
		var Teams = _connection.Table<Team>();
		List<Team> _teams = new List<Team>();
		foreach (var item in Teams) {
			_teams.Add(item);
		}
		_connection.Commit();
		return _teams;
	}

}















