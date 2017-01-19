using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

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

public class PDFReader : MonoBehaviour {
	public string path= @"head_first_design_patterns";
	Dictionary<string,int> dictionary= new Dictionary<string,int>();
	// Use this for initialization
	void Start () {
		DebugTime ();
		//string path= @"head_first_design_patterns.pdf";
		//string path= @"D:\games-client\pdf.pdf";
		path+=".pdf";
		string s= PdfTextExtractor1.pdfText (path);
		s = s.ToLower ();
		string[] words = s.Split (' ','\n','\t','=','+','{','}','-','(','*',')',';','.',',','@','\"','?','[',']','_',':');
		//another list
		string text = System.IO.File.ReadAllText(@"text.txt");
		string[] myWords = text.Split (' ');
		Debug.Log ("Vob: " + myWords.Length);
		int count = 0, vob = 0, expert = 0;
		foreach (string w in words) {
			if (!CheckInvalid (w))
				continue;
//			if (w.EndsWith ("s"))
//				w.Remove (w.Length - 1);

			if (myWords.Contains (w))
				expert++;
			if (dictionary.ContainsKey (w))
				dictionary [w] += 1;
			else {
				dictionary.Add (w, 1);
				vob++;
			}
			count++;
		}
		Debug.Log ("Words: " + count);
		Debug.Log ("Words: " + vob);
		Debug.Log ("Expert:" + ((float)expert/count).ToString("p1"));
		int percent = 0;
		int take100 = 100;
		string copytext = "";
		foreach (KeyValuePair<string,int> item in dictionary.OrderByDescending(key=> key.Value).Take(1000))
		{ 
			percent += dictionary [item.Key];
			if (!myWords.Contains (item.Key)) {
				Debug.Log (item.Key + "\t" + dictionary [item.Key]);
				take100--;
				copytext += (item.Key + " ");
				if (take100 <= 0)
					break;
			}
		}
		Debug.Log (copytext);
	}

	bool CheckInvalid(string w)
	{
		if (w.Length <= 2 || w.Length >= 12)
			return false;
		if (w.Any (char.IsDigit))
			return false;
		return true;
	}

	void DebugTime()
	{
		Debug.Log ("Current time : " + System.DateTime.Now.Second);
	}
}
