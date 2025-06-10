using UnityEngine;

public class DropLocation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public int ID;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Jeepney")
        {
            anim.Play("fade_in");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Jeepney")
        {
            anim.Play("fade_out");
        }
    }
}
