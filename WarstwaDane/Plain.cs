using System.Collections.ObjectModel;

namespace Data {
    internal class Plain {


        public Plain(int width, int height, int num) {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException();


            this.Width = width;
            this.Height = height;
            this.Spheres = new ObservableCollection<SphereData>();
            Random rand = new Random();
            int min = Math.Min(this.Width, this.Height);

            Func<double> rad = () => 2 + (rand.NextDouble() * (min) / 10);
            Func<double, double, int> cord = (x, r) => rand.Next((int)r, (int)(x - r));
            Func<double> vel = () => 1 + rand.NextDouble() * 3;
            Func<double> mass = () => 1 + rand.NextDouble() * 9;

            for (int i = 0; i < num; i++) {
                double R;
                double x;
                double y;
                bool clear;
                do {
                    R = rad();
                    x = cord(this.Width, R);
                    y = cord(this.Height, R);
                    clear = true;
                    foreach (SphereData s in this.Spheres) {
                        double dx = (x - s.X);
                        double dy = (y - s.Y);
                        if (s.R + R >= Math.Sqrt(dx * dx + dy * dy)) {
                            clear = false;
                            break;
                        }
                    }
                } while (!clear);

                this.Spheres.Add(new SphereData(x, y, R, vel(), vel(), mass()));
            }
        }

        public int Width { get; }
        public int Height { get; }
        public ObservableCollection<SphereData> Spheres { get; set; }



    }
}
