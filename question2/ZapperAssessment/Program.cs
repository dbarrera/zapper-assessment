namespace ZapperAssessment;

internal class Program
{
    const string SettingsFilePath = "settings.bin";

    static void Main(string[] args)
    {
        Console.WriteLine(IsFeatureEnabled("00000000", 7));
        Console.WriteLine(IsFeatureEnabled("00000010", 7));
        Console.WriteLine(IsFeatureEnabled("11111111", 4));

        Console.WriteLine("Persist Settings:");

        SaveSettings(SettingsFilePath, "00000000");
        string settings = LoadSettings(SettingsFilePath);
        Console.WriteLine(IsFeatureEnabled(settings, 7));

        SaveSettings(SettingsFilePath, "00000010");
        settings = LoadSettings(SettingsFilePath);
        Console.WriteLine(IsFeatureEnabled(settings, 7));

        SaveSettings(SettingsFilePath, "11111111");
        settings = LoadSettings(SettingsFilePath);
        Console.WriteLine(IsFeatureEnabled(settings, 4));
    }

    static bool IsFeatureEnabled(string settings, int featureIndex)
    {
        if (settings.Length != 8)
            throw new ArgumentException($"Invalid settings {settings}.");
        if (featureIndex < 1 || featureIndex > 8)
            throw new ArgumentException($"Invalid feature index {featureIndex}.");

        int index = featureIndex - 1;

        return settings[index] == '1';
    }

    static string SaveSettings(string filePath, string settings)
    {
        File.WriteAllBytes(filePath, [StringToByte(settings)]);

        return settings;
    }

    static string LoadSettings(string filePath)
    {
        if (File.Exists(filePath) == false)
            throw new FileNotFoundException("File not found.");

        return ByteToString(File.ReadAllBytes(filePath)[0]);
    }

    static string ByteToString(byte settings)
        => Convert.ToString(settings, 2).PadLeft(8, '0');

    static byte StringToByte(string settings)
    {
        if (settings.Length != 8 || IsBinaryString(settings) == false)
            throw new ArgumentException("Invalid binary string.");

        return Convert.ToByte(settings, 2);

        bool IsBinaryString(string str)
        {
            foreach (char c in str)
            {
                if (c != '0' && c != '1') return false;
            }

            return settings.Length > 0;
        }
    }
}

