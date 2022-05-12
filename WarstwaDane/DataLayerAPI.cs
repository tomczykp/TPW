using System.Collections.ObjectModel;

namespace Data {
    public abstract class DataLayerAPI {


        public static DataLayerAPI createAPI() => new DataAPI();
        public abstract ObservableCollection<SphereData> Init(int num);
        public DataLayerAPI() {
            this.Width = 640;
            this.Height = 360;
        }


        public int Width { get; }
        public int Height { get; }
        public abstract ObservableCollection<SphereData> Spheres { get; set; }

        public abstract void Stop();
        public abstract void Pause();
        public abstract void Resume();

        internal sealed class DataAPI : DataLayerAPI {
            private object locked = new object();
            private object barrier = new object();
            private Boolean Active { get; set; }
            private int QueueCnt;
            private Plain Plain { get; set; }
            private List<Task> Tasks { get; set; }

            public DataAPI() : base() {
                this.Active = false;
                this.QueueCnt = 0;
                this.Tasks = new List<Task>();
                
            }
            public override ObservableCollection<SphereData> Spheres {
                get => this.Plain.Spheres;
                set => this.Plain.Spheres = value;
            }

            public override ObservableCollection<SphereData> Init(int num) {
                this.Tasks.Clear();
                locked = new object();
                barrier = new object();
                this.Active = true;
                this.Plain = new Plain(this.Width, this.Height, num);

                foreach (SphereData ball in this.Spheres) {
                    this.Tasks.Add(Task.Run(() => this.run(ball)));
                    
                }

                return this.Spheres;
            }

            private async void run(SphereData ball) {
                while (this.Active) {

                    lock (this.locked) {
                        ball.Move();
                    }

                    await Task.Delay(20);
                }
            }

            public override void Pause() { 
            
            }

            public override void Resume() {

            }

            public override void Stop() {
                this.Spheres.Clear();
                this.Tasks.Clear();
                locked = new object();
                barrier = new object();
                
                
                this.Active = false;
            }

        }
    }

}
