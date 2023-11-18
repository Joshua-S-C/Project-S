using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerProfileManagerScript : MonoBehaviour
{
    private static List<PlayerProfile> profiles = new List<PlayerProfile>();
    private static List<int> newPlayersAddedIndex;
    // Start is called before the first frame update
    void Start()
    {
        newPlayersAddedIndex = new List<int>();
        if (profiles.Count > 0)
        {
            InitializePlayers();
        }
    }

    // Update is called once per frame
    void Update()
    {
        AddedPlayerMessages();
    }
    private void AddedPlayerMessages()
    {
        for(int i = 0;i < newPlayersAddedIndex.Count;i++)
        {
            GetComponent<PlayerManagerScript>().NewPlayerJoinedGame(newPlayersAddedIndex[i]);
        }
        newPlayersAddedIndex = new List<int>();
    }

    //adds a new device profile if that inputdevice is unique
    public static void AddProfile(PlayerInput input)
    {
        if (input.devices.Count > 0 && !ContainsDevice(input.devices[0]))
        {
            newPlayersAddedIndex.Add(input.playerIndex);
            string name = "Player " + (input.playerIndex + 1);
            profiles.Add(new PlayerProfile(input.devices[0],name));
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
        for (int i = 0;i < profiles.Count;i++)
        {
            PlayerInput input = GetComponent<PlayerInputManager>().JoinPlayer(i, 0, "", profiles[i].GetInputDevice());
            GetComponent<PlayerManagerScript>().PlayerJoinedGame(input.gameObject);
            
            
        }
    }
    public static PlayerProfile GetProfile(int index)
    {
        return profiles[index];
    }
    public static int GetProfileCount()
    {
        return profiles.Count;
    }
}

//contains all the information connecting a player to an input device
public class PlayerProfile
{
    private InputDevice device;
    private string playerName;
    int index;

    public PlayerProfile(InputDevice newDevice,string name)
    {
        device = newDevice;
        playerName = name;
        
    }
    public InputDevice GetInputDevice()
    {
        return device;
    }
    public string GetName()
    {
        return playerName;
    }
}
