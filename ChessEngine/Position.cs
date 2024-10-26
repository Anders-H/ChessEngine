using System;
using System.Text.RegularExpressions;

namespace ChessEngine;

public class Position
{
    public string Name { get; }
    public int PhysicalX { get; }
    public int PhysicalY { get; }
        
    public Position(string? name)
    {
        Name = (name ?? "").ToLower();

        if (!Regex.IsMatch(Name, "^[a-h][1-8]$"))
            throw new ArgumentException();

        PhysicalX = Name[0] switch
        {
            'a' => 0,
            'b' => 1,
            'c' => 2,
            'd' => 3,
            'e' => 4,
            'f' => 5,
            'g' => 6,
            'h' => 7,
            _ => throw new Exception()
        };

        PhysicalY = Name[1] switch
        {
            '8' => 0,
            '7' => 1,
            '6' => 2,
            '5' => 3,
            '4' => 4,
            '3' => 5,
            '2' => 6,
            '1' => 7,
            _ => throw new Exception()
        };
    }

    public Position(int physicalX, int physicalY)
    {
        if (physicalX < 0 || physicalX > 7)
            throw new ArgumentOutOfRangeException();

        if (physicalY < 0 || physicalY > 7)
            throw new ArgumentOutOfRangeException();
        
        PhysicalX = physicalX;
        PhysicalY = physicalY;
        Name = $"{NameFromX(physicalX)}{NameFromY(physicalY)}";
    }

    private static string NameFromX(int x) =>
        x switch
        {
            0 => "a",
            1 => "b",
            2 => "c",
            3 => "d",
            4 => "e",
            5 => "f",
            6 => "g",
            7 => "h",
            _ => throw new Exception()
        };
        
    private static string NameFromY(int y) =>
        y switch
        {
            0 => "8",
            1 => "7",
            2 => "6",
            3 => "5",
            4 => "4",
            5 => "3",
            6 => "2",
            7 => "1",
            _ => throw new Exception()
        };

    public override string ToString() =>
        Name;

    public static implicit operator string(Position? p) =>
        p?.ToString() ?? "";
        
    public static implicit operator Position(string s) =>
        new(s);
}