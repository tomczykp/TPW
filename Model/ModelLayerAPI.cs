using Logic;
using System.Collections.ObjectModel;

namespace Presentation.Model {
    public abstract class ModelLayerAPI {
        private readonly LogicLayerAPI api;
        public static ModelLayerAPI createAPI() => new ModelAPI(LogicLayerAPI.createAPI());

        public ModelLayerAPI(LogicLayerAPI api) => this.api = api;

        public abstract void Stop();
        public abstract void Pause();
        public abstract void Resume();
        public abstract ObservableCollection<Ball> Init(int number);
        public abstract ObservableCollection<Ball> DeployBalls(int number);

        internal class ModelAPI : ModelLayerAPI {

            private ObservableCollection<Ball> Balls { get; set; }

            public ModelAPI(LogicLayerAPI api) : base(api) 
                => this.Balls = new ObservableCollection<Ball>();

            public override ObservableCollection<Ball> Init(int n) {
                this.api.createPlain(640, 360);
                return this.DeployBalls(n);
            }

            public override void Stop() {
                this.Balls.Clear();
                this.api.Stop();
            }

            public override void Pause() => this.api.Pause();
            public override void Resume() => this.api.Resume();

            public override ObservableCollection<Ball> DeployBalls(int number) {
                this.api.Make(number);
                this.Balls.Clear();

                foreach (Sphere s in this.api.GetSpheres())
                    this.Balls.Add(new Ball(s));


                return this.Balls;
            }
        }
    }
}
