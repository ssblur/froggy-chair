using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using TinyLife.Objects;
using TinyLife.World;

namespace FroggySetMod;

// These classes exist for froggy sound effects and such
public class LilyPadTable : Furniture {

    public LilyPadTable(Guid id, FurnitureType type, int[] colors, Map map, Vector2 pos) : base(id, type, colors, map, pos) {}

}

public class FroggyChair : Furniture {

    public FroggyChair(Guid id, FurnitureType type, int[] colors, Map map, Vector2 pos) : base(id, type, colors, map, pos) {}

}