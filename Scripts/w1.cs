using UnityEngine;
public class w1 : MonoBehaviour
{
    void Update()
    {
        if (CheckPoint.c == 5)
        {
            Destroy(gameObject);
        }
    }
}
