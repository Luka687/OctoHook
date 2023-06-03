using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Fish[] _fishes;

    private string[] sastojci = new string[] { "cokolada","pomorandza","banana","kiwi","jagoda"};
    private List<string[]> listaNarudzbina = new List<string[]>();
    private List<string>narudzbina = new List<string>();
    private int narudzbinaIndex = -1;


    private bool pozivanjeRibeUp;
    private float sliderPozivanjeRibeTime;
    public Slider pozivanjeRibeSlider;
    public float vremeZaHvatanjeRibe = 10.0f;
    private IEnumerator coroutine;

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
        coroutine = pozivanjeRibe(vremeZaHvatanjeRibe);
        StartCoroutine(coroutine);

        BeginFish();
    }

    public async void BeginFish()
    {
        
        for(int i=0;i<_fishes.Length;i++)
        {
            
            _fishes[i].WaitForFood(Random.Range(20.0f, 35.0f));
            _fishes[i].setFishOrder(generisiReceptZaRibu());
        }
    }

    public string[] generisiReceptZaRibu()
    {
        string[] recept = new string[3];
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            recept[i] = sastojci[Random.Range(0, sastojci.Length)];
        }
        return recept;
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

    //QTE za pozivanje ribe 10 seconds ili odredjeno vreme
    public IEnumerator pozivanjeRibe(float odredjenoVremeZaHvatanjeRibe)
    {
        sliderPozivanjeRibeTime = 0;
        pozivanjeRibeUp = true;
        yield return new WaitForSeconds(odredjenoVremeZaHvatanjeRibe);
        Debug.Log("kraj");
        pozivanjeRibeUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pozivanjeRibeUp)
        {
            sliderPozivanjeRibeTime += Time.deltaTime;
            pozivanjeRibeSlider.value = sliderPozivanjeRibeTime / vremeZaHvatanjeRibe;
        }
        if(pozivanjeRibeUp && Input.GetButtonDown("Q"))
        {
            Debug.Log("Hooked");
            StopCoroutine(coroutine);
            pozivanjeRibeUp = false;
        }
    }
}
