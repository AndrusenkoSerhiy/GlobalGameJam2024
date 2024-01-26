using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Rooms;
using Tags;
using UnityEngine;
using TMPro;

public class RoomInfoMonitor : MonoBehaviour {
  public TMP_Text TextLabel;

  public void SetInfo(Room room) {
    var tags = new List<Tag>();
    var tagsHate = new List<Tag>();
    foreach (var actor in room.ActorsInRoom) {
      foreach (var tag in actor.CharacterData.PreferedTags) {
        if (!tags.Contains(tag)) tags.Add(tag);
      }

      foreach (var tag in actor.CharacterData.HatedTags) {
        if (!tagsHate.Contains(tag)) tagsHate.Add(tag);
      }
    }

    StringBuilder stringBuilder = new StringBuilder("Room likes :");
    foreach (var tag in tags) {
      var stringBuilderTag = new StringBuilder("\n -");
      stringBuilderTag.Append(tag.name);
      stringBuilder.Append(stringBuilderTag);
    }

    stringBuilder.Append("\n\nRoom hates :");
    foreach (var tag in tagsHate) {
      var stringBuilderTag = new StringBuilder("\n -");
      stringBuilderTag.Append(tag.name);
      stringBuilder.Append(stringBuilderTag);
    }

    TextLabel.text = stringBuilder.ToString();
  }
}