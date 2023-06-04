using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private string[] sastojci = new string[] { "cokolada", "borovnica", "banana", "kiwi", "jagoda" };
    private List<string> sastojciList = new List<string> { "cokolada", "borovnica", "banana", "kiwi", "jagoda" };
    public List<Sprite> cakeSprites = new List<Sprite>();
    private float waitingTime;
    public Slider waitSlider;
    public GameObject _goFishOrder;
    public List<string> fishOrder = new List<string>();
    private List<GameObject> fishOrderImages = new List<GameObject>();

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

}
