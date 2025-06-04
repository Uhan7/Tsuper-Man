using UnityEngine;

public class TestForCanvas : MonoBehaviour
{
    private void Update()
    {
        
    }

    public void TestToggle(bool var)
    {
        Debug.Log("Am I Epic : " + var);
    }

    public void Tog(UnityEngine.UI.Toggle toggle)
    {
        Debug.Log("Testing " + toggle.isOn);
    }

    public void TestInputField()
    {

    }
}
