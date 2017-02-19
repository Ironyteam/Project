using UnityEngine;
using System.Collections;

public class HexTemplate
{
    public const int WIDTH = 15;
    public const int HEIGHT = 15;

    public Hex[,] hex = new Hex[WIDTH, HEIGHT];

    public HexTemplate() { }
}
