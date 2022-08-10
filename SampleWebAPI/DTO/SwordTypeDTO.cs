namespace SampleWebAPI.DTO
{
    public class SwordTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }  
        public TypeReadDTO Type  { get; set; }
    }
}
