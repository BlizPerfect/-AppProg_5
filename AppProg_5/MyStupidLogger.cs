using System;
using System.Diagnostics;
using System.IO;

class MyStupidLogger
{
    public string Path { get; private set; }
    private Stopwatch Watch = new Stopwatch();

    public MyStupidLogger(string path)
    {
        Path = path;
        СallToStartWork();
    }

    public void StartStopWatch()
    {
        Watch.Start();
    }
    public void StopStopWatch(string text)
    {
        Watch.Stop();
        var time = Watch.ElapsedMilliseconds;
        Watch.Reset();
        WriteDown(text + " за: " + time + " мс.");
    }

    public void СallToFinishWork()
    {
        WriteDown("Успешное окончание работы.");
    }
    public void СallToStartWork()
    {
        CallToInfo("\n");
        WriteDown("Запуск программы.");
    }

    public void CallToError(string text)
    {
        WriteDown("Обработанная ошибка в " + text);
    }
    public void CallToInfo(string text)
    {
        WriteDown(text);
    }

    private string GetDate()
    {
        return DateTime.Now + ": ";
    }

    private void WriteDown(string text)
    {
        using (StreamWriter sw = new StreamWriter(Path, true, System.Text.Encoding.Default))
        {
            sw.WriteLineAsync(GetDate() + text);
        }
    }
}
