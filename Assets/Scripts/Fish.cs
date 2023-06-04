using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private string[] sastojci = new string[] { "Cokolada", "Borovnica", "Banana", "Kiwi", "Jagoda" };
    private List<string> sastojciList = new List<string> { "Cokolada", "Borovnica", "Banana", "Kiwi", "Jagoda" };
    public List<Sprite> cakeSprites = new List<Sprite>();
    private float waitingTime;
    public Slider waitSlider;
    public GameObject _goFishOrder;
    public List<string> fishOrder = new List<string>();
    private List<GameObject> fishOrderImages = new List<GameObject>();
    private GameManager _gm;

    public async Task WaitForFood(float duration)
    {
        var start = Time.time;
        var end = Time.time + duration;
        while (Time.time < end)
        {
            this.waitSlider.value = (Time.time-start)/duration;
            await Task.Yield();
        }
        //I onda ode
        oslobodiMesto();
        _gm.AddScore(-50);
        Destroy(this.gameObject);
    }
    public void oslobodiMesto()
    {
        _gm.oslobodiSpawnMesto(this.transform.parent.gameObject);

        Destroy(this.gameObject);
    }

    private void Awake()
    {
        foreach(Transform child in _goFishOrder.transform)
        {
            fishOrderImages.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        generisiReceptZaRibu();
        showOrder();
        _gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    public void generisiReceptZaRibu()
    {
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            fishOrder.Add(sastojci[Random.Range(0, sastojci.Length)]);
        }
    }


    private void showOrder()
    {
        for(int i = 0; i < fishOrder.Count; i++)
        {
            fishOrderImages[i].SetActive(true);
            fishOrderImages[i].GetComponent<Image>().sprite =cakeSprites[sastojciList.IndexOf(fishOrder[i])];
            
        }
        
    }

    
    private Vector2 target;
    public void addTarget(Transform vec)
    {
        target = new Vector2(vec.position.x - 55.0f, vec.position.y);
    }
    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime);
    }

    public void compareOrder(List<string> order)
    {
        bool provera = true;
        for(int i = 0; i < fishOrder.Count; i++)
        {
            Debug.Log("Octopus order:" + order[i] + " fish order:" + fishOrder[i]);
            if (order[i] != fishOrder[i])
            {
                provera = false;
                break;
            }
        }
        if (provera)
        {
            //FISH SATISFIED dodajBodove()
            _gm.AddScore(+100);
            oslobodiMesto();
        }
        else{
            _gm.AddScore(-25);
            Debug.Log("you suck");
        }
    }

}
