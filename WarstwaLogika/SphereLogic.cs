using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic;

public class SphereLogic {
    private double x;
    private double y;
    private double r;

    public SphereLogic(SphereData s) {
        this.X = s.X;
        this.Y = s.Y;
        this.R = s.R;
        Random random = new Random();
        s.PropertyChanged += this.update;
    }

    private void update(object sender, PropertyChangedEventArgs e) {
        SphereData s = (SphereData)sender;

        switch (e.PropertyName) {
            case nameof(this.X):
                this.X = s.X;
                break;
            case nameof(this.Y):
                this.Y = s.Y;
                break;
            case nameof(this.R):
                this.R = s.R;
                break;
        }

    }

    public double R {
        get => this.r;
        set {
            this.r = value;
            this.RaisePropertyChanged(nameof(this.R));
        }
    }

    public double X {
        get => this.x;
        set {
            this.x = value;
            this.RaisePropertyChanged(nameof(this.X));
        }
    }

    public double Y {
        get => this.y;
        set {
            this.y = value;
            this.RaisePropertyChanged(nameof(this.Y));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

