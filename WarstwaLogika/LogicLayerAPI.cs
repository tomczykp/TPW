using Data;

namespace Logic {
    public abstract class LogicLayerAPI {

        private readonly DataLayerAPI dataAPI;

        public static LogicLayerAPI createAPI(DataLayerAPI? api = null) => new LogicAPI(api);

        public LogicLayerAPI(DataLayerAPI api) => this.dataAPI = api;

        public abstract void createPlain(int w, int h);
        public abstract void Stop();
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Make(int num);

        public abstract List<Sphere> GetSpheres();

        internal class LogicAPI : LogicLayerAPI {
            private Plain plain;
            private bool inited;

            public LogicAPI(DataLayerAPI api) : base(api) {

            }

            public override void createPlain(int w, int h) 
                => this.plain = new Plain(w, h);

            public override List<Sphere> GetSpheres() 
                => this.plain.Spheres;

            public override void Make(int num) {
                this.inited = true;
                this.plain.Active = true;
                this.plain.MakeSpheres(num);
                foreach (Sphere s in this.plain.Spheres) {
                    Thread t = new Thread(() => {
                        Random random = new Random();
                        while (this.plain.Active) {

                            if (s.X + s.VX < this.plain.Width)
                                s.VX *= -1;

                            if (s.Y + s.VY < this.plain.Height)
                                s.VY *= -1;

                            s.X += s.VX;
                            s.Y += s.VY;
                            Thread.Sleep(5);
                        }
                    });
                    t.Start();
                }
            }
            

            public override void Pause() {
                if (!this.inited) return;
                this.plain.Active = false;
            }


            public override void Resume() {
                if (!this.inited) return;
                this.plain.Active = true;
            }


            public override void Stop() {
                Console.WriteLine("Stop");
                this.plain.Active = false;
                this.inited = false;
            }


        }


    }
}
