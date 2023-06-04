using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<GameObject> _fishes = new List<GameObject>();

    //private string[] sastojci = new string[] { "cokolada","pomorandza","banana","kiwi","jagoda"};
    //private List<string[]> listaNarudzbina = new List<string[]>();
    //private List<string>narudzbina = new List<string>();
    //private int narudzbinaIndex = -1;


    private bool pozivanjeRibeUp;
    private float sliderPozivanjeRibeTime;
    public Slider pozivanjeRibeSlider;
    public float vremeZaHvatanjeRibe = 10.0f;
    private IEnumerator coroutine;

    public List<GameObject> listaRibaZaInstanciranje = new List<GameObject>();
    public GameObject spawnLocationsParent;
    private List<GameObject> listaSpawnLokacija = new List<GameObject>();
    public float spawnInterval=10;
    private int spawnCounter = 3;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in spawnLocationsParent.transform)
        {
            listaSpawnLokacija.Add(child.gameObject);
        }
        //coroutine = pozivanjeRibe(vremeZaHvatanjeRibe);
        //StartCoroutine(coroutine);

        makeFish();
        //BeginFish();
    }

    public void makeFish()
    {
        if (spawnCounter > 0)
        {
            spawnCounter--;
            var lokacija = listaSpawnLokacija[Random.Range(0, listaSpawnLokacija.Count)];
            GameObject riba = Instantiate(listaRibaZaInstanciranje[Random.Range(0, listaRibaZaInstanciranje.Count)], lokacija.transform);
            _fishes.Add(riba);
            riba.GetComponent<Fish>().WaitForFood(Random.Range(20.0f, 35.0f));
            riba.GetComponent<Fish>().addTarget(riba.transform);

        }
    }

    public async void BeginFish()
    {
        
        //for(int i=0;i<_fishes.Length;i++)
        //{
            
        //   // _fishes[i].WaitForFood
        //}
    }

    //Ubacivanje narudzbine u listu narudzbina
    //public void generisiRecepte()
    //{
    //    string[] recept = new string[3];
    //    for (int i = 0; i < Random.Range(1,4); i++)
    //    {
    //        recept[i] = sastojci[Random.Range(0, sastojci.Length)];
    //    }

    //    listaNarudzbina.Add(recept);
    //}

    //igrac dodaje sloj torte
    //public void dodajSloj(string sloj)
    //{
    //    narudzbina.Add(sloj);
    //}

    //provera da li je igrac napravio dobru porudzbinu
    //public void proveriNarudzbinu()
    //{
    //    bool flag=false;
    //    string[] provera = new string[3];
    //    for(int i = 0; i < narudzbina.Count; i++)
    //    {
    //        provera[i] = narudzbina[i];
    //    }
    //    foreach(string[] niz in listaNarudzbina)
    //    {
            
    //        for(int i = 0; i < niz.Length; i++)
    //        {
    //            if(flag)
    //            {
    //                break;
    //            }
    //            if (niz[i] != provera[i])
    //            {
    //                //Debug.Log("nisu isti"+" "+niz[i]+" "+listaNarudzbina.IndexOf(niz));
    //                flag = true;
    //            }
    //        }
    //        //ako je tacan recept
    //        if (!flag)
    //        {
    //            narudzbinaIndex = listaNarudzbina.IndexOf(niz);
    //            break;
    //        }
    //        flag = false;
    //    }
    //    //Debug.Log(narudzbinaIndex);
    //}

    ////uklanjanje porudzbine iz liste
    //public void ukloniIzListe(int elListe)
    //{
    //    listaNarudzbina.Remove(listaNarudzbina[elListe]);
    //    narudzbinaIndex = -1;
    //}

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
