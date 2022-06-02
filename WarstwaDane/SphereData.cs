using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data;

public class SphereData {

    public event PropertyChangedEventHandler PropertyChanged;
    private double _x;
    private double _y;
    private double _r;

    public SphereData(double x, double y, double radius, double vx, double vy, double weight) {
        this._x = x;
        this._y = y;
        this._r = radius;
        this.VX = vx;
        this.VY = vy;
        this.Weight = weight;
    }

    public void Move(object locked) {
        this.X += this.VX;
        this.Y += this.VY;
        lock (locked) {
            this.RaisePropertyChanged("Position");
        }
    }

    public double VX { get; set; }
    public double VY { get; set; }

    public double X {
        get => this._x;
        set {
            this._x = value;
            this.RaisePropertyChanged(nameof(this.X));
        }
    }

    public double Y {
        get => this._y;
        set {
            this._y = value;
            this.RaisePropertyChanged(nameof(this.Y));
        }
    }

    public double R {
        get => this._r;
        set {
            if (value > 0) {
                this._r = value;
                this.RaisePropertyChanged(nameof(this.R));
            }
            else {
                throw new ArgumentOutOfRangeException();
            }
        }
    }


    public double Weight { get; }

    protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    public override bool Equals(object? obj) => obj is SphereData data && this.X == data.X && this.Y == data.Y && this.R == data.R && this.Weight == data.Weight;
}

