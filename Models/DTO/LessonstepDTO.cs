namespace Du_An_Web_Ban_Khoa_Hoc.Models.DTO
{
    public class LessonstepDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public string ContentType { get; set; } = "video";
        public int? FileId { get; set; }
        public int? DurationSec { get; set; }
        public int SortOrder { get; set; } = 1;

        public int? LessonId { get; set; } // Nếu muốn update/xóa bài học đã có
    }
}
