using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

public class Team  {

	[PrimaryKey, AutoIncrement]
	public int _Id {
				get;
				set;
	}
	public string Name {
				get;
				set;
	}
	public int Score {
				get;
				set;
	}


	public override string ToString ()
	{
		return string.Format ("[Id: Id={0}, Name={1}, Score={2}]", _Id, Name, Score);
	}
}
