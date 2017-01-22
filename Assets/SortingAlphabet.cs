using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SortingAlphabet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string text = System.IO.File.ReadAllText(@"text.txt");
		string[] s = text.Split (' ');
		Array.Sort (s);
		text = "";
		int endline=0;
		foreach (string w in s) {
//			endline++;
//			if (endline >= 100) {
//				text += "\n";
//				endline = 0;
//			}
			text += (w + " ");

		}
		text = text.Remove (text.Length - 1);
		System.IO.File.WriteAllText (@"text.txt", text);
	}
	public static void AddToVob(string vob)
	{
		string text = System.IO.File.ReadAllText(@"text.txt");
		text += (" " + vob);
		System.IO.File.WriteAllText (@"text.txt", text);
	}
	public static void RemoveToVob(string vob)
	{
		string text = System.IO.File.ReadAllText(@"text.txt");
		int index = text.LastIndexOf (vob);
		text = text.Remove (index-1, vob.Length+1);
		System.IO.File.WriteAllText (@"text.txt", text);
	}
}
