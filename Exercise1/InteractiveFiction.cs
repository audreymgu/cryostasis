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

      //Location linkages

      //cryo_a
      cryo_a.East = reconditioning;

      //cryo_b
      cryo_b.North = reconditioning;

      //cryo_c
      cryo_c.West = reconditioning;

      //reconditioning
      reconditioning.West = cryo_a;
      reconditioning.East = cryo_c;

      //Set current location
      Location currentLocation;
      currentLocation = cryo_b;

      //Create the inventory
      List<Item> inventory = new List<Item>();
      Item widget = new Item("Widget");
      widget.Details = "A widget.";
      inventory.Add(widget);

      //Game loop
      bool playing = true;
      while(playing) {
        //Print the description of the current location
        string description = currentLocation.GetDescription();
        Console.WriteLine(description);

        //Get command
        Console.Write("> ");
        string command = Console.ReadLine();

        //Process command
        string[] splitCommands = command.Split(' ');

        //Execute command...

        //Move to a new location
        if (splitCommands[0].ToLower() == "go") {
          try {
            if (splitCommands[1].ToLower() == "north" && currentLocation.North != null) {
              currentLocation = currentLocation.North;
            } else if (splitCommands[1].ToLower() == "south" && currentLocation.South != null) {
              currentLocation = currentLocation.South;
            } else if (splitCommands[1].ToLower() == "east" && currentLocation.East != null) {
              currentLocation = currentLocation.East;
            } else if (splitCommands[1].ToLower() == "west" && currentLocation.West != null) {
              currentLocation = currentLocation.West;
            } else {
              Console.WriteLine("You tried to go to the " + splitCommands[1] + " and there was nothing to the " + splitCommands[1]);
            }
          }
          // EXCEPTION: Location not specified
          catch (IndexOutOfRangeException) {
            Console.WriteLine("Please choose a location to navigate to.");
          }
        }

        //Check inventory
        if (splitCommands[0].ToLower() == "inventory") {
          Console.WriteLine("--- INVENTORY ---");
          //Loop through everything in the inventory and print out its name
          foreach(Item item in inventory) {
            Console.WriteLine("\t" + item.Name);
          }
        }

        //Examine item
        if (splitCommands[0].ToLower() == "examine") {
          try {
            foreach(var item in inventory) {
              if (splitCommands[1].ToLower() == item.Name.ToLower()) {
                Console.WriteLine(item.Details);
              }
            }
          }
          // EXCEPTION: Item not specified
          catch (IndexOutOfRangeException) {
            Console.WriteLine("Please choose an item to examine.");
          }
        }

        if (splitCommands[0].ToLower() == "exit") {
          playing = false;
        }
        //Start the game loop over
      }
    }
}
