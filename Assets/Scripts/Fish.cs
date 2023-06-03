using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private float waitingTime;
    public Slider waitSlider;
    public string[] fishOrder= new string[3];

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

    public void setFishOrder(string[] order)
    {
        this.fishOrder = order;
    }

}
