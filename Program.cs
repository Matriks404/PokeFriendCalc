int build = 1;

Console.WriteLine($"Welcome to PokéFriendCalc! (build {build})");

Console.WriteLine("Note: The step calculations for generations VI and VII might not be correct.");
Console.WriteLine("Note: This program doesn't calculate steps for Let's Go, Pikachu! and Let's go, Eevee! games.\n");

Console.WriteLine("In following prompts if you want to proceed with a default value just press the ENTER key.\n");

int ParseInt(string prompt, int defaultValue, int min = int.MinValue, int max = int.MaxValue)
{
    int result;
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input)) return defaultValue;

        if (!int.TryParse(input, out result) || result < min || result > max)
        {
            Console.WriteLine($"\nError: Please enter a number between {min} and {max}.");
        }
        else
        {
            return result;
        }
    }
}

int defaultGeneration = 4;

int gen = ParseInt($"Enter generation (1-9, default: {defaultGeneration}): ", defaultGeneration, 1, 9);

int defaultCurrentHappiness;

if (gen == 1) {
	defaultCurrentHappiness = 90;
} else if (gen >= 2 && gen <= 7) {
	defaultCurrentHappiness = 70;
} else {
	defaultCurrentHappiness = 50;
}

int maxCurrentHappiness = (gen == 8) ? 158 : 254;

int chap = ParseInt($"Enter current happiness (default: {defaultCurrentHappiness}, max: {maxCurrentHappiness}): ", defaultCurrentHappiness, 0, maxCurrentHappiness);

while (chap == 255) // Check if current happiness is 255
{
    Console.WriteLine("\nError: Current happiness cannot be 255.");
    chap = ParseInt($"Enter current happiness (default: {defaultCurrentHappiness}, max: {maxCurrentHappiness}): ", defaultCurrentHappiness, 1, maxCurrentHappiness);
}

int defaultDesiredHappiness;

if (gen == 1) {
	defaultDesiredHappiness = 255;
} else if (gen >= 2 && gen <= 7) {
	defaultDesiredHappiness = 220;
} else if (gen == 8) {
	defaultDesiredHappiness = 159;
} else {
	defaultDesiredHappiness = 160;
}

int maxDesiredHappiness = (gen == 8) ? 159 : 255;

int dhap = ParseInt($"Enter desired happiness (default: {defaultDesiredHappiness}, max: {maxDesiredHappiness}): ", defaultDesiredHappiness, chap + 1, maxDesiredHappiness);

int n = ParseInt($"How many iterations? (default: 1000): ", 1000, 1, Int32.MaxValue);

List<int> values = new List<int>();
Random rnd = new Random();

int requiredSteps;

if (gen == 1 || gen == 5) {
	requiredSteps = 255;
} else if (gen == 2) {
	requiredSteps = 512;
} else {
	requiredSteps = 128;
}

for (int i = 0; i < n; i++)
{
    int hap = chap, steps = 0;
    while (hap < dhap)
    {
        steps += requiredSteps;
        if (rnd.Next() % 2 == 1) {
        	if ((gen == 1 && hap < 100) ||
        		(gen >= 5 && gen <= 7 && hap < 200)) {
        		hap += 2;
        	} else {
        		hap++;
        	}
        }
    }
    Console.WriteLine($"Iteration {i + 1}, steps: {steps}");
    values.Add(steps);
}

Console.WriteLine($"\nAvg: {Math.Round(values.Average())}, Min: {values.Min()}, Max: {values.Max()}");