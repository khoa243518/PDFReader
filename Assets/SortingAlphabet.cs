using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SortingAlphabet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string text = System.IO.File.ReadAllText(@"D:\OpenSource\ReaderPDF\text.txt");
		string[] s = text.Split (' ');
		Array.Sort (s);
		text = "";
		foreach (string w in s)
			text += (w+ " ");
		System.IO.File.WriteAllText (@"D:\OpenSource\ReaderPDF\text.txt", text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
