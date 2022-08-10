namespace SampleWebAPI.DTO
{
    public class SwordWithTypeDTO
    {
        public string Name { get; set; }
        public string Weight { get; set; }
        public int SamuraiId { get; set; }
        public TypeCreateDTO Types { get; set; }
    }
}
