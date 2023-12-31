using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Threading.Tasks;

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
    private bool[] listaAktivnihLokacija = new bool[] { false,false,false};
    private int mesto = -1;
   
    public float spawnInterval=8;
    private int spawnCounter = 3;
    private int score=0;
    public TMP_Text ScoreText;
    public TMP_Text TimeText;
    private float vreme;

    public bool timerIsRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        foreach (Transform child in spawnLocationsParent.transform)
        {
            listaSpawnLokacija.Add(child.gameObject);
        }
        //coroutine = pozivanjeRibe(vremeZaHvatanjeRibe);
        //StartCoroutine(coroutine);

        makeFish();
        //BeginFish();
    }

    public async void makeFish()
    {
        if (spawnCounter > 0 && checkIfSpawnAvailable())
        {
            spawnCounter--;
            var lokacija = listaSpawnLokacija[mesto];
            listaAktivnihLokacija[mesto] = true;
            GameObject riba = Instantiate(listaRibaZaInstanciranje[Random.Range(0, listaRibaZaInstanciranje.Count)], lokacija.transform);
            _fishes.Add(riba);
            riba.GetComponent<Fish>().WaitForFood(Random.Range(15.0f, 20.0f));
            riba.GetComponent<Fish>().addTarget(riba.transform);
            await WaitForNext(spawnInterval);
            makeFish();
        }
        else
        {
            await PauseForMoment(2.0f);
            makeFish();
        }
    }

    public bool checkIfSpawnAvailable()
    {
        bool provera = false;
        for (int i = 0; i < listaAktivnihLokacija.Length; i++)
        {
            if (listaAktivnihLokacija[i] != true)
            {
                provera = true;
                mesto = i;
            }
        }
        return provera;
    }

    public void oslobodiSpawnMesto(GameObject go)
    {
        spawnCounter++;
        Debug.Log("Radi kujo " + listaSpawnLokacija.IndexOf(go));
        listaAktivnihLokacija[listaSpawnLokacija.IndexOf(go)] = false;
    }

    public async Task WaitForNext(float spawnInterval)
    {

        var end = Time.time + spawnInterval;
        while (Time.time < end)
        {
            await Task.Yield();
        }
    }

    public async Task PauseForMoment(float spawnInterval)
    {

        var end = Time.time + spawnInterval;
        while (Time.time < end)
        {
            await Task.Yield();
        }
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
    public float timeRemaining = 120;
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
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                this.gameObject.GetComponent<SceneChanger>().Scene2();
            }
        }

        if (score < 0)
        {
            this.gameObject.GetComponent<SceneChanger>().Scene2();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void AddScore(int i)
    {
        score += i;
        ScoreText.text ="High Score: " + score.ToString();
    }

    
}
