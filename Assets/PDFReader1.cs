using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using UnityEngine.UI;


public enum TypeFile
{
	pdf,
	txt,
	srt
}
public static class PdfTextExtractor1
{
	public static string pdfText(string path)
	{
		PdfReader reader = new PdfReader(path);
		string text = string.Empty;
		for(int page = 1; page <=  reader.NumberOfPages; page++)
		{
			text += PdfTextExtractor.GetTextFromPage(reader,page);
		}
		reader.Close();
		return text;
	}   
}

public class PDFReader1 : MonoBehaviour {
	public GameObject list;
	public GameObject mainUI;
	public GameObject item;
	public string path= @"head_first_design_patterns";
	Dictionary<string,int> dictionary= new Dictionary<string,int>();
	public TypeFile type;
	// Use this for initialization

	string s;
	string[] words;
	void Start()
	{
		DebugTime ();
		instance = this;
		//trouble speed
		if (type == TypeFile.pdf) {
			s = PdfTextExtractor1.pdfText (path+".pdf");
		} 
		//
		else if(type ==TypeFile.txt) {
			s = System.IO.File.ReadAllText(path+".txt");
		} else {
			s = System.IO.File.ReadAllText(path+".srt");
		}
		DebugTime ();
		s = s.Replace ("’", "'");
		s = s.Replace ("‘", "'");
		s = s.Replace ("�", "'"); 
		s = s.ToLower ();
		words = s.Split (' ','\n','\t','=','+','#','{','}','-','(','*',')',';','.',',','@','\"','?','[',']','/','_',':','!','“','”','<','>',(char)13);
		s = "";
//		foreach (string w in words) {
//			s += (w + " ");
//		}
		DebugTime ();



		Load ();
		DebugTime ();
	}
	public static PDFReader1 instance;
	public void Load () {
		Debug.Log (System.DateTime.Now.Second);
		for(int i = 0 ; i < list.transform.childCount;i++)
			Destroy (list.transform.GetChild (i).gameObject);
		list.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
		//string path= @"head_first_design_patterns.pdf";
		//string path= @"D:\games-client\pdf.pdf";
		string text = System.IO.File.ReadAllText(@"text.txt");
		string[] myWords = text.Split (' ');
		int count = 0, vob = 0, expert = 0;
		DebugTime ();

		foreach (string key in dictionary.Keys.ToList())
			dictionary.Remove (key);
		DebugTime ();

		foreach (string w in words) {
			string wt = w;
			if (wt.StartsWith ("'") || wt.StartsWith ("’"))
				wt = wt.Remove (0);
			if (wt.EndsWith ("'") || wt.EndsWith ("’"))
				wt = wt.Remove (wt.Length-1);
			if (!CheckInvalid (wt))
				continue;
			//			if (w.EndsWith ("s"))
			//				w.Remove (w.Length - 1);
			//frist trouble speed
			if ( myWords. Contains (wt))
				expert++;
			//

			//second trouble speed
			if (dictionary.ContainsKey (wt))
				dictionary [wt] += 1;
			else {
				dictionary.Add (wt, 1);
				vob++;
			}
			//
			count++;

		}

		DebugTime ();

		mainUI.transform.GetChild(0).GetComponent<Text>().text = "Words: " + count.ToString();
		mainUI.transform.GetChild(1).GetComponent<Text> ().text = "Words: " + vob.ToString();
		mainUI.transform.GetChild(2).GetComponent<Text>().text = ("Expert:" + ((float)expert/count).ToString("p1"));
		mainUI.transform.GetChild(3).GetComponent<Text>().text = ("Diffical:" + (((count*0.92f)-expert)/Mathf.Sqrt(count)).ToString("n2"));
		mainUI.transform.GetChild(4).GetComponent<Text>().text = ("Vob:" + myWords.Length);
		int percent = 0;
		int take100 = 100;
		string copytext = "";
		foreach (KeyValuePair<string,int> _item in dictionary.OrderByDescending(key=> key.Value).Take(1000))
		{ 
			percent += dictionary [_item.Key];
			if (!myWords.Contains (_item.Key)) {
				GameObject newItem = GameObject.Instantiate (item) as GameObject;
				newItem.GetComponent<Item> ().InitItem (_item.Key, dictionary [_item.Key]);
				newItem.transform.SetParent (list.transform, false);
				take100--;
				copytext += (_item.Key + " ");
				if (take100 <= 0)
					break;
			}
		}
		DebugTime ();

	}

	bool CheckInvalid(string w)
	{
		if (w.Length <= 2 || w.Length >= 20)
			return false;
		if (w.Any (char.IsDigit))
			return false;
		return true;
	}

	void DebugTime()
	{
		Debug.Log ("Current time : " + System.DateTime.Now.TimeOfDay);
	}
}
