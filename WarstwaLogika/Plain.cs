namespace Logic {
    internal class Plain {

        private readonly int _w;
        private readonly int _h;
        private readonly List<Sphere> _sphere;

        public Plain(int width, int height, int numSpheres, int rad) {

            if (width <= 0 || height <= 0 || width < 2 * rad || height < 2 * rad) {
                throw new ArgumentOutOfRangeException();
            }

            this._w = width;
            this._h = height;
            this._sphere = new List<Sphere>();
            this.MakeSpheres(numSpheres, rad);
        }

        public Plain(int width, int height) {
            if (width <= 0 || height <= 0) {
                throw new ArgumentOutOfRangeException();
            }

            this._w = width;
            this._h = height;
            this._sphere = new List<Sphere>();
        }

        public int Width => this._w;
        public int Height => this._h;
        public bool Active { get; set; }
        public List<Sphere> Spheres => this._sphere;

        public void MakeSpheres(int numSpheres, int rad) {

            if (this.Width < 2 * rad || this.Height < 2 * rad) {
                throw new ArgumentOutOfRangeException();
            }

            Random r = new Random();
            Func<int, int> cord = (x) => r.Next(rad, x - rad);
            Func<int, int> vel = (x) => r.Next(x) / 10;

            for (int i = 0; i < numSpheres; i++) {
                this.Spheres.Add(new Sphere(cord(this._w), cord(this._h), rad, vel(this._w), vel(this._w)));
            }
        }

        public void MakeSpheres(int numSpheres) {
            Random rand = new Random();
            int min = Math.Min(this._h, this._w);

            Func<int> rad = () => rand.Next(0, (min) / 5);
            Func<int, int, int> cord = (x, r) => rand.Next(r, x - r);
            Func<int, int> vel = (x) => rand.Next(x) / 8;

            for (int i = 0; i < numSpheres; i++) {
                int R = rad();
                this.Spheres.Add(
                    new Sphere(
                        cord(this._w, R), cord(this._h, R),
                        R, vel(this._w), vel(this._h)
                    )
                );
            }
        }

    }
}
