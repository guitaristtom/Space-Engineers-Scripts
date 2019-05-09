// Thomas's Miner Manager Script
// =============================
// Version 0.1
// Date: 2019-05-08
//
// This script will manage the extending of pistons, stopping the drills, rotor,
// and pistons if the cargo containers are too full or if the batteries are too
// empty.

// =======================================================================================
//                                                                            --- Configuration ---
// =======================================================================================

// --- Essential Configuration ---
// =======================================================================================

// The group name for the cargo containers used for the input for the drills
string inputCargoGroupName = "Outpost - Storage";

// Keyword for LCDs on the same grid to output data to
string outputLcdKeyword = "!MinerManagerOutput";

// --- Debug Configuration ---
// =======================================================================================
// The names for the standard and debug output LCD screens
string debugLcdKeyword = "!MinerManagerDebug";

// =======================================================================================
//                                                                      --- End of Configuration ---
//                                                        Don't change anything beyond this point!
// =======================================================================================
// This line and below here can be Minified with https://codebeautify.org/csharpviewer

List<IMyCargoContainer> inputCargoBlocks = new List<IMyCargoContainer>();
List<IMyBatteryBlock> batteryBlocks = new List<IMyBatteryBlock>();
List<IMyTextPanel> debugLcds = new List<IMyTextPanel>();
List<IMyTextPanel> outputLcds = new List<IMyTextPanel>();

public Program()
{
    // Grab the group, and check that it exists
    IMyBlockGroup inputCargoGroup = GridTerminalSystem.GetBlockGroupWithName(inputCargoGroupName);
    if (inputCargoGroup == null)
    {
        Echo("Cargo group not found.\r\nPlease change the 'inputCargoGroupName' variable");
        return;
    }

    // Set up all of the input cargo containers (WIP echo them all out right now)
    // inputCargoBlocks = new List<IMyCargoContainer>();
    inputCargoGroup.GetBlocksOfType<IMyCargoContainer>(inputCargoBlocks);

    // Set up a list for all batteries, and check if any batteries are available
    // on the same immediate grid
    // batteryBlocks = new List<IMyBatteryBlock>();
    GridTerminalSystem.GetBlocksOfType<IMyBatteryBlock>(batteryBlocks, block => block.IsSameConstructAs(Me));
    if (batteryBlocks == null) // WIP need to check if list is empty
    {
        Echo("No batteries found.\r\nPlease add some batteries and recompile.");
        return;
    }

    // Set up the LCDs
    // debugLcds = new List<IMyTextPanel>;
    GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(debugLcds, block => block.CustomName.Contains(debugLcdKeyword));
    // outputLcds = new List<IMyTextPanel>;
    GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(outputLcds, block => block.CustomName.Contains(outputLcdKeyword));

    return;
}

public void Save()
{
}

public void DisplayDebug(IMyTextPanel lcd)
{
    Echo("DisplayDebug ran!");

    // Turn the LCD on
    lcd.Enabled = true;

    // Set the LCD to `text and image` mode
    lcd.ContentType = ContentType.TEXT_AND_IMAGE;

    // First line doesn't have true, so you erase everything
    lcd.WriteText("");

    // Everything else does, so it appends
    lcd.WriteText("This is the debug LCD", true);
    lcd.WriteText("\n\rDebug things~!", true);

    return;
}

public void DisplayOutput(IMyTextPanel lcd)
{
    Echo("DisplayOutput ran!");

    // Turn the LCD on
    lcd.Enabled = true;

    // Set the LCD to `text and image` mode
    lcd.ContentType = ContentType.TEXT_AND_IMAGE;

    // First line doesn't have true, so you erase everything
    lcd.WriteText("This is the standard LCD");

    // Everything else does, so it appends
    lcd.WriteText("\n\rNormal LCD things!#!#", true);

    return;
}

public void Main(string arg)
{
    Echo("Main ran!");
    // List off the names of all the batteries on the immediate grid
    Echo($"Input Cargo Blocks:");
    foreach (var cargoBlock in inputCargoBlocks)
    {
        Echo($"- {cargoBlock.CustomName}");
    }

    Echo("\r\n");

    // List off the names of all the batteries on the immediate grid
    Echo($"Battery Blocks:");
    foreach (var batteryBlock in batteryBlocks)
    {
        Echo($"- {batteryBlock.CustomName}");
    }

    Echo("\r\n");
    // Test display output
    Echo("debugLcd:");
    foreach (var debugLcd in debugLcds)
    {
        Echo($"- {debugLcd.CustomName}");
        DisplayDebug(debugLcd);
    }

    Echo("\r\n");
    Echo("outputLcd:");
    foreach (var outputLcd in outputLcds)
    {
        Echo($"- {outputLcd.CustomName}");
        DisplayOutput(outputLcd);
    }

}
