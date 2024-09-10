using UnityEngine;

public class DelayRemoveComponent : MonoBehaviour
{
    float sec;

    public void Init(float sec)
    {
        this.sec = sec;
    }



    void Start()
    {   Destroy(this, sec);
    }

}