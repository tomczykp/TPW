using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Logic;
public abstract class LogicLayerAPI {

    private readonly DataLayerAPI api;

    public static LogicLayerAPI createAPI() => new LogicAPI(DataLayerAPI.createAPI());

    public LogicLayerAPI(DataLayerAPI api) {
        this.Spheres = new ObservableCollection<SphereLogic>();
        this.api = api;
    }

    public abstract void Stop();
    public abstract void Pause();
    public abstract void Resume();
    public abstract void Start(int num);

    public abstract ObservableCollection<SphereLogic> GetSpheres();
    public ObservableCollection<SphereLogic> Spheres { get; set; }

    internal class LogicAPI : LogicLayerAPI {
        public LogicAPI(DataLayerAPI api) : base(api) { }

        public override ObservableCollection<SphereLogic> GetSpheres()
            => this.Spheres;

        public override void Start(int num) {
            this.Spheres.Clear();
            foreach (SphereData s in this.api.Init(num)) {
                
                this.Spheres.Add(new SphereLogic(s));
                s.PropertyChanged += this.update;
            }
        }

        private void EdgeCollision(SphereData s) {
            if (s.X + s.VX + s.R > this.api.Width || s.X + s.VX - s.R  < 0)
                s.VX *= -1;

            if (s.Y + s.VY + s.R > this.api.Height || s.Y + s.VY - s.R  < 0)
                s.VY *= -1;
        }

        private void SphereCollision(SphereData S) {
            foreach (SphereData s in this.api.Spheres) {
                if (s.Equals(S))
                    continue;

                double dx = s.X - S.X;
                double dy = s.Y - S.Y;
                if (Math.Sqrt((dx * dx) + (dy * dy)) < (S.R + s.R)) {
                    double V1x = S.VX;
                    double V1y = S.VY;
                    double v2x = s.VX;
                    double v2y = s.VY;

                    S.VX = ((S.Weight - s.Weight) * V1x + (2 * s.Weight * v2x)) / (S.Weight + s.Weight);
                    s.VX = ((s.Weight - S.Weight) * v2x + (2 * S.Weight * V1x)) / (S.Weight + s.Weight);

                    S.VY = ((S.Weight - s.Weight) * V1y + (2 * s.Weight * v2y)) / (S.Weight + s.Weight);
                    s.VY = ((s.Weight - S.Weight) * v2y + (2 * S.Weight * V1y)) / (S.Weight + s.Weight);
                }
            }
        }


        public override void Pause() => this.api.Pause();
        public override void Resume() => this.api.Resume();
        public override void Stop() {
            this.Spheres.Clear();
            this.api.Stop();
        }
          

        private void update(object sender, PropertyChangedEventArgs e) {
            SphereData ball = (SphereData)sender;
            if (e.PropertyName == "Position") {
                this.EdgeCollision(ball);
                this.SphereCollision(ball);
            }

        }
    }


}

