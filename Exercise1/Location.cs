public class Location {
  public string Name;
  public string Description;
  public Location North;
  public Location South;
  public Location East;
  public Location West;

  //CONSTRUCTOR
  public Location(string name) {
    this.Name = name;
  }

  public string GetDescription() {
    string gameplayDescription = "";

    gameplayDescription += "*** " + this.Name + " ***\n";
    gameplayDescription += this.Description + "\n";
    if (this.North != null) {
      gameplayDescription += "To the north you see: " + this.North.Name + "\n";
    }
    if (this.South != null) {
      gameplayDescription += "To the south you see: " + this.South.Name + "\n";
    }
    if (this.East != null) {
      gameplayDescription += "To the east you see: " + this.East.Name + "\n";
    }
    if (this.West != null) {
      gameplayDescription += "To the west you see: " + this.West.Name + "\n";
    }

    return gameplayDescription;
  }
}
