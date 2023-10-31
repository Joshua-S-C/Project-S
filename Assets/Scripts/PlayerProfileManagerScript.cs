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

    //adds a new device profile if that inputdevice is unique
    public static void AddProfile(InputDevice newDevice)
    {
        if(!ContainsDevice(newDevice))
        {
            profiles.Add(new PlayerProfile(newDevice));
        }
        

    }

    //checks if the profiles array already has a profile with this input device
    private static bool ContainsDevice(InputDevice newDevice)
    {
        for(int i = 0;i < profiles.Count;i++)
        {
            if (profiles[i].GetInputDevice() == newDevice)
            {
                return true;
            }
        }


        return false;
    }
    private void InitializePlayers()
    {
        for(int i = 0;i < profiles.Count;i++)
        {
            GetComponent<PlayerInputManager>().JoinPlayer(i, i, "", profiles[i].GetInputDevice());
        }
    }
}

//contains all the information connecting a player to an input device
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
