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
	Dictionary<string,int> dictionary= new Dictionary<string,int>();
	// Use this for initialization
	void Start () {
		int deltatime = System.DateTime.Now.Second;
		string path= @"D:\OpenSource\ReaderPDF\pdf.pdf";
		//string path= @"D:\games-client\pdf.pdf";
		string s= PdfTextExtractor1.pdfText (path);
		s = s.ToLower ();
		string[] words = s.Split (' ','\n','\t','=','+','{','}','-','(','*',')',';','\'','.',',','@','\"','?','[',']','_',':');

		//another list
		string text = System.IO.File.ReadAllText(@"D:\OpenSource\ReaderPDF\text.txt");
		string[] myWords = text.Split (' ');

		int count = 0, vob = 0, expert = 0;
		foreach (string w in words) {
			if (!CheckInvalid (w))
				continue;
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
		foreach (KeyValuePair<string,int> item in dictionary.OrderByDescending(key=> key.Value).Take(1000))
		{ 
			percent += dictionary [item.Key];
			if (!myWords.Contains (item.Key)) {
				Debug.Log (item.Key + "\t" + dictionary [item.Key] + "\t" + ((float)percent / count).ToString ("p1"));
				take100--;
				if (take100 <= 0)
					break;
			}
		}
		deltatime = System.DateTime.Now.Second - deltatime;
		Debug.Log ("Delta time: " + deltatime);
	}

	bool CheckInvalid(string w)
	{
		if (w.Length <= 2 || w.Length >= 12)
			return false;
		if (w.Any (char.IsDigit))
			return false;
		return true;
	}
}
