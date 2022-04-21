using Presentation.Model;
using System.Collections;
using System.Windows.Input;

namespace Presentation.ViewModel {
    public class ViewModelController : ViewModelBase {

        private readonly int width;
        private readonly int height;
        private readonly ModelLayerAPI api;
        private IList balls;
        private int BallNum = 2;

        public ViewModelController() : this(ModelLayerAPI.createAPI()) { }

        public ViewModelController(ModelLayerAPI api) {
            this.api = api;
            this.width = api.Width;
            this.height = api.Height;
            this.Start = new AssignFunc(() => this.StartAction());
            this.Stop = new AssignFunc(() => this.api.Stop());

            this.Pause = new AssignFunc(() => this.api.Pause());
            this.Resume = new AssignFunc(() => this.api.Resume());

        }

        public ICommand Start { get; set; }
        public ICommand Stop { get; set; }
        public ICommand Pause { get; set; }
        public ICommand Resume { get; set; }

        public void StartAction() {
            this.api.Init(this.BallNumber);
            this.api.Resume();
        }

        public int Width => this.width;
        public int Height => this.height;

        public int BallNumber {
            get => this.BallNum;
            set {
                if (value.Equals(this.BallNum))
                    return;

                if (value < 0)
                    value = 0;

                this.BallNum = value;
                this.RaisePropertyChanged("BallNumber");
            }
        }

        public IList Balls {
            get => this.balls;
            set {
                if (value.Equals(this.balls))
                    return;


                this.balls = value;
                this.RaisePropertyChanged("Balls");
            }
        }

    }
}
