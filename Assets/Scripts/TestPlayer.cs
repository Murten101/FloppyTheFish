using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void OnMove(InputValue value)
    {
        Vector2 parsed = value.Get<Vector2>(); 
        Debug.Log("x:"+parsed.x+", y:"+parsed.y);
    }

}
