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
        public abstract void Init(int number);
        public abstract ObservableCollection<Ball> DeployBalls(int number);

        public int Width { get; }
        public int Height { get; }

        internal class ModelAPI : ModelLayerAPI {

            private ObservableCollection<Ball> Balls { get; set; }

            public ModelAPI(LogicLayerAPI api) : base(api) {
                this.Balls = new ObservableCollection<Ball>();
            }
            public override void Init(int n) {
                this.api.createPlain(700, 400);
                this.DeployBalls(n);
            }

            public override void Stop() => this.api.Stop();
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
