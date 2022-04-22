using Presentation.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Presentation.ViewModel {
    public class ViewModelController : ViewModelBase {

        private readonly ModelLayerAPI api;
        private ObservableCollection<Ball> balls;
        private int BallNum = 7;

        public ViewModelController() : this(ModelLayerAPI.createAPI()) { }

        public ViewModelController(ModelLayerAPI api) {
            this.api = api;
            this.Start = new AssignFunc(() => this.Init());
            this.Stop = new AssignFunc(() => this.api.Stop());

            this.Pause = new AssignFunc(() => this.api.Pause());
            this.Resume = new AssignFunc(() => this.api.Resume());

        }

        public ICommand Start { get; set; }
        public ICommand Stop { get; set; }
        public ICommand Pause { get; set; }
        public ICommand Resume { get; set; }

        private void Init() => this.Balls = this.api.Init(this.BallNumber);

        public int BallNumber {
            get => this.BallNum;
            set {
                if (value.Equals(this.BallNum))
                    return;

                if (value < 0)
                    value = 0;

                this.BallNum = value;
                this.RaisePropertyChanged(nameof(this.BallNumber));
            }
        }

        public ObservableCollection<Ball> Balls {
            get => this.balls;
            set {
                if (value.Equals(this.balls))
                    return;

                this.balls = value;
                this.RaisePropertyChanged(nameof(this.Balls));
            }
        }

    }
}
