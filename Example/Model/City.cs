namespace Example.Model
{
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ZipCode { get; set; }
        public Country Country { get; set; }
    }
}
