using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presentation.Model;
public class SphereModel : INotifyPropertyChanged {

    private double x;
    private double y;
    private double r;
    private readonly string color;

    internal SphereModel(SphereLogic s) {
        this.x = s.X;
        this.y = s.Y;
        this.r = s.R;
        Random random = new Random();
        this.color = string.Format("#{0:X6}", random.Next(0x1000000));
        s.PropertyChanged += this.update;
    }

    private void update(object sender, PropertyChangedEventArgs e) {
        SphereLogic s = (SphereLogic)sender;

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
            default:
                throw new ArgumentException();
        }

    }

    public string Color => this.color;

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
