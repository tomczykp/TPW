namespace Data {
    public abstract class DataLayerAPI {

        public static DataLayerAPI createAPI() => new DataAPI();

        internal sealed class DataAPI : DataLayerAPI {
            public DataAPI() { }
        }
    }

}
