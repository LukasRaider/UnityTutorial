using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance;

    [SerializeField]
    private InputActionAsset Controls;

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else if (Instance != this) 
        { 
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
