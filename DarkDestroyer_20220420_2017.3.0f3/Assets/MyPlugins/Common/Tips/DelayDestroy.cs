using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    public void Delay(float time)
    {
        Destroy(gameObject, time);
    }
}                                   