using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace Data;

internal class Logger {
    private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
    private static ObservableCollection<SphereData> spheres;
    private readonly Stopwatch watch = new Stopwatch();
    public Logger(ObservableCollection<SphereData> balls) {
        Thread t = new Thread(() => {

            this.watch.Start();
            while (true) {

                if (this.watch.ElapsedMilliseconds >= 5) {
                    this.watch.Restart();
                    using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\log.txt", true)) {
                        string stamp = "INFO [" + DateTime.Now.ToString("yyyy-MM-dd: HH:mm:ss.fffffff") + "]:\n";

                        foreach (SphereData s in balls) {
                            writer.WriteLine(stamp + JsonSerializer.Serialize(s, options));
                        }
                    }
                }
            }
        }) {
            IsBackground = true
        };
        t.Start();
    }

    public void stop() {
        this.watch.Reset();
        this.watch.Stop();
    }

}
