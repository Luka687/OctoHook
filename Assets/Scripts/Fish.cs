using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private float waitingTime;
    public Slider waitSlider;

    public async Task WaitForFood(float duration)
    {
        var start = Time.time+0.01f;
        var end = Time.time + duration;
        while (Time.time < end)
        {
            waitSlider.value = (Time.time-start)/duration;
            await Task.Yield();
        }
    }

    
}
