using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presentation.Model;
public class Ball : INotifyPropertyChanged {

    private int x;
    private int y;
    private int r;

    internal Ball(Sphere s) {
        this.x = s.X;
        this.y = s.Y;
        this.r = s.R;
        s.PropertyChanged += this.update;
    }

    private void update(object sender, PropertyChangedEventArgs e) {
        Sphere s = (Sphere)sender;

        switch (e.PropertyName) {
            case nameof(X):
                this.X = s.X - s.R;
                break;
            case nameof(Y):
                this.Y = s.Y - s.R;
                break;
            case nameof(R):
                this.R = s.R;
                break;
            default:
                throw new ArgumentException();
        }

    }

    public int R {
        get => this.r;
        set {
            this.r = value;
            this.RaisePropertyChanged(nameof(R));
        }
    }

    public int X {
        get => this.x;
        set {
            this.x = value;
            this.RaisePropertyChanged(nameof(X));
        }
    }

    public int Y {
        get => this.y;
        set {
            this.y = value;
            this.RaisePropertyChanged(nameof(Y));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
