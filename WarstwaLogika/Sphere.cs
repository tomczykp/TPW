using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic {
    public class Sphere : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        private int _x;
        private int _y;
        private int _r;
        private readonly int weight;

        public Sphere(int x, int y, int radius, int vx, int vy) {
            this._x = x;
            this._y = y;
            this._r = radius;
        }

        public int VX { get; set; }
        public int VY { get; set; }

        public int X {
            get => this._x;
            set {
                this._x = value;
                this.RaisePropertyChanged(nameof(X));
            }
        }

        public int Y {
            get => this._y;
            set {
                this._y = value;
                this.RaisePropertyChanged(nameof(Y));
            }
        }

        public int R {
            get => this._r;
            set {
                if (value > 0) {
                    this._r = value;
                    this.RaisePropertyChanged(nameof(R));
                }
                else {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int Weight { get; }

        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

