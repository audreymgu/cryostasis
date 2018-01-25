using System;
using System.Collections.Generic;

public class InteractiveFiction {
    static void Main(string[] args) {
      PlayGame();
    }

    public static void PlayGame() {
      //Create our game world
      Location cryo_a = new Location("Cryosleep A");
      cryo_a.Description = "A frigid room with a number of large cylinders. The placard indicates this is Cryosleep Room A.";

      Location cryo_b = new Location("Cryosleep B");
      cryo_b.Description = "A frigid room with a number of large cylinders. The placard indicates this is Cryosleep Room B. This is where you woke up.";

      Location cryo_c = new Location("Cryosleep C");
      cryo_c.Description = "A frigid room with a number of large cylinders. The placard indicates this is Cryosleep Room C.";

      Location reconditioning = new Location("Reconditioning Room");
      reconditioning.Description = "A room with what looks to be a variety of medical equipment. There are lockers to your left and your right.";

      reconditioning.Examination = "You cautiously look around the room; it appears to be connected on three sides to rooms that appear to have served a similar purpose to the room in which you woke up. While the windows to Cryosleep B and Cryosleep C allow you to peer inside, the window to Cryosleep A is completely frosted over. Checking the lockers on your right reveals an odd looking suit and a thin red card.";

      Location hallway_s = new Location("South Hallway");
      hallway_s.Description = "A long hallway running from West to East.";

      //Location linkages

      //cryo_a
      cryo_a.East = reconditioning;

      //cryo_b
      cryo_b.North = reconditioning;

      //cryo_c
      cryo_c.West = reconditioning;

      //reconditioning
      reconditioning.LockExists = true;
      reconditioning.North = hallway_s;
      reconditioning.NorthLocked = true;
      reconditioning.NorthKey = "redcard";
      reconditioning.South = cryo_b;
      reconditioning.East = cryo_c;
      reconditioning.West = cryo_a;
      reconditioning.WestLocked = true;
      reconditioning.WestKey = "redcard";

      //Set current location
      Location currentLocation;
      currentLocation = cryo_b;

      //Create the inventory
      List<Item> inventory = new List<Item>();
      Item widget = new Item("Widget");
      widget.Details = "A widget.";
      inventory.Add(widget);

      //Create room items
      Item suit = new Item("Suit");
      suit.Details = "A full-body suit. It gleams an iridescent red under the flourescent lights.";
      reconditioning.RoomContains.Add(suit);

      Item rcard = new Item("RedCard");
      rcard.Details = "A small red card. The words EMPLOYEE ACCESS are emblazoned in white lettering across one side.";
      reconditioning.RoomContains.Add(rcard);

      //Game loop
      bool playing = true;
      while(playing) {
        //Print the description of the current location
        if (currentLocation.JustArrived) {
          string description = currentLocation.GetDescription();
          Console.WriteLine(description);
          currentLocation.JustArrived = false;
        }

        //Get command
        Console.Write("> ");
        string command = Console.ReadLine();

        //Process command
        string[] splitCommands = command.Split(' ');

        //Execute command...

        //Move to a new location
        if (splitCommands[0].ToLower() == "go") {
          try {
            if (splitCommands[1].ToLower() == "north" && currentLocation.North != null && currentLocation.NorthLocked == false) {
              currentLocation.JustArrived = true;
              currentLocation = currentLocation.North;
            } else if (splitCommands[1].ToLower() == "south" && currentLocation.South != null && currentLocation.SouthLocked == false) {
              currentLocation.JustArrived = true;
              currentLocation = currentLocation.South;
            } else if (splitCommands[1].ToLower() == "east" && currentLocation.East != null && currentLocation.EastLocked == false) {
              currentLocation.JustArrived = true;
              currentLocation = currentLocation.East;
            } else if (splitCommands[1].ToLower() == "west" && currentLocation.West != null && currentLocation.WestLocked == false) {
              currentLocation.JustArrived = true;
              currentLocation = currentLocation.West;
            } else if (currentLocation.LockExists == true) {
              Console.WriteLine("Locked.");
            } else {
              Console.WriteLine("You tried to go to the " + splitCommands[1] + " and there was nothing to the " + splitCommands[1] + ".");
            }
          }
          // EXCEPTION: Location not specified
          catch (IndexOutOfRangeException) {
            Console.WriteLine("Please choose a location to navigate to.");
          }
        }

        //Unlock a path
        if (splitCommands[0].ToLower() == "unlock") {
          try {
            // Check whether player actually has item
            bool hasItem = false;
            foreach(var item in inventory) {
              if (splitCommands[2].ToLower() == item.Name.ToLower()) {
                hasItem = true;
              }
            }
            // If so, check if item is the right item
            if (hasItem) {
              string unlockStatus = currentLocation.UnlockDaemon(splitCommands[1].ToLower(), splitCommands[2].ToLower());
              Console.WriteLine(unlockStatus);
            } else {
              Console.WriteLine("You do not have " + splitCommands[2].ToLower() + ".");
            }
          }
          catch (IndexOutOfRangeException) {
            Console.WriteLine("To unlock a path, specify the direction (north, south, east, west) and the item you would like to use.");
          }
        }

        //Check inventory
        if (splitCommands[0].ToLower() == "inventory" || splitCommands[0].ToLower() == "items") {
          Console.WriteLine("--- INVENTORY ---");
          //Loop through everything in the inventory and print out its name
          foreach(Item item in inventory) {
            Console.WriteLine(item.Name);
          }
        }

        // Pick up item
        if (splitCommands[0].ToLower() == "pick" && splitCommands[1].ToLower() == "up") {
          try {
            Item match = null;
            foreach(var item in currentLocation.RoomContains) {
              if (splitCommands[2].ToLower() == item.Name.ToLower()) {
                match = item;
              }
            }
            inventory.Add(match);
            currentLocation.RoomContains.Remove(match);
            Console.WriteLine("You picked up the " + match.Name.ToLower() + ".");
          }
          catch (IndexOutOfRangeException) {
            Console.WriteLine("Please choose an item to pick up.");
          }
        }

        //Drop item
        if (splitCommands[0].ToLower() == "drop") {
          try {
            Item match = null;
            foreach(var item in inventory) {
              if (splitCommands[1].ToLower() == item.Name.ToLower()) {
                match = item;
              }
            }
            inventory.Remove(match);
            currentLocation.RoomContains.Add(match);
          }
          catch (IndexOutOfRangeException) {
            Console.WriteLine("Please choose an item to drop.");
          }
        }

        //Examine things more closely
        if (splitCommands[0].ToLower() == "examine") {
          try {
            if (splitCommands[1].ToLower() == "room") {
              Console.WriteLine(currentLocation.Examination);
            } else {
              foreach(var item in inventory) {
                if (splitCommands[1].ToLower() == item.Name.ToLower()) {
                  Console.WriteLine(item.Details);
                }
              }
              foreach(var item in currentLocation.RoomContains) {
                if (splitCommands[1].ToLower() == item.Name.ToLower()) {
                  Console.WriteLine(item.Details);
                }
              }
            }

          }
          // EXCEPTION: Item not specified
          catch (IndexOutOfRangeException) {
            Console.WriteLine("Please choose something to examine.");
          }
        }

        if (splitCommands[0].ToLower() == "exit") {
          playing = false;
        }
        //Start the game loop over
      }
    }
}
