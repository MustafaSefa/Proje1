using TMPro;
using UnityEngine;
public class Diamond : MonoBehaviour
{
    public TextMeshProUGUI d;
    private int _d;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            _d++;
            d.text = _d.ToString();
        }
    }
}
