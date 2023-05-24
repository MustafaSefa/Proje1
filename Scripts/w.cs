using UnityEngine;
public class w : MonoBehaviour
{
    void Update()
    {
        if (CheckPoint.c == 3)
        {
            Destroy(gameObject);
        }
    }
}
