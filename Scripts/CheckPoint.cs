using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    public static int c;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            c++;
            Destroy(gameObject);
        }
    }
}
