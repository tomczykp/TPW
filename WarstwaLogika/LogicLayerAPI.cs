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

        public abstract List<Sphere> GetSpheres();

        internal class LogicAPI : LogicLayerAPI {
            private Plain plain;


            public LogicAPI(DataLayerAPI api) : base(api) {

            }

            public override void createPlain(int w, int h) => this.plain = new Plain(w, h);

            public override List<Sphere> GetSpheres() => this.plain.Spheres;


            public override void Pause() {

            }


            public override void Resume() {

            }


            public override void Stop() {

            }


        }


    }
}
