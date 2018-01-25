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

      Location hallway_n = new Location("North Hallway");
      hallway_n.Description = "A long hallway running from West to East, opposite the South Hallway.";

      Location hallway_s = new Location("South Hallway");
      hallway_s.Description = "A long hallway running from West to East.";

      Location hallway_e = new Location("East Hallway");
      hallway_e.Description = "A long hallway running from North to South.";

      Location hallway_w = new Location("West Hallway");
      hallway_w.Description = "A long hallway running from North to South.";

      Location corner_nw = new Location("Northwest Corner");
      corner_nw.Description = "The northwest corner of the main living quarters.";

      Location corner_ne = new Location("Northeast Corner");
      corner_ne.Description = "The northeast corner of the main living quarters.";

      Location corner_sw = new Location("Southwest Corner");
      corner_sw.Description = "The southwest corner of the main living quarters.";

      Location corner_se = new Location("Southeast Corner");
      corner_se.Description = "The southeast corner of the main living quarters.";

      Location livingQuarters = new Location("Living Quarters");
      corner_se.Description = "The main atrium of the living quarters. The furniture and fixtures are in complete disarray.";

      Location elevator = new Location("Elevator");

      Location adminRoom = new Location("Administrator's Room");
      adminRoom.Description = "A small, non-descript room. A computer sits at the desk.";

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

      //hallway_n
      hallway_n.North = elevator;
      hallway_n.South = livingQuarters;
      hallway_n.East = corner_ne;
      hallway_n.West = corner_nw;

      //hallway_s
      hallway_s.North = livingQuarters;
      hallway_s.South = reconditioning;
      hallway_s.East = corner_se;
      hallway_s.West = corner_sw;

      //hallway_e
      hallway_e.North = corner_ne;
      hallway_e.South = corner_se;
      hallway_e.West = livingQuarters;

      //hallway_w
      hallway_w.North = corner_nw;
      hallway_w.South = corner_sw;
      hallway_w.East = livingQuarters;

      //corner_nw
      corner_nw.LockExists = true;
      corner_nw.South = hallway_w;
      corner_nw.East = hallway_n;
      corner_nw.West = adminRoom;
      corner_nw.WestLocked = true;
      corner_nw.WestKey = "redcard";

      //corner_ne
      corner_ne.South = hallway_e;
      corner_ne.West = hallway_n;

      //corner_sw
      corner_sw.North = hallway_w;
      corner_sw.East = hallway_s;

      //corner_se
      corner_se.North = hallway_e;
      corner_se.West = hallway_s;

      //livingQuarters
      livingQuarters.North = hallway_n;
      livingQuarters.South = hallway_s;
      livingQuarters.East = hallway_e;
      livingQuarters.West = hallway_w;

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

      Item computer = new Item("Computer");
      computer.Details = "An old, beige-box computer. It uses a CRT screen.";
      computer.UseItem = "When you press the power button, the computer drowsily awakes from its slumber. You feel someone watching you. Wheeling around, you come face to face with a hologram.";
      adminRoom.EnvItems.Add(computer);

      //Create characters
      Character hologram = new Character("Hologram");
      hologram.Dialogue = "You must go. There's no time. 1. What do you mean?/n  2. Yeah, no kidding./n~ I don't have time to explain. Take the elevator in the north hallway. You have to leave immediately./n~ Good, I don't need to tell you twice. We can talk later, but now you need to leave. It's not safe here./n *** Choose a response ***/n  1. OK./n~ EOD";
      adminRoom.Characters.Add(hologram);
      // string[] hologram_chat = hologram.LoadDialogue();
      //
      // Console.WriteLine(hologram_chat[0]);
      // Console.WriteLine("meep");
      // Console.WriteLine(hologram_chat[1]);

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

        //Use environmental items
        if (splitCommands[0].ToLower() == "use") {
          try {
            Item match = null;
            foreach(var item in currentLocation.EnvItems) {
              if (splitCommands[1].ToLower() == item.Name.ToLower()) {
                match = item;
              }
            }
            Console.WriteLine(match.UseItem);
          }
          catch (IndexOutOfRangeException) {
            Console.WriteLine("Please choose an item to use.");
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

        // Speak with characters
        if (splitCommands[0].ToLower() == "speak" && splitCommands[1].ToLower() == "with") {
          try {
            foreach(var character in currentLocation.Characters) {
              if (splitCommands[2].ToLower() == character.Name.ToLower()) {
                Console.WriteLine(character.Dialogue);
              }
            }
          }
          catch (IndexOutOfRangeException) {
            Console.WriteLine("Please choose a character to speak to.");
          }
        }

        //Endings
        if (currentLocation == cryo_a) {
          Console.WriteLine("Against your better judgment, you go into Cryosleep A. The cryogenic chemicals that have leaked from the holding cells immediately engulf your body. You freeze to death almost immediately.");
          Console.WriteLine("*** GAME OVER ***");
          playing = false;
        }

        if (currentLocation == elevator) {
          Console.WriteLine("You reach the elevator in the middle of the north hallway. As you take it up to the next floor, you hear noises in the vents.");
          Console.WriteLine("*** END OF LEVEL ***");
          playing = false;
        }

        //Exit the game
        if (splitCommands[0].ToLower() == "exit") {
          playing = false;
        }
        //Start the game loop over
      }
    }
}
