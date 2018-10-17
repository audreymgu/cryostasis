using System;
using System.Collections.Generic;

public class Character {
  public string Name;
  public string Dialogue;

  public Character(string name) {
    this.Name = name;
    this.Dialogue = "";
  }

  public string[] LoadDialogue() {
    string[] dialogueList = Dialogue.Split(new [] { "/n~" }, StringSplitOptions.None);
    return dialogueList;
  }
}
