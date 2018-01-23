using System;
using System.Collections.Generic;

public class InteractiveFiction {
    static void Main(string[] args) {
      PlayGame();
    }

    public static void PlayGame() {
      //Create our game world
      Location field = new Location("The Field");
      field.Description = "You are standing in a field. You like it here.";

      Location tower = new Location("The TOWER");
      tower.Description = "The tower looms over you.";

      //Link all of our locations together
      field.North = tower;
      tower.South = field;

      Location currentLocation;
      currentLocation = field;

      //Create the inventory
      List<Item> inventory = new List<Item>();
      Item torch = new Item("Torch");
      Item sword = new Item("Sword");
      inventory.Add(torch);
      inventory.Add(sword);

      //Game loop
      bool playing = true;
      while(playing) {
        //Print the description of the current location
        string description = currentLocation.GetDescription();
        Console.WriteLine(description);

        //Get a command
        Console.Write("> ");
        string command = Console.ReadLine();

        //Process the command
        string[] splitCommands = command.Split(' ');

        //Execute the command
        if (splitCommands[0].ToLower() == "go") {
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

        if (splitCommands[0].ToLower() == "inventory") {
          Console.WriteLine("--- INVENTORY ---");
          //Loop through everything in the inventory and print out its name
          foreach(Item item in inventory) {
            Console.WriteLine("\t" + item.Name);
          }
        }

        if (splitCommands[0].ToLower() == "exit") {
          playing = false;
        }
        //Start the game loop over
      }
    }
}
