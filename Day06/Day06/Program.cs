var lines = File.ReadAllLines("../Day06/input.txt");

Console.WriteLine($"{lines.Length} lines in file");

var firstMarker = PacketBuffer.FindFirstMarker(lines[0], 4);

Console.WriteLine($"first marker {firstMarker}");

var firstMessage = PacketBuffer.FindFirstMarker(lines[0], 14);

Console.WriteLine($"first message {firstMessage}");
