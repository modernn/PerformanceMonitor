using System;
using System.IO;
using System.Reflection;

//Logger from https://stackoverflow.com/questions/20185015/how-to-write-log-file-in-c 
//Thank you BKSpurgeon
 
public class LogWriter
{
    private string m_exePath = string.Empty;
    public LogWriter(string logMessage)
    {
        LogWrite(logMessage);
    }
    public void LogWrite(string logMessage)
    {
        m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        try
        {
            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "PerformanceMonitorlog "+DateTime.Now.Month+"-"+DateTime.Now.Day+"-"+DateTime.Now.Year+".txt"))
            {
                Log(logMessage, w);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void Log(string logMessage, TextWriter txtWriter)
    {
        try
        {
            txtWriter.WriteLine(logMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}