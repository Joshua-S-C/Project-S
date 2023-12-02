using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerProfileManagerScript : MonoBehaviour
{
    private static PlayerProfileManagerScript instance;
    private static List<PlayerProfile> profiles = new List<PlayerProfile>();
    private static List<int> newPlayersAddedIndex;

    [Serializable]
    public struct PlayerDropdowns
    {
        public TMP_Dropdown primary;
        public TMP_Dropdown secondary;
        public TMP_Dropdown tactical;
    }
    public PlayerDropdowns[] playerDropdowns = new PlayerDropdowns[4];
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
            PlayerDropdowns currentDropdowns = instance.playerDropdowns[input.playerIndex];
            currentDropdowns.primary.interactable = true;
            currentDropdowns.secondary.interactable = true;
            currentDropdowns.tactical.interactable = true;
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

    public void SetPlayerPrimary(int dropdownItemIndex, int playerIndex)
    {
        Debug.Log(dropdownItemIndex);
        if (playerIndex >= profiles.Count) return;
        PlayerProfile profile = profiles[playerIndex];
        PrimaryWeapon newWeapon = (PrimaryWeapon)dropdownItemIndex;
        profile.primary = newWeapon;
        Debug.Log(profile.primary);
    }
    public void SetPlayerSecondary(int dropdownItemIndex, int playerIndex)
    {
        Debug.Log(dropdownItemIndex);
        if (playerIndex >= profiles.Count) return;
        PlayerProfile profile = profiles[playerIndex];
        SecondaryWeapon newWeapon = (SecondaryWeapon)dropdownItemIndex;
        profile.secondary = newWeapon;
    }
    public void SetPlayerTactical(int dropdownItemIndex, int playerIndex)
    {
        Debug.Log(dropdownItemIndex);
        if (playerIndex >= profiles.Count) return;
        PlayerProfile profile = profiles[playerIndex];
        TacticalWeapon newWeapon = (TacticalWeapon)dropdownItemIndex;
        profile.tactical = newWeapon;
    }
    public void SetPlayer1Primary(int dropdownItemIndex)
    {
        SetPlayerPrimary(dropdownItemIndex, 0);
    }
    public void SetPlayer2Primary(int dropdownItemIndex)
    {
        SetPlayerPrimary(dropdownItemIndex, 1);
    }
    public void SetPlayer3Primary(int dropdownItemIndex)
    {
        SetPlayerPrimary(dropdownItemIndex, 2);
    }
    public void SetPlayer4Primary(int dropdownItemIndex)
    {
        SetPlayerPrimary(dropdownItemIndex, 3);
    }
}

//contains all the information connecting a player to an input device
public class PlayerProfile
{
    private InputDevice device;
    private string playerName;
    int index;
    public PrimaryWeapon primary = PrimaryWeapon.SemiAutoRifle;
    public SecondaryWeapon secondary = SecondaryWeapon.Pistol;
    public TacticalWeapon tactical = TacticalWeapon.Grenade;

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
