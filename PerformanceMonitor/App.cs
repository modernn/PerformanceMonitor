using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Configuration;

public class App
{
    public static LogWriter myLogWriter = new LogWriter("Beginning the Logging of Computer Performance");
    public static PerformanceCounter cpuCounter;
    public static PerformanceCounter ramCounter;
    public static int TotalRAM = 16281;

    public static void Main()
    {
        cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        bool ShouldRun = true;
        int cycleTime = 1000;
        myLogWriter.LogWrite("Logging performance every "+cycleTime+"ms");
        while(ShouldRun)
        {
            var myLogEntry = GetLogEntry();
            Console.WriteLine(myLogEntry);
            myLogWriter.LogWrite(myLogEntry);
            Thread.Sleep(1000);
        }
    }
    public static string GetLogEntry()
    {
        return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + " RAM Used: " + GetRamUsage() + " CPU Used: " + GetCpuUsage();
    }
    public static string GetCpuUsage()
    {
        return Math.Round(cpuCounter.NextValue(),2) + "%";
    }
    public static string GetRamUsage()
    {
        float CurrentlyAvailiableRAM = ramCounter.NextValue();
        float CurrentlyUsedRAM = TotalRAM - CurrentlyAvailiableRAM;
        var CurrentUsedRamPercent = Math.Round(Math.Round(CurrentlyUsedRAM/TotalRAM, 3)*100,2);
        return  "("+CurrentlyUsedRAM+"MB/"+TotalRAM+"MB) "+CurrentUsedRamPercent+"%";
    }
}