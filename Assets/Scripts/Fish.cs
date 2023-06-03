using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private string[] sastojci = new string[] { "cokolada", "pomorandza", "banana", "kiwi", "jagoda" };
    private float waitingTime;
    public Slider waitSlider;
    public GameObject _goFishOrder;
    private List<string> fishOrder = new List<string>();
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
        }
        
    }



}
