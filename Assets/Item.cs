using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	bool _add = false;
	string text ="";
	public void InitItem(string _text,int time)
	{
		text = _text;
		transform.GetChild (1).GetComponent<Text> ().text = text;// +  ( text.Length>=4 ?char.ConvertToUtf32(text,3).ToString()  :".").ToString();
		transform.GetChild (2).GetComponent<Text> ().text = time.ToString ();
		transform.GetChild (0).GetComponent<Image> ().color = new Color (0.9f, 1f, 0.9f, 1f);
	}
	public void AddToVob()
	{
		
		if (_add == false) {
			transform.GetChild (3).GetChild (0).GetComponent<Text> ().text = "Remove";
			transform.GetChild (0).GetComponent<Image> ().color = new Color (1f, 0.9f, 0.9f, 1f);
			SortingAlphabet.AddToVob (text);
		} else {
			transform.GetChild (3).GetChild (0).GetComponent<Text> ().text = "Add";
			transform.GetChild (0).GetComponent<Image> ().color = new Color (0.9f, 1f, 0.9f, 1f);
			SortingAlphabet.RemoveToVob (text);
		}
		//PDFReader1.instance.Load ();
		_add = !_add;
	}
}
