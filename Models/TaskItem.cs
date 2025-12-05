namespace TaskManagerApi.Models;

public class TaskItem
{
  public int Id { get; set; }
  public string Title { get; set; } = "";
  public string Description { get; set; } = "";
  public string DueDate { get; set; } = "";
  public string Status { get; set; } = "pending";
  public int UserId { get; set; }
   
}
