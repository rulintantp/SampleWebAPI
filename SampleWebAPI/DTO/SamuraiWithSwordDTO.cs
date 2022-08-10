namespace SampleWebAPI.DTO
{
    public class SamuraiWithSwordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SwordDTO> Swords { get; set;} = new List<SwordDTO> (); 
    }
}
