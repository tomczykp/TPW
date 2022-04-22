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
                        while (true) {

                            if (this.plain.Active) {
                                if (s.X + s.VX + s.R > this.plain.Width || s.X + s.VX < 0)
                                    s.VX *= -1;

                                if (s.Y + s.VY + s.R > this.plain.Height || s.Y + s.VY < 0)
                                    s.VY *= -1;

                                s.X += s.VX;
                                s.Y += s.VY;
                            }
                            Thread.Sleep(20);
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
                this.plain.Active = false;
                this.inited = false;
                this.plain.Spheres.Clear();
            }


        }


    }
}
