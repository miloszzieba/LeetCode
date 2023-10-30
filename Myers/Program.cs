using Myers;

var oldLines = new string[] { "A", "B", "C", "D", "E", "F", "G" };
var newLines = new string[] { "A", "A", "B", "A", "A", "B", "C", "E", "F", "A" };
var myers = new MyersOutput();
var s = myers.GetMyersOutput(oldLines, newLines);

Console.WriteLine(s);
Console.ReadLine();