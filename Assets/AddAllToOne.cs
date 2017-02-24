using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class AddAllToOne : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		string[] nameFiles = Directory.GetFiles ("Sub");
		string allText="";
		foreach(string nameFile in nameFiles)
		{
			string text = System.IO.File.ReadAllText(nameFile);
			allText += text;
		}
		if (!File.Exists ("Sub.txt"))
			File.Create ("Sub.txt");
		File.WriteAllText ("Sub.txt", allText);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
