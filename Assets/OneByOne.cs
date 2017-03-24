using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OneByOne : MonoBehaviour {
	public string nameFile="text";
	// Use this for initialization
	void Start () {
		string text = "";
		int endline=0;
		foreach (string w in System.IO.File.ReadAllLines(nameFile+".txt")) {
			endline++;
			if (endline % 2 ==1) {
				text += "\n";
				text += (w + " ");
			}

		}
		text = text.Remove (text.Length - 1);
		System.IO.File.WriteAllText (nameFile+".txt", text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
