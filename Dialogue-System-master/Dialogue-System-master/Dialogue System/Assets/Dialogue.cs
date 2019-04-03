using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pass this object into the Dialogue Manager whenever we want to start a new dialogue
/// </summary>
[System.Serializable]
public class Dialogue {

    [Tooltip ("Name of Speaker")]
	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

}
