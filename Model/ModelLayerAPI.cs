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
        public abstract ObservableCollection<SphereModel> Init(int number);
        public abstract ObservableCollection<SphereModel> DeployBalls();

        internal class ModelAPI : ModelLayerAPI {

            private ObservableCollection<SphereModel> Balls { get; set; }

            public ModelAPI(LogicLayerAPI api) : base(api)
                => this.Balls = new ObservableCollection<SphereModel>();

            public override ObservableCollection<SphereModel> Init(int n) {
                this.api.Start(n);
                this.Balls.Clear();
                return this.DeployBalls();
            }

            public override void Stop() {
                this.Balls.Clear();
                this.api.Stop();
            }

            public override void Pause() => this.api.Pause();
            public override void Resume() => this.api.Resume();

            public override ObservableCollection<SphereModel> DeployBalls() {
                
                foreach (SphereLogic s in this.api.GetSpheres())
                    this.Balls.Add(new SphereModel(s));

                return this.Balls;
            }
        }
    }
}
