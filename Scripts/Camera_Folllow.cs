using UnityEngine;
public class Camera_Folllow : MonoBehaviour
{
    private float Speed = 2.5f;
    public Transform Player;
    void Update()
    {
        Vector3 pos = new Vector3(Player.position.x + 1.7f, Player.position.y + 0.4f, -10f);
        transform.position = Vector3.Slerp(transform.position, pos, Speed * Time.deltaTime);
    }
}
