using System.Collections.Generic;

public class Location {
  //Basic Information
  public string Name;
  public string Description;
  public string Examination;

  //Player State
  public bool JustArrived;

  //Locks
  public bool LockExists;
  public bool NorthLocked;
  public string NorthKey;
  public bool SouthLocked;
  public string SouthKey;
  public bool EastLocked;
  public string EastKey;
  public bool WestLocked;
  public string WestKey;

  //Items and Interactive Objects
  public List<Item> RoomContains;
  public List<Item> EnvItems;
  public List<Character> Characters;

  //Mapping
  public Location North;
  public Location South;
  public Location East;
  public Location West;

  //CONSTRUCTOR
  public Location(string name) {
    this.Name = name;
    this.JustArrived = true;
    this.RoomContains = new List<Item>();
    this.EnvItems = new List<Item>();
    this.Characters = new List<Character>();
  }

  public string GetDescription() {
    string gameplayDescription = "";

    gameplayDescription += "*** " + this.Name + " ***\n";
    gameplayDescription += this.Description + "\n";
    if (this.North != null) {
      gameplayDescription += "To the north: " + this.North.Name + "\n";
    }
    if (this.South != null) {
      gameplayDescription += "To the south: " + this.South.Name + "\n";
    }
    if (this.East != null) {
      gameplayDescription += "To the east: " + this.East.Name + "\n";
    }
    if (this.West != null) {
      gameplayDescription += "To the west: " + this.West.Name + "\n";
    }

    return gameplayDescription;
  }

  //Check if accessible to player
  public bool CheckAccess(string playerItem, string keyName) {
    if (playerItem == keyName) {
      return true;
    } else {
      return false;
    }
  }

  //Gatekeeper
  public string UnlockDaemon(string cardinalDirection, string selectedItem) {
    string gameplayUnlock = "";
    if (cardinalDirection == "north" && this.North != null && this.NorthLocked == true) {
      if (this.CheckAccess(selectedItem, this.NorthKey)) {
        this.NorthLocked = false;
        this.UpdateLocks();
        gameplayUnlock += "You unlocked the path to " + this.North.Name + " with your " + selectedItem + ".";
      }
    } else if (cardinalDirection == "south" && this.South != null && this.SouthLocked == true) {
      if (this.CheckAccess(selectedItem, this.SouthKey)) {
        this.SouthLocked = false;
        this.UpdateLocks();
        gameplayUnlock += "You unlocked the path to " + this.South.Name + " with your " + selectedItem + ".";
      }
    } else if (cardinalDirection == "east" && this.East != null && this.EastLocked == true) {
      if (this.CheckAccess(selectedItem, this.EastKey)) {
        this.EastLocked = false;
        this.UpdateLocks();
        gameplayUnlock += "You unlocked the path to " + this.East.Name + " with your " + selectedItem + ".";
      }
    } else if (cardinalDirection == "west" && this.West != null && this.WestLocked == true) {
      if (this.CheckAccess(selectedItem, this.WestKey)) {
        this.WestLocked = false;
        this.UpdateLocks();
        gameplayUnlock += "You unlocked the path to " + this.West.Name + " with your " + selectedItem + ".";
      }
    }
    return gameplayUnlock;
  }

  //Update lock status
  public bool UpdateLocks() {
    if (this.NorthLocked == true || this.SouthLocked == true || this.EastLocked == true || this.WestLocked == true) {
      this.LockExists = true;
      return this.LockExists;
    } else {
      this.LockExists = false;
      return this.LockExists;
    }
  }
}
