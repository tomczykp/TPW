using System.Collections.ObjectModel;

namespace Data {
    public abstract class DataLayerAPI {


        public static DataLayerAPI createAPI() => new DataAPI();
        public abstract List<SphereData> Init(int num);
        public DataLayerAPI() {
            this.Width = 640;
            this.Height = 360;
        }


        public int Width { get; }
        public int Height { get; }
        public abstract List<SphereData> Spheres { get; set; }

        public abstract void Stop();
        public abstract void Pause();
        public abstract void Resume();

        internal sealed class DataAPI : DataLayerAPI {
            private readonly object locked = new object();
            private readonly object barrier = new object();
            private Boolean Active { get; set; }
            private int QueueCnt;
            private Plain Plain { get; set; }
            private List<Thread> Threads { get; set; }

            public DataAPI() : base() {
                this.Active = false;
                this.QueueCnt = 0;
                this.Threads = new List<Thread>();
                
            }
            public override List<SphereData> Spheres {
                get => this.Plain.Spheres;
                set => this.Plain.Spheres = value;
            }

            public override List<SphereData> Init(int num) {
                this.Active = true;
                this.Plain = new Plain(this.Width, this.Height, num);

                foreach (SphereData ball in this.Spheres) {
                    Thread t = new Thread(() => {
                        while (this.Active) {

                            lock (this.locked) {
                                ball.Move();
                            }

                            if (Interlocked.CompareExchange(ref this.QueueCnt, 1, 0) == 0) {
                                Monitor.Enter(this.barrier);
                                
                                while (this.QueueCnt != this.Spheres.Count && this.Active) 
                                { }
                                
                                Interlocked.Decrement(ref this.QueueCnt);
                                Monitor.Exit(this.barrier);
                            }
                            else {
                                Interlocked.Increment(ref this.QueueCnt);
                                Monitor.Enter(this.barrier);
                                Interlocked.Decrement(ref this.QueueCnt);
                                Monitor.Exit(this.barrier);
                            }

                            Thread.Sleep(20);
                        }
                    });
                    this.Threads.Add(t);
                    t.Start();
                }

                return this.Spheres;
            }

            public override void Pause() { 
            
            }

            public override void Resume() {

            }

            public override void Stop() {
                this.Spheres.Clear();
                this.Active = false;
            }

        }
    }

}
