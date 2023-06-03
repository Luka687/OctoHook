using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string[] sastojci = new string[] { "cokolada","vanila","banana","pistaci","jagoda"};
    private List<string[]> listaNarudzbina = new List<string[]>();
    private List<string>narudzbina = new List<string>();
    private int narudzbinaIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        //Testiranje
        //string[] d = new string[] { "vanila", "jagoda" };
        //listaNarudzbina.Add(d);
        //generisiRecepte();
        //generisiRecepte();
        //listaNarudzbina.Add(d);
        //foreach (string[] s in listaNarudzbina)
        //{
        //    string let = "";
        //    foreach (string element in s)
        //    {
        //        let = let + " " + element;

        //    }
        //    Debug.Log(let);
        //}
        //Debug.Log("/////");
        //dodajSloj("vanila");
        //dodajSloj("jagoda");
        //proveriNarudzbinu();
        //ukloniIzListe(narudzbinaIndex);
    }

    //Ubacivanje narudzbine u listu narudzbina
    public void generisiRecepte()
    {
        string[] recept = new string[3];
        for (int i = 0; i < Random.Range(1,4); i++)
        {
            recept[i] = sastojci[Random.Range(0, sastojci.Length)];
        }

        listaNarudzbina.Add(recept);
    }

    //igrac dodaje sloj torte
    public void dodajSloj(string sloj)
    {
        narudzbina.Add(sloj);
    }

    //provera da li je igrac napravio dobru porudzbinu
    public void proveriNarudzbinu()
    {
        bool flag=false;
        string[] provera = new string[3];
        for(int i = 0; i < narudzbina.Count; i++)
        {
            provera[i] = narudzbina[i];
        }
        foreach(string[] niz in listaNarudzbina)
        {
            
            for(int i = 0; i < niz.Length; i++)
            {
                if(flag)
                {
                    break;
                }
                if (niz[i] != provera[i])
                {
                    //Debug.Log("nisu isti"+" "+niz[i]+" "+listaNarudzbina.IndexOf(niz));
                    flag = true;
                }
            }
            //ako je tacan recept
            if (!flag)
            {
                narudzbinaIndex = listaNarudzbina.IndexOf(niz);
                break;
            }
            flag = false;
        }
        //Debug.Log(narudzbinaIndex);
    }

    //uklanjanje porudzbine iz liste
    public void ukloniIzListe(int elListe)
    {
        listaNarudzbina.Remove(listaNarudzbina[elListe]);
        narudzbinaIndex = -1;
    }

    //QTE za pozivanje ribe 10 seconds
    public void pozivanjeRibe()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
