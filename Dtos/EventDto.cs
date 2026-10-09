using FirstSprintProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FirstSprintProject.Dtos;

public class EventDto
{

    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
    public required string Title { get; set; }
    public required string Description { get; set; }
    [Required(ErrorMessage = "Start time is required")]
    public DateTime? StartAt { get; set; }
    [Required(ErrorMessage = "End date is required")]
    [GreaterThan("StartAt")]
    public DateTime? EndAt { get; set; }
}
