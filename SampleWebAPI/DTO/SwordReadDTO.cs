namespace SampleWebAPI.DTO
{
    public class SwordReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }   
        public int SamuraiId { get; set; }
        public List<ElementReadDTO> Elements { get; set; } = new List<ElementReadDTO>();
        public TypeReadDTO Type { get; set; }
    }
}
