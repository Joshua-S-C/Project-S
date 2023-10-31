using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerProfileManagerScript : MonoBehaviour
{
    private static List<PlayerProfile> profiles = new List<PlayerProfile>();
    // Start is called before the first frame update
    void Start()
    {
        if (profiles.Count > 0)
        {
            InitializePlayers();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void AddProfile(InputDevice newDevice)
    {
        profiles.Add(new PlayerProfile(newDevice));

    }
    private void InitializePlayers()
    {
        for(int i = 0;i < profiles.Count;i++)
        {
            Debug.Log("hit");
            GetComponent<PlayerInputManager>().JoinPlayer(i, i, "", profiles[i].GetInputDevice());
        }
    }
}


public class PlayerProfile
{
    private InputDevice device;

    public PlayerProfile(InputDevice newDevice)
    {
        device = newDevice;
    }
    public InputDevice GetInputDevice()
    {
        return device;
    }
}
