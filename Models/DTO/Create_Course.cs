namespace Du_An_Web_Ban_Khoa_Hoc.Models.DTO
{
    public class Create_Course
    {
        public int? CourseId { get; set; }        // Nếu update
        public int StepNumber { get; set; } = 0;  // default 0 nếu chưa set
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? ThumbnailUrl { get; set; }
        public decimal? Price { get; set; }
        public string? Duration { get; set; }
        public string? Level { get; set; }
        public string? Prerequisites { get; set; }
        public string? LearningOutcomes { get; set; }
        public List<int>? TagIds { get; set; }
        public List<LessonstepDTO>? Lessons { get; set; }
    }
}
